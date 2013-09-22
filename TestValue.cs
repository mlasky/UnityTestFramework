using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

public class TestValue<T> 
{
    public  TestValue<T> not 
    {
        get { _inversion = !_inversion; return this; }
        set {}
    }

    private T           _value;
    private bool        _inversion = true;

    public TestValue(T val) 
    {
        _value = val;
    }

    public TestValue<T> ToBe(T val)
    {
        if (!(Comparer<T>.Equals(val, _value) == _inversion)) {
            throw new TestFailedException();
        }

        return this;
    }

    public override string ToString() 
    {
        return _value.ToString();
    }
}
