using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

[System.Serializable]
public class TestWindow : EditorWindow 
{
    private static  GUIStyle    _passedStyle;
    private static  GUIStyle    _failedStyle;
    private static  GUIStyle    _notRunStyle;
    private static  GUIStyle    _runningStyle;
    private static  GUIStyle    _suitePassedStyle;
    private static  Vector2     _scrollPosition;
              
    private         TestHarness _testHarness;
        
    [MenuItem ("Window/TestWindow")]
    static void Init () 
    {
        _scrollPosition = Vector2.zero;
        ShowWindow();
    }

    static void ShowWindow () 
    {
        Type type = typeof(TestWindow);
        EditorWindow.GetWindow(type, false, "TestWindow").Repaint();
    }
    
    void OnInspectorUpdate () {
        if (_testHarness == null) { _testHarness = new TestHarness(); }

        if (!_testHarness.HasTests) 
        {
            _testHarness.FindTests();
            ShowWindow();
        }
    }

    void OnGUI () 
    {
        _SetupStyles();
    
        
        if (_testHarness != null && _testHarness.HasTests) 
        {
            if (GUILayout.Button("Run Tests"))     { _testHarness.RunTests();  }
            if (GUILayout.Button("Refresh Tests")) { _testHarness.FindTests(); }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
                _DrawSuites();    
            EditorGUILayout.EndScrollView();
        }
        else {
            GUILayout.Label("No Tests.  Please Refresh.");
        }
    }

    private void _DrawSuites ()
    {
        if (_testHarness == null) { return; }

        foreach(string suiteName in _testHarness.GetSuiteNames()) 
        {
            GUILayout.Label(suiteName, _suitePassedStyle);

            _DrawTests(suiteName);
        }
    }

    private void _DrawTests (string suiteName)
    {
        foreach(TestRunner tr in _testHarness.GetTestsInSuite(suiteName))
        {
            GUILayout.Label(tr.Name, _GetStyle(tr));
        }
    }

    private GUIStyle _GetStyle (TestRunner tr)
    {
        GUIStyle style;
        switch (tr.TestState)
        {
            case TestRunner.TestStates.PASSED:
                style = _passedStyle;
                break;
            case TestRunner.TestStates.FAILED:
                style = _failedStyle;
                break;
            case TestRunner.TestStates.RUNNING:
                style = _runningStyle;
                break;
            case TestRunner.TestStates.NOT_RUN:
                style = _notRunStyle;
                break;
            default:
                style = _notRunStyle;
                break;
        }

        return style;
    }

    private static void _SetupStyles ()
    {
        if (_passedStyle == null)
        {
            Color passedColor = new Color(45f / 255f, 240f / 255f, 91f / 255f);

            _passedStyle = new GUIStyle();
            _passedStyle.padding = new RectOffset(15, 3, 3, 3);
            _passedStyle.margin  = new RectOffset(10, 1, 0, 1);
            _passedStyle.normal.textColor = Color.black;
            _passedStyle.normal.background = _ColorToTex(600, 1, passedColor);    
        }
        
        if (_failedStyle == null)
        {
            Color failedColor = new Color(240f / 255f, 45f / 255f, 45f / 255f);

            _failedStyle = new GUIStyle();
            _failedStyle.padding = new RectOffset(15, 3, 3, 3);
            _failedStyle.margin  = new RectOffset(10, 1, 0, 1);
            _failedStyle.normal.textColor = Color.white;
            _failedStyle.normal.background = _ColorToTex(600, 1, failedColor);    
        }

        if (_notRunStyle == null)
        {
            _notRunStyle = new GUIStyle();
            _notRunStyle.padding = new RectOffset(5, 3, 3, 3);
            _notRunStyle.margin  = new RectOffset(10, 1, 0, 1);
            _notRunStyle.normal.textColor = Color.white;
            _notRunStyle.normal.background = _ColorToTex(600, 1, Color.gray);    
        }

        if (_runningStyle == null)
        {
            _runningStyle = new GUIStyle();
            _runningStyle.padding = new RectOffset(15, 3, 3, 3);
            _runningStyle.normal.textColor = Color.white;
            _runningStyle.normal.background = _ColorToTex(600, 1, Color.yellow);    
        }

        if (_suitePassedStyle == null)
        {
            Color sColor = new Color(90f / 255f, 106f / 255f, 83f / 255f);
            
            _suitePassedStyle = new GUIStyle();
            _suitePassedStyle.padding = new RectOffset(5, 5, 5, 5);
            _suitePassedStyle.margin  = new RectOffset(10, 1, 0, 1);
            _suitePassedStyle.normal.textColor = Color.white;
            _suitePassedStyle.normal.background = _ColorToTex(600, 1, sColor);    
        }
    }

    private static Texture2D _ColorToTex (int width, int height, Color col)
    {
        Color[] pix = new Color[width*height];

        for(int i = 0; i < pix.Length; i++)
        {
            pix[i] = col;
        }
 
        Texture2D result = new Texture2D(width, height);

        result.SetPixels(pix);
        result.Apply();

        return result;
    }
}
