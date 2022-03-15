using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnnotationManager))]
public class AnnotationDebug : Editor
{
    private AnnotationManager _annotationManager;

    public void OnEnable()
    {
        _annotationManager = (AnnotationManager)this.target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Read Annotation and Location"))
        {
            try
            {
                Debug.Log("annotation at location: " +
                          _annotationManager.AnnotationReference.annotationLocation);
                Debug.Log("Annotation string: " + _annotationManager.AnnotationReference.annotationText);
            }
            catch
            {
                Debug.Log("Annotation Debugger Error");
            }
        }
    }
}
