using System;

[AttributeUsage(AttributeTargets.Method)]
public class BeforeEachTestAttribute : TestFrameworkAttribute 
{
    public BeforeEachTestAttribute (string suiteName) : base(suiteName) {}
}