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

    public TestStates testState;

    public TestRunner (MethodInfo m) : base(m)
    {
         testState  = TestStates.NOT_RUN;
    }

    public override void RunMethod ()
    {
        testState = TestStates.RUNNING;

        try {
            _method.Invoke(null, new [] { this });
            testState = TestStates.PASSED;
        } 
        catch (Exception) {
            testState = TestStates.FAILED;
        }
    }
}
