using System;

[AttributeUsage(AttributeTargets.Method)]
public class BeforeEachTestAttribute : TestFrameworkAttribute 
{
    public BeforeEachTestAttribute () : base() {}
}