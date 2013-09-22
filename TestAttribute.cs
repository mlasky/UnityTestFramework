using System;

[AttributeUsage(AttributeTargets.Method)]
public class TestAttribute : Attribute 
{
    public string Suite {
        get { return _suite;  }
        set {}
    }

    private string _suite;

    public TestAttribute() 
    {
        _suite = "";
    }

    public TestAttribute(string suiteName) 
    {
        _suite = suiteName;
    }
}
