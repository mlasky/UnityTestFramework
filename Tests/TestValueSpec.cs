using System;
using System.Reflection;

[HasTests("TestFramework::TestValue")]
public static class TestValueSpec 
{
    [Test]
    public static void ExpectTrueToBeTrue(TestRunner tr) 
    {
       tr.Expect(true).ToBe(true);
    }

    [Test]
    public static void ExpectTrueNotToBeNotTrue(TestRunner tr) 
    {
       tr.Expect(true).Not.Not.ToBe(true);
    }

    [Test]
    public static void ExpectOneToBeOne(TestRunner tr) 
    {
       tr.Expect(1).ToBe(1);
    }

    [Test]
    public static void ExpectOneFloatToBeOneInteger(TestRunner tr) 
    {
       tr.Expect(1f).ToBe(1);
    }  

    [Test]
    public static void ExpectHelloToBeHello(TestRunner tr) 
    {
       tr.Expect("Hello").ToBe("Hello");
    }

    [Test]
    public static void ExpectOneNotToBeTwo(TestRunner tr) 
    {
       tr.Expect(1).Not.ToBe(2);
    }

    [Test]
    public static void ExpectOnePointOneFloatNotToBeOneInteger(TestRunner tr) 
    {
       tr.Expect(1.1f).Not.ToBe(1);
    }  

    [Test]
    public static void ExpectTrueNotToBeFalse(TestRunner tr) 
    {
       tr.Expect(true).Not.ToBe(false);
    }

    [Test]
    public static void ExpectHelloNotToBeHi(TestRunner tr) 
    {
       tr.Expect("Hello").Not.ToBe("Hi");
    }

    [Test]
    public static void ExpectOneToBeLessThanTwo(TestRunner tr) 
    {
       tr.Expect(1).ToBeLessThan(2);
    }    

    [Test]
    public static void ExpectTwoNotToBeLessThanTwo(TestRunner tr) 
    {
       tr.Expect(2).Not.ToBeLessThan(2);
    }   

    [Test]
    public static void ExpectTwoToBeGreaterThanOne(TestRunner tr) 
    {
       tr.Expect(2).ToBeGreaterThan(1);
    }   

    [Test]
    public static void ExpectTwoNotToBeGreaterThanTwo(TestRunner tr) 
    {
       tr.Expect(2).Not.ToBeGreaterThan(2);
    }  
}