using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;


public class TestRunner : MethodRunner<TestFrameworkAttribute>
{
    public  enum        TestStates
    {
        NOT_RUN = 0,
        RUNNING = 1,
        PASSED  = 2,
        FAILED  = 3
    }

    public TestStates  TestState 
    {
        get { return _testState; }
        set {}
    }

    private TestStates _testState;

    public TestRunner (MethodInfo m) : base(m)
    {
         _testState  = TestStates.NOT_RUN;
    }

    public override void RunMethod ()
    {
        _testState = TestStates.RUNNING;

        try {
            _method.Invoke(null, new [] { this });
            _testState = TestStates.PASSED;
        } 
        catch (Exception) {
            _testState = TestStates.FAILED;
        }
    }
}
