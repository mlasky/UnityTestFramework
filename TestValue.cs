using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

public class TestValue<T> where T: IComparable<T>
{
    public  TestValue<T> Not 
    {
        get 
        { 
            _inversion = !_inversion; 
            return this; 
        }
        set {}
    }

    private T     _value;
    private bool  _inversion = true;

    public TestValue(T val) 
    {
        _value = val;
    }

    public TestValue<T> ToBe (T val)
    {
        if (!(Comparer<T>.Equals(val, _value) == _inversion)) 
        {
            throw new TestFailedException(_GetMessage<T>(val, "to be"));
        }
        else {
            throw new TestPassedException();
        }

        return this;
    }

    public TestValue<T> ToBeLessThan (T val) 
    {
        if ((val.CompareTo(_value) <= 0) == _inversion)
        {
            throw new TestFailedException(_GetMessage<T>(val, "to be less than"));
        }
        else {
            throw new TestPassedException();
        }
        
        return this;    
    }

    public TestValue<T> ToBeGreaterThan (T val) 
    {
        if ((val.CompareTo(_value) >= 0) == _inversion)
        {
            throw new TestFailedException(_GetMessage<T>(val, "to be greater than"));
        }
        else {
            throw new TestPassedException();
        }
            
        return this;
    }

    public string _GetMessage<T>(T val, string operation) 
    {
        return "Expected " + val + ((!_inversion)? " not ":" ") + operation + " " + _value;
    }

    public override string ToString () 
    {
        return _value.ToString();
    }
}
