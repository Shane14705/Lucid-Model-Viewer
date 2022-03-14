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

        //Add model to dictionary and increment key (pretty sure this key logic has to be networked in order to prevent duplicates) - some kind of owner ID?
        //TODO: NETWORK THIS ID LOGIC SO IT IS NOT BROKEN - AS IS, WE WILL HAVE DUPLICATE ERRORS!
        
        //Which is faster for checking if an annotation exists at a certain location? Iterating through each model, or checking if a key exists. Scratch that second part: position changes.
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
    
    // Start is called before the first frame update
    void Awake()
    {
        //Remote parameter tells whether the model was added remotely or locally...perhaps we should store the owner of each model with the annotation manager for permissions reasons
        this.model.annotations.modelAdded += (set, annotationModel, remote) => DisplayAnnotation(annotationModel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
