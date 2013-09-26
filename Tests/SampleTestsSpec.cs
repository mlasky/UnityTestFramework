using System;
using System.Reflection;

[HasTests("TestFramework::SampleTests")]
public class SampleTestsSpec 
{
    private static bool _member = false;

    [BeforeEachTest]
    public static void BeforeEach(BeforeEachTestRunner r)
    {
        _member = true;
    } 

    [Test]
    public static void ThisIsASampleTest(TestRunner r)
    {
        r.Expect(true).ToBe(true);
    }

    [Test]
    public static void ExpectMemberToBeTrue(TestRunner r)
    {
        r.Expect(_member).Not.ToBe(false);
    }
}
