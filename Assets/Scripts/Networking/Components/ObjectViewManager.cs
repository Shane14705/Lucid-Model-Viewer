using System;
using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class ObjectViewManager : RealtimeComponent<ObjectViewModel>
{
    //When we Instantiate an annotation prefab, use this variable to set the prefab's script values (ex: DictKey, model reference, etc)
    private AnnotationManager _newAnnotationManager;

    private AnnotationModel _newAnnotationModel;
    
    private GameObject _annotationPrefab;
    
    
    //Do I need to worry about this kind of thing for AnnotationModels that are added/removed to the collection, or is this just the equivalent of what I need to do when the Add or Remove callbacks are made?
    protected override void OnRealtimeModelReplaced(ObjectViewModel previousModel, ObjectViewModel currentModel)
    {
        base.OnRealtimeModelReplaced(previousModel, currentModel);
    }

    private void OnEnable()
    {
        Resources.Load<GameObject>("prefabs/AnnotationPrefab.prefab");
    }

    //This method sets up a model in order to add it to the dictionary
    public void CreateAnnotation(string currentString, Vector3 position)
    {
        //Initialize Model
        _newAnnotationModel = new AnnotationModel
        {
            annotationLocation = position,
            annotationText = currentString
        };

        //TODO: Check if an annotation already exists at the location and give the user the option to disable overlapping annotations
                this.model.annotations.Add(_newAnnotationModel);

    }

    //This method is responsible for actually instantiating an annotationPrefab when a model is added to the dictionary
    private void DisplayAnnotation(AnnotationModel newAnnotation)
    {
        _newAnnotationManager = Instantiate(_annotationPrefab).GetComponent<AnnotationManager>();
        _newAnnotationManager.AnnotationReference = newAnnotation;
        //We could eventually be giving each piece of a 3d Model its own ObjectManager, in which case this becomes important. Probably should consider the performance issues of such a system though...
        _newAnnotationManager.ObjectManager = this;
    }

    //Attempts to remove the annotation model from the data store, but first checks if the ID of the deleter is the one who owns the annotation
    public bool RemoveAnnotation(AnnotationModel annotationModel)
    {
        //TODO: Come up with solution to remove a user's ownership over all of their models when they leave
        if (annotationModel.creatorID == realtime.clientID)
        {
            this.model.annotations.Remove(annotationModel);
            Debug.Log("Model cleared successfully!");
            return true;

        }
        else
        {
            Debug.Log("RemoveAnnotation: Local user is not the user that created this annotation, and cannot delete it!");
            return false;
        }
        
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        //Remote parameter tells whether the model was added remotely or locally...perhaps we should store the owner of each model with the annotation manager for permissions reasons
        this.model.annotations.modelAdded += (set, annotationModel, remote) => Debug.Log("Model added!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
