using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectViewManager))]
public class ObjectViewDebugger : Editor
{
    private string _currentString;
    private ObjectViewManager _manager;

    private void OnEnable()
    {
        _manager = (ObjectViewManager)this.target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Add Annotation"))
        {
            _currentString = GUILayout.TextField(_currentString, 100);
            _manager.CreateAnnotation(_currentString, new Vector3(0,0,0));
        }
        

    }
}
