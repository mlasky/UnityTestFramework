using System;

[AttributeUsage(AttributeTargets.Method)]
public class TestAttribute : TestFrameworkAttribute 
{
    public TestAttribute () : base() {}
}
