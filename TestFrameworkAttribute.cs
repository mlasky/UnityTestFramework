using System;

public class TestFrameworkAttribute : Attribute 
{
    public string Suite 
    {
        get { return _suite;  }
        set {}
    }

    private string _suite;

    public TestFrameworkAttribute () 
    { 
        _suite = ""; 
    }

    public TestFrameworkAttribute (string suiteName)
    {
         _suite = suiteName;
    }
} 