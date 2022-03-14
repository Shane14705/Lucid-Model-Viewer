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
        base.OnInspectorGUI();

        _currentString = EditorGUILayout.TextField(_currentString);
        
        if (GUILayout.Button("Add Annotation"))
        {
            _manager.CreateAnnotation(_currentString, new Vector3(0,0,0));
        }
        

    }
}
