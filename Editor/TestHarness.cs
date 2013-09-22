using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

public class TestHarness : EditorWindow 
{
    private static  Dictionary<string, List<TestRunner>> _testSuites;
    private static  GUIStyle                             _passedStyle;
    private static  GUIStyle                             _failedStyle;
    private static  GUIStyle                             _notRunStyle;
    private static  GUIStyle                             _runningStyle;
    private static  GUIStyle                             _suitePassedStyle;
        
    [MenuItem ("Window/TestHarness")]
    static void Init () 
    {
        ShowWindow();
    }

    static void ShowWindow () 
    {
        EditorWindow.GetWindow(typeof(TestHarness), false, "TestHarness");
    }
    
    void OnInspectorUpdate () {}

    void OnGUI () 
    {
        _setupStyles();

        if (_testSuites != null) 
        {
            foreach(string suiteName in _testSuites.Keys) 
            {
                GUILayout.Label(suiteName, _suitePassedStyle);

                foreach(TestRunner tr in _testSuites[suiteName])
                {
                    GUIStyle style;
                    switch (tr.testState)
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

                    GUILayout.Label(tr.Name, style);
                }
            }
        }
        else {
            GUILayout.Label("No Tests.  Please Refresh.");
        }

        if (GUILayout.Button("Run Tests"))     { RunTests();  }
        if (GUILayout.Button("Refresh Tests")) { FindTests(); }
    }

    public void RunTests () 
    {
        foreach(List<TestRunner> testSuite in _testSuites.Values)
        {
            foreach(TestRunner tr in testSuite)
            {
                tr.RunTest();
            }
        }
    }

    public void FindTests () 
    {
        _testSuites = new Dictionary<string, List<TestRunner>>();

        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach(Assembly a in assemblies)
        {
            foreach(Type t in a.GetTypes()) 
            {
                foreach(MethodInfo m in t.GetMethods())
                {
                    if (_IsTestMethod(m)) { _AddTest(m); }
                }
            }
        }
    }

    private bool _IsTestMethod (MethodInfo m) 
    {
        return m.GetCustomAttributes(typeof(TestAttribute), true).Length > 0;
    }

    private void _AddTest (MethodInfo m) 
    {
        TestAttribute[] testAttrs = (TestAttribute[]) 
                                    m.GetCustomAttributes(typeof(TestAttribute), true);
        
        foreach (TestAttribute ta in testAttrs) 
        {
            TestRunner tr = new TestRunner(m);

            try { _testSuites[ta.Suite].Add(tr); } 
            catch (KeyNotFoundException) 
            {
                _testSuites[ta.Suite] = new List<TestRunner>();
                _testSuites[ta.Suite].Add(tr);
            }
        }
    }

    private static void _setupStyles ()
    {
        if (_passedStyle == null)
        {
            Color passedColor = new Color(45f / 255f, 240f / 255f, 91f / 255f);

            _passedStyle = new GUIStyle();
            _passedStyle.padding = new RectOffset(15, 3, 3, 3);
            _passedStyle.normal.textColor = Color.black;
            _passedStyle.normal.background = _ColorToTex(600, 1, passedColor);    
        }
        
        if (_failedStyle == null)
        {
            Color failedColor = new Color(240f / 255f, 45f / 255f, 45f / 255f);
            _failedStyle = new GUIStyle();
            _failedStyle.padding = new RectOffset(15, 3, 3, 3);
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
