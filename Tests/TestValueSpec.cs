using System;
using System.Reflection;

[HasTests]
public static class TestValueSpec 
{
    [Test("TestFramework::TestValue::ToBe")]
    public static void ExpectTrueToBeTrue(TestRunner tr) 
    {
       tr.Expect(true).ToBe(true);
    }

    [Test("TestFramework::TestValue::ToBe")]
    public static void ExpectTrueNotToBeNotTrue(TestRunner tr) 
    {
       tr.Expect(true).Not.Not.ToBe(true);
    }

    [Test("TestFramework::TestValue::ToBe")]
    public static void ExpectOneToBeOne(TestRunner tr) 
    {
       tr.Expect(1).ToBe(1);
    }

    [Test("TestFramework::TestValue::ToBe")]
    public static void ExpectOneFloatToBeOneInteger(TestRunner tr) 
    {
       tr.Expect(1f).ToBe(1);
    }  

    [Test("TestFramework::TestValue::ToBe")]
    public static void ExpectHelloToBeHello(TestRunner tr) 
    {
       tr.Expect("Hello").ToBe("Hello");
    }

    [Test("TestFramework::TestValue::ToBe")]
    public static void ExpectOneNotToBeTwo(TestRunner tr) 
    {
       tr.Expect(1).Not.ToBe(2);
    }

    [Test("TestFramework::TestValue::ToBe")]
    public static void ExpectOnePointOneFloatNotToBeOneInteger(TestRunner tr) 
    {
       tr.Expect(1.1f).Not.ToBe(1);
    }  

    [Test("TestFramework::TestValue::ToBe")]
    public static void ExpectTrueNotToBeFalse(TestRunner tr) 
    {
       tr.Expect(true).Not.ToBe(false);
    }

    [Test("TestFramework::TestValue::ToBe")]
    public static void ExpectHelloNotToBeHi(TestRunner tr) 
    {
       tr.Expect("Hello").Not.ToBe("Hi");
    }

    [Test("TestFramework::TestValue::ToBeLessThan")]
    public static void ExpectOneToBeLessThanTwo(TestRunner tr) 
    {
       tr.Expect(1).ToBeLessThan(2);
    }    

    [Test("TestFramework::TestValue::ToBeLessThan")]
    public static void ExpectTwoNotToBeLessThanTwo(TestRunner tr) 
    {
       tr.Expect(2).Not.ToBeLessThan(2);
    }   

    [Test("TestFramework::TestValue::ToBeGreaterThan")]
    public static void ExpectTwoToBeGreaterThanOne(TestRunner tr) 
    {
       tr.Expect(2).ToBeGreaterThan(1);
    }   

    [Test("TestFramework::TestValue::ToBeGreaterThan")]
    public static void ExpectTwoNotToBeGreaterThanTwo(TestRunner tr) 
    {
       tr.Expect(2).Not.ToBeGreaterThan(2);
    }  
}