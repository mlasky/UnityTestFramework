using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

public class TestHarness 
{
    public bool HasTests 
    {
        get { return _testSuites != null && _testSuites.Count > 0; }
        set {}
    }
    private static  Dictionary<string, List<TestRunner>> _testSuites;

    public void RunTests () 
    {
        foreach(List<TestRunner> testSuite in _testSuites.Values)
        {
            foreach(TestRunner tr in testSuite) { tr.RunTest(); }
        }
    }

    public void FindTests () 
    {
        _testSuites = new Dictionary<string, List<TestRunner>>();

        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        _CheckAssemblies(assemblies);
    }

    private void _CheckAssemblies (Assembly[] assemblies)
    {
        foreach(Assembly a in assemblies) { _CheckTypes(a); }
    }

    private void _CheckTypes (Assembly a) 
    {
        foreach(Type t in a.GetTypes()) 
        { 
            if (_IsTestedType(t)) { _CheckMethods(t); }
        }
    }

    private void _CheckMethods (Type t) 
    {
        foreach(MethodInfo m in t.GetMethods())
        {
            if (_IsTestMethod(m)) { _AddTest(m); }
        }
    }

    public  Dictionary<string, List<TestRunner>>.KeyCollection GetSuiteNames () 
    {
        return _testSuites.Keys;
    }

    public Dictionary<string, List<TestRunner>>.ValueCollection GetSuites () 
    {
        return _testSuites.Values;
    }

    public List<TestRunner> GetTests(string suiteName)
    {
        return _testSuites[suiteName];
    }

    private bool _IsTestMethod (MethodInfo m) 
    {
        return m.GetCustomAttributes(typeof(TestAttribute), true).Length > 0;
    }

    private bool _IsTestedType (Type t)
    {
        return t.GetCustomAttributes(typeof(HasTestsAttribute), true).Length > 0;   
    }

    private void _AddTest (MethodInfo m) 
    {
        System.Type type = typeof(TestAttribute);
        
        TestAttribute[] testAttrs = (TestAttribute[]) 
                                    m.GetCustomAttributes(type, true);
        
        foreach (TestAttribute ta in testAttrs) 
        {
            TestRunner tr = new TestRunner(m);

            try { _testSuites[ta.Suite].Add(tr); } 
            catch (KeyNotFoundException) 
            {
                _testSuites[ta.Suite] = new List<TestRunner>();
                _testSuites[ta.Suite].Add(tr);
            }
        }
    }
}