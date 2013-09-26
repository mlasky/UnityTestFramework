using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

[HasTests("TestFramework::TestHarness")]
public class TestHarnessSpec 
{
    public static TestHarness harness;
    public static string      sampleSuiteName;

    [BeforeEachTest]
    public static void BeforeEach (BeforeEachTestRunner r)
    {
        harness         = new TestHarness();
        sampleSuiteName = "TestFramework::SampleTests";
    }

    [Test]
    public static void HasTestSuitesProperty (TestRunner r)
    {
        harness.FindTests();

        string foundName = harness.TestSuites[sampleSuiteName].Name;

        r.Expect(foundName).ToBe(sampleSuiteName);
    }

    [Test]
    public static void FindsTheSampleTestSuite (TestRunner r)
    {
        harness.FindTests();
        bool found = false;
        foreach(string suitename in harness.GetSuiteNames())
        {
            if (suitename == sampleSuiteName)
            {
                found = true;
            }
        }

        r.Expect(found).ToBe(true);
    }

    [Test]
    public static void FindsAllMethodsInSampleSuite (TestRunner r)
    {
        harness.FindTests();
        Type sampleType = typeof(SampleTestsSpec);
        List<MethodInfo> methods = TestHarness.FindTestMethodsInType(sampleType);

        r.Expect(methods.Count).ToBe(3);
    }

    [Test]
    public static void GetTestsReturnsAllTestsInSampleSuite (TestRunner r)
    {
        harness.FindTests();

        List<TestRunner> testsFound = harness.GetTestsInSuite(sampleSuiteName);

        r.Expect(testsFound.Count).ToBe(2);
    }

    [Test]
    public static void FindTestMethodsInTypeGetsAllMethodsInSampleSuite (TestRunner r)
    {
        harness.FindTests();

        Type specType = typeof(SampleTestsSpec);
        List<MethodInfo> methods = TestHarness.FindTestMethodsInType(specType);

        r.Expect(methods.Count).ToBe(3);
    }
}