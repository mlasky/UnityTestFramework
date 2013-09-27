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

    public string       StatusMessage 
    {
        get { return _statusMessage; }
        set {}
    }

    public TestStates  TestState 
    {
        get { return _testState; }
        set {}
    }

    private TestStates _testState;
    private string     _statusMessage = "Not Run";

    public TestRunner (MethodInfo m) : base(m)
    {
         _testState  = TestStates.NOT_RUN;
    }

    public override void RunMethod ()
    {
        _testState = TestStates.FAILED;
        _statusMessage = "Test Failed";

        try {
            _method.Invoke(null, new [] { this });
        } 
        catch (TargetInvocationException tie) {
            if (tie.InnerException is TestPassedException)
            {
                _testState = TestStates.PASSED;
                _statusMessage = "Passed!";
            }
            else if (tie.InnerException is TestFailedException)
            {
                _testState = TestStates.FAILED;
                TestFailedException failedException = (TestFailedException) 
                                                      tie.InnerException;
                _statusMessage = failedException.message;    
            }
            
        }
    }
}