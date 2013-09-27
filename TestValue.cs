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
        _TestWith<T>(Comparer<T>.Equals(_value, val), val, "to be");
        return this;
    }

    public TestValue<T> ToBeLessThan (T val) 
    {
        _TestWith<T>(_value.CompareTo(val) < 0, val, "to be less than");    
        return this;
    }

    public TestValue<T> ToBeGreaterThan (T val) 
    {
        _TestWith<T>(_value.CompareTo(val) > 0, val, "to be greater than");
        return this;
    }

    public string _GetMessage<T>(T val, string operation) 
    {
        string not = (!_inversion)? " not ":" ";

        return  "Expected " + _value + not + operation + " " + val;
    }

    private void _TestWith<T>(bool expression, T val, string failMsgPart)
    {
        if (expression == _inversion) {
            throw new TestPassedException();            
        }
        else {
            throw new TestFailedException(_GetMessage<T>(val, failMsgPart));
        }
    }

    public override string ToString () 
    {
        return _value.ToString();
    }
}
