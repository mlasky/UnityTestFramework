using System;

[AttributeUsage(AttributeTargets.Method)]
public class TestAttribute : TestFrameworkAttribute 
{
    public TestAttribute (string suiteName) : base(suiteName) {}
}
