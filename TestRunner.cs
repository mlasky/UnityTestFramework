using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System;
using UnityEngine;

public class TestRunner 
{
    public  enum        TestStates
    {
        NOT_RUN = 0,
        RUNNING = 1,
        PASSED  = 2,
        FAILED  = 3
    }

    public  string      Name 
    {
        get { return _name; }
        set {}
    }

    public string       Suite
    {
        get { return _suite; }
        set {}
    }

    private MethodInfo _method;
    private string     _name;
    private string     _suite;
    
    public TestStates testState;

    public TestRunner(MethodInfo m)
    {
        System.Type     t           = typeof(TestAttribute);
        TestAttribute[] testAttrs   = (TestAttribute[]) 
                                      m.GetCustomAttributes(t, true);

        _method    = m;
        _name      = Regex.Replace(m.Name, "([A-Z])", " $1", RegexOptions.None);
        _suite     = testAttrs[0].Suite;
        testState  = TestStates.NOT_RUN;
    }

    public void RunTest()
    {
        testState = TestStates.RUNNING;

        try {
            _method.Invoke(null, new [] { this });
            testState = TestStates.PASSED;
        } 
        catch (Exception e) {
            testState = TestStates.FAILED;
        }
    }

    public TestValue<T> Expect<T>(T val)
    {
        return new TestValue<T>(val);
    }
}
