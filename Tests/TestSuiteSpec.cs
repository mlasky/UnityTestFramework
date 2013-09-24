using System;
using System.Reflection;

[HasTests]
public static class TestSuiteSpec 
{
    public static TestSuite testSuite;

    private static MethodInfo _GetMethod (string name)
    {
        Type type = typeof(TestSuiteSpec);

        return type.GetMethod(name,
                                    BindingFlags.Static | 
                                    BindingFlags.Public | 
                                    BindingFlags.FlattenHierarchy);
    }
    
    [BeforeEachTest("TestFramework::TestSuite")]
    public static void TestSuiteBeforeEach(BeforeEachTestRunner r)
    {
        testSuite = new TestSuite("My Awesome Test Suite");            
    }

    [Test("TestFramework::TestSuite")]
    public static void HasName (TestRunner r)
    {
        r.Expect(testSuite.Name).ToBe("My Awesome Test Suite");
    }

    [Test("TestFramework::TestSuite")]
    public static void CanAddTestMethods (TestRunner r)
    {
        string      expected    = "HasName";
        MethodInfo  method      = _GetMethod(expected);

        testSuite.AddMethod(method);

        string methodName = testSuite.GetTestRunner(expected).MethodName;

        r.Expect(testSuite.GetTestRunner(methodName).MethodName).ToBe(expected);
    }

    [Test("TestFramework::TestSuite")]
    public static void CanAddBeforeEachMethods (TestRunner r)
    {
        string      expected   = "TestSuiteBeforeEach";
        MethodInfo  method     = _GetMethod(expected);

        testSuite.AddMethod(method);

        string  methodName = testSuite.GetBeforeEachTestRunner(expected).MethodName;

        r.Expect(methodName).ToBe(expected);
    }

    [Test("TestFramework::TestSuite")]
    public static void MethodHasAttributeReturnsTrueWhenMethodHasAttribute (TestRunner r)
    {
        MethodInfo method = _GetMethod("HasName");

        bool testAttrCheck = TestSuite.MethodHasAttribute<TestAttribute>(method);

        r.Expect(testAttrCheck).ToBe(true);
    }

    [Test("TestFramework::TestSuite")]
    public static void MethodHasAttributeReturnsFalseWhenMethodDoesNotHaveAttribute (TestRunner r)
    {
        MethodInfo method = _GetMethod("HasName");

        bool testAttrCheck = TestSuite.MethodHasAttribute<HasTestsAttribute>(method);

        r.Expect(testAttrCheck).ToBe(false);
    }
}