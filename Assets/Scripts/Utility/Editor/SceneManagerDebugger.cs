using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneStateComponent))]
public class SceneManagerDebugger : Editor
{
    private string _currentURI;
    private SceneStateComponent _stateManager;
    private void OnEnable()
    {
        _stateManager = (SceneStateComponent)this.target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _currentURI = EditorGUILayout.TextField(_currentURI);

        if (GUILayout.Button("Add Object to Scene"))
        {
            //TODO: WRITE LOGIC TO CHECK IF THE URI ENTERED LEADS TO A VALID FILE
            Debug.Log("This should call a method to instantiate an object view manager with the supplied object into the scene!");
        }


    }
}
