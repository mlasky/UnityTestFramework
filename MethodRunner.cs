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

    public string       Suite
    {
        get { return _suite; }
        set {}
    }

    protected   MethodInfo _method;
    private     string     _name;
    private     string     _methodName;
    private     string     _suite;

    protected T[]      _attributes;
    
    public MethodRunner (MethodInfo m)
    {
        _attributes = (T[]) m.GetCustomAttributes(typeof(T), true);
        _method     = m;
        _methodName = m.Name;
        _name       = Regex.Replace(m.Name, "([A-Z])", " $1", RegexOptions.None);
        _suite      = _attributes[0].Suite;
    }    
    
    public virtual void RunMethod() 
    {
        _method.Invoke(_method.GetType(), new [] { this });
    }

    public TestValue<T> Expect<T> (T val) where T: IComparable<T>
    {
        return new TestValue<T>(val);
    }
}