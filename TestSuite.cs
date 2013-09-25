using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine; 

public class TestSuite
{
    public string Name 
    {
        get { return _name; }
        set {}
    }
    
    private string                                   _name;
    private Dictionary<string, TestRunner>           _testRunners;    
    private Dictionary<string, BeforeEachTestRunner> _beforeEachTestRunners;
    
    public TestSuite (string name)
    {
        _name                  = name;

        _testRunners           = new Dictionary<string, TestRunner>();
        _beforeEachTestRunners = new Dictionary<string, BeforeEachTestRunner>();
    }

    public void ExecuteTests ()
    {
        foreach(TestRunner tr in _testRunners.Values)
        {
            foreach(BeforeEachTestRunner ber in _beforeEachTestRunners.Values)
            {
                ber.RunMethod();
            }
            tr.RunMethod();
        }
    }

    public void AddMethod (MethodInfo method)
    {
        if      (IsTestMethod(method))       { AddTestMethod(method);       }
        else if (IsBeforeEachMethod(method)) { AddBeforeEachMethod(method); }
    }

    public TestRunner GetTestRunner (string name)
    {
        return _testRunners[name];
    }

    public List<TestRunner> GetTestRunners ()
    {
        List<TestRunner> testRunners = new List<TestRunner>();

        foreach (TestRunner tr in _testRunners.Values)
        {
            testRunners.Add(tr);
        }
        return testRunners;
    }

    public BeforeEachTestRunner GetBeforeEachTestRunner (string name)
    {
        return _beforeEachTestRunners[name];
    }

    public void AddTestMethod (MethodInfo method)
    {
        TestRunner tr = new TestRunner(method);
        _testRunners[tr.MethodName] = tr;
    }

    public void AddBeforeEachMethod (MethodInfo method)
    {
        BeforeEachTestRunner tr = new BeforeEachTestRunner(method);
        _beforeEachTestRunners[tr.MethodName] = tr;
    }

    public static bool IsTestMethod (MethodInfo method) 
    {
        return MethodHasAttribute<TestAttribute>(method);
    }

    public static bool IsBeforeEachMethod (MethodInfo method) 
    {
        return MethodHasAttribute<BeforeEachTestAttribute>(method);
    }

    public static bool MethodHasAttribute<T> (MethodInfo m)
    {
        return m.GetCustomAttributes(typeof(T), true).Length > 0;
    }

    public static string GetSuiteNameFromMethod(MethodInfo method)
    {
        Type type = typeof(TestFrameworkAttribute);
        TestFrameworkAttribute attr = (TestFrameworkAttribute) 
                                      method.GetCustomAttributes(type, true)[0];
        return attr.Suite;
    }

    public override string ToString() 
    {
        return "TestSuite: " + Name;
    }
}

