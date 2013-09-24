using System;

[AttributeUsage(AttributeTargets.Class)]
public class HasTestsAttribute : TestFrameworkAttribute 
{
    public HasTestsAttribute (string suiteName) : base(suiteName) {}
    public HasTestsAttribute () : base() {}
}
