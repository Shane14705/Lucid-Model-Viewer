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

    private LineRenderer _lineRenderer;

    private UIDisplay _uiManager;

    private void OnEnable()
    {
        _lineRenderer = this.GetComponent<LineRenderer>();
        _uiManager = this.GetComponentInChildren<UIDisplay>();
    }

    private void Start()
    {
        Vector3[] positions = new Vector3[2]
        {
            this.transform.localPosition, (this._uiManager.distanceFromModel * this._uiManager.uiPlacementLocalSpace)
            
        };
        _lineRenderer.SetPositions(positions);
    }

    //This function will handle changing the text that goes with the annotation
    public void UpdateAnnotationText(AnnotationModel model, string value)
    {
        throw new NotImplementedException();
    }

    //This function will handle changing the point on the 3d model where the annotation should stem from
    public void UpdateAnnotationLocation(AnnotationModel model, Vector3 value)
    {
        
    }

    private void OnDestroy()
    {
        //TODO: Fix issue where we can unsubscribe from events and destroy annotation here, but model wont actually get removed in ObjViewManager due to us not being the owners of it
        //If this really ends up being a problem we can pull this logic into an unloading routine that we remember to call, since we already probably need to for initialization
        this._annotationReference.annotationLocationDidChange -= UpdateAnnotationLocation;
        this._annotationReference.annotationTextDidChange -= UpdateAnnotationText;

        //Remember, this must be the final call as the model will likely get GC'd soon after this, and we dont want null references
        this._objectManager.RemoveAnnotation(this._annotationReference);
    }
}
