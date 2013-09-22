using System;

[AttributeUsage(AttributeTargets.Method)]
public class TestBeforeEachAttribute : Attribute 
{
    public TestBeforeEachAttribute() {}
}