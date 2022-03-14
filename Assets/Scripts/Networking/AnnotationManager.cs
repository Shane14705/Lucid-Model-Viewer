using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script is included on each instantiated annotation, and is responsible for handling updates and initialization for annotations after their model is added/removed from the ObjectViewManager's RealtimeDictionary
public class AnnotationManager : MonoBehaviour
{
    
    //Question: can the annotation model have an owner even though it has no RealtimeComponent (to lock editing to only certain clients)?
    private AnnotationModel _annotationReference;

    public AnnotationModel AnnotationReference
    {
        get => _annotationReference;
        set => _annotationReference = value;
    }

    public ObjectViewManager ObjectManager
    {
        get => _objectManager;
        set => _objectManager = value;
    }

    private ObjectViewManager _objectManager;
    
    
    private void Awake()
    {
        this._annotationReference.annotationLocationDidChange += UpdateAnnotationLocation;    
        this._annotationReference.annotationTextDidChange += UpdateAnnotationText;
    }

    //This function will handle changing the text that goes with the annotation
    private void UpdateAnnotationText(AnnotationModel model, string value)
    {
        throw new NotImplementedException();
    }

    //This function will handle changing the point on the 3d model where the annotation should stem from
    private void UpdateAnnotationLocation(AnnotationModel model, Vector3 value)
    {
        throw new NotImplementedException();
    }

    private void OnDestroy()
    {
        this._annotationReference.annotationLocationDidChange -= UpdateAnnotationLocation;
        this._annotationReference.annotationTextDidChange -= UpdateAnnotationText;

        //Remember, this must be the final call as the model will likely get GC'd soon after this, and we dont want null references
        this._objectManager.RemoveAnnotation(this._annotationReference);
    }
}
