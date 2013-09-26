using System;

[AttributeUsage(AttributeTargets.Class)]
public class HasTestsAttribute : TestFrameworkAttribute 
{
    public string SuiteName 
    {
        get { return _suiteName;  }
        set {}
    }

    private string _suiteName;

    public HasTestsAttribute () : base()
    { 
        _suiteName = ""; 
    }

    public HasTestsAttribute (string suiteName) : base()
    {
         _suiteName = suiteName;
    }
}
