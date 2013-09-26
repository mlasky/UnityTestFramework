using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

[HasTests]
public class TestHarness 
{
    public bool HasTests 
    {
        get { return _testSuites != null && _testSuites.Count > 0; }
        set {}
    }

    public Dictionary<string, TestSuite> TestSuites 
    {
        get { return _testSuites; }
        set {}
    }
    
    private List<MethodInfo>                _testFrameworkMethods;
    private Dictionary<string, TestSuite>   _testSuites;

    public void RunTests () 
    {
        foreach(TestSuite testSuite in _testSuites.Values)
        {
            testSuite.ExecuteTests();
        }
    }

    public void FindTests () 
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        _testFrameworkMethods = FindTestMethodsInAssemblies(assemblies);
        _testSuites = BuildSuites(_testFrameworkMethods);
    }

    public List<string> GetSuiteNames ()
    {
        List<string> suiteNames = new List<string>();

        foreach(string suiteName in _testSuites.Keys)
        {
            suiteNames.Add(suiteName);
        }
        
        return suiteNames;
    }

    public List<TestRunner> GetTests () 
    {
        List<TestRunner> tests = new List<TestRunner>();

        foreach(TestSuite ts in _testSuites.Values)
        {
            tests.AddRange(ts.GetTestRunners());
        }
        
        return tests;
    }

    public List<TestRunner> GetTestsInSuite (string suiteName) 
    {
        return _testSuites[suiteName].GetTestRunners();
    }

    public static Dictionary<string, TestSuite> BuildSuites (List<MethodInfo> methods)
    {
        Dictionary<string, TestSuite> suites = new Dictionary<string, TestSuite>();
        
        foreach (MethodInfo method in methods)
        {
            string name = TestSuite.GetSuiteNameFromMethod(method);
            
            if (!suites.ContainsKey(name)) 
            { 
                suites[name] = new TestSuite(name); 
            }
            
            suites[name].AddMethod(method);
        }
        
        return suites;
    }

    public static List<MethodInfo> FindTestMethodsInAssemblies (Assembly[] assemblies)
    {
        List<MethodInfo> testMethods = new List<MethodInfo>();

        foreach(Assembly assembly in assemblies) 
        { 
            testMethods.AddRange(FindTestMethodsInAssembly(assembly));
        }

        return testMethods;
    }

    public static List<MethodInfo> FindTestMethodsInAssembly (Assembly assembly) 
    {
        List<MethodInfo> testMethods = new List<MethodInfo>();

        foreach(Type type in assembly.GetTypes()) 
        { 
            if (IsTestedType(type)) 
            { 
                testMethods.AddRange(FindTestMethodsInType(type));
            }
        }
        
        return testMethods;
    }

    public static List<MethodInfo> FindTestMethodsInType (Type type) 
    {
        List<MethodInfo> testMethods = new List<MethodInfo>();

        foreach(MethodInfo method in type.GetMethods())
        {   
            if (IsTestFrameWorkMethod(method))
            {
                testMethods.Add(method);
            }
        }
        
        return testMethods;
    }

    public static bool IsTestFrameWorkMethod (MethodInfo m)
    {
        Type type = typeof(TestFrameworkAttribute);
        return m.GetCustomAttributes(type, true).Length > 0;
    }

    public static bool IsTestedType (Type t)
    {
        Type type = typeof(HasTestsAttribute);
        return t.GetCustomAttributes(type, true).Length > 0;   
    }
}