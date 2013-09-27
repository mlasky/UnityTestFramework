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
        get { return _statusMessages[(int) _testState]; }
        set {}
    }

    public TestStates  TestState 
    {
        get { return _testState; }
        set {}
    }

    private string[]   _statusMessages;
    private TestStates _testState;

    public TestRunner (MethodInfo m) : base(m)
    {
        _testState  = TestStates.NOT_RUN;

        _statusMessages = new string[4];
        _statusMessages[(int) TestStates.NOT_RUN] = "Not Run";
        _statusMessages[(int) TestStates.RUNNING] = "Running";
        _statusMessages[(int) TestStates.PASSED]  = "Passed";
        _statusMessages[(int) TestStates.FAILED]  = "Failed";
    }

    public override void RunMethod ()
    {
        _testState = TestStates.FAILED;

        try {
            _method.Invoke(null, new [] { this });
        } 
        catch (TargetInvocationException tie) 
        {
            if (tie.InnerException is TestPassedException) {
                _OnTestPassed(tie.InnerException);
            }
            else if (tie.InnerException is TestFailedException) {
                _OnTestFailure((TestFailedException) tie.InnerException);    
            }
        }
    }

    private void _OnTestPassed (Exception e) 
    {
        _testState = TestStates.PASSED;
    }

    private void _OnTestFailure (TestFailedException e)
    {
        _testState = TestStates.FAILED;
        _statusMessages[(int) TestStates.FAILED] = e.testStatus;
    }
}