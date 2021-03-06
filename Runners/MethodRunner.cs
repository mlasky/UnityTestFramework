using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System;
using UnityEngine;

public class MethodRunner<T> where T: TestFrameworkAttribute 
{
    public  string      Name 
    {
        get { return _name; }
        set {}
    }

    public string       MethodName
    {
        get { return _methodName; }
        set {}
    }

    public MethodInfo   Method
    {
        get {return _method; }
        set {}
    }

    protected   MethodInfo _method;
    
    private     string     _name;
    private     string     _methodName;
 
    
    public MethodRunner (MethodInfo m)
    {
        _method     = m;
        _methodName = m.Name;
        _name       = Regex.Replace(m.Name, "([A-Z])", " $1", RegexOptions.None);
    }    
    
    public virtual void RunMethod() 
    {
        _method.Invoke(null, new [] { this });
    }

    public TestValue<U> Expect<U> (U val) where U: IComparable<U>
    {
        return new TestValue<U>(val);
    }
}