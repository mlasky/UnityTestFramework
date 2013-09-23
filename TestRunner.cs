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
            _method.Invoke(_method.GetType(), new [] { this });
            testState = TestStates.PASSED;
        } 
        catch (Exception e) {
            testState = TestStates.FAILED;
        }
    }

    public TestValue<T> Expect<T>(T val) where T: IComparable<T>
    {
        return new TestValue<T>(val);
    }

    [Test("TestFramework::Expect::ToBe")]
    public static void ExpectTrueToBeTrue(TestRunner tr) 
    {
       tr.Expect(true).ToBe(true);
    }

    [Test("TestFramework::Expect::ToBe")]
    public static void ExpectTrueNotToBeNotTrue(TestRunner tr) 
    {
       tr.Expect(true).Not.Not.ToBe(true);
    }

    [Test("TestFramework::Expect::ToBe")]
    public static void ExpectOneToBeOne(TestRunner tr) 
    {
       tr.Expect(1).ToBe(1);
    }

    [Test("TestFramework::Expect::ToBe")]
    public static void ExpectOneFloatToBeOneInteger(TestRunner tr) 
    {
       tr.Expect(1f).ToBe(1);
    }  

    [Test("TestFramework::Expect::ToBe")]
    public static void ExpectHelloToBeHello(TestRunner tr) 
    {
       tr.Expect("Hello").ToBe("Hello");
    }

    [Test("TestFramework::Expect::ToBe")]
    public static void ExpectOneNotToBeTwo(TestRunner tr) 
    {
       tr.Expect(1).Not.ToBe(2);
    }

    [Test("TestFramework::Expect::ToBe")]
    public static void ExpectOnePointOneFloatNotToBeOneInteger(TestRunner tr) 
    {
       tr.Expect(1.1f).Not.ToBe(1);
    }  

    [Test("TestFramework::Expect::ToBe")]
    public static void ExpectTrueNotToBeFalse(TestRunner tr) 
    {
       tr.Expect(true).Not.ToBe(false);
    }

    [Test("TestFramework::Expect::ToBe")]
    public static void ExpectHelloNotToBeHi(TestRunner tr) 
    {
       tr.Expect("Hello").Not.ToBe("Hi");
    }

    [Test("TestFramework::Expect::ToBeLessThan")]
    public static void ExpectOneToBeLessThanTwo(TestRunner tr) 
    {
       tr.Expect(1).ToBeLessThan(2);
    }    

    [Test("TestFramework::Expect::ToBeLessThan")]
    public static void ExpectTwoNotToBeLessThanTwo(TestRunner tr) 
    {
       tr.Expect(2).Not.ToBeLessThan(2);
    }   

    [Test("TestFramework::Expect::ToBeGreaterThan")]
    public static void ExpectTwoToBeGreaterThanOne(TestRunner tr) 
    {
       tr.Expect(2).ToBeGreaterThan(1);
    }   

    [Test("TestFramework::Expect::ToBeGreaterThan")]
    public static void ExpectTwoNotToBeGreaterThanTwo(TestRunner tr) 
    {
       tr.Expect(2).Not.ToBeGreaterThan(2);
    }   
}
