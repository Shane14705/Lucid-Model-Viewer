using System;
using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using Normal.Realtime.Serialization;
using UnityEngine;

public class ObjectViewManager : RealtimeComponent<ObjectViewModel>
{
    //When we Instantiate an annotation prefab, use this to set the prefab's script values (ex: DictKey, model reference, etc)
    
    //REMEMBER: THIS IS NOT NETWORKED
    private Dictionary<AnnotationModel, AnnotationManager> _AnnotationRegistry = new Dictionary<AnnotationModel, AnnotationManager>();

    private AnnotationModel _newAnnotationModel;
    private AnnotationManager _newAnnotationManager;
    
    private GameObject _annotationPrefab;

    private RealtimeSet<AnnotationModel>.ModelAdded _addCallback;
    private RealtimeSet<AnnotationModel>.ModelRemoved _removedCallback;
    
    //This is where logic for setting up annotations found from the server should go
    protected override void OnRealtimeModelReplaced(ObjectViewModel previousModel, ObjectViewModel currentModel)
    {
        
        base.OnRealtimeModelReplaced(previousModel, currentModel);
        if (previousModel != null)
        {
            previousModel.annotations.modelAdded -= _addCallback;
            previousModel.annotations.modelRemoved -= _removedCallback;
            
            //Destroy all registered annotation managers
            foreach (var pair in _AnnotationRegistry)
            {
                //This may or may not cause an issue, as OnDestroy in the AnnotationManager will attempt to remove its model from the old ObjectViewModel...
                Destroy(pair.Value);
                _AnnotationRegistry.Remove(pair.Key);
            }
        }

        //If we implement persistence, this will reinstantiate all of the saved annotations in the model
        foreach (AnnotationModel newModel in this.model.annotations)
        {
            //Leaving remote parameter false for now
            DisplayAnnotation(this.model.annotations, newModel, false);
        }
        
        //Finally, subscribe to events so that any models that are added after this get properly displayed as well
        this.model.annotations.modelAdded += _addCallback;
        this.model.annotations.modelRemoved += _removedCallback;
    }

    private void OnEnable()
    {
        _addCallback = DisplayAnnotation;
        
        //TODO: Figure out if we need to add any other logic to the model removed callback, as this is our final chance to perform action when an annotation is properly destroyed
        _removedCallback = (set, annotationModel, remote) => { _AnnotationRegistry.Remove(annotationModel); };
        
        _annotationPrefab = Resources.Load<GameObject>("Prefabs/AnnotationPrefab");
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
    private void DisplayAnnotation(RealtimeSet<AnnotationModel> set, AnnotationModel annotationModel, bool remote)
    {
        Debug.Log("inserting?");
        try
        {
            _AnnotationRegistry.Add(annotationModel, Instantiate(_annotationPrefab).GetComponent<AnnotationManager>());
        }
        catch (ArgumentException exception)
        {
            //If we made it here, that means that we were not able to register the new annotation (most likely we tried to add the same model twice, which should never really happen so...
            Debug.Log(
                "Something has gone horribly wrong: Attempt to add AnnotationModel to registry when it already exists");
        }

        
        _AnnotationRegistry.TryGetValue(annotationModel, out _newAnnotationManager);
        //We could eventually be giving each piece of a 3d Model its own ObjectManager, in which case this becomes important. Probably should consider the performance issues of such a system though...
        _newAnnotationManager.ObjectManager = this;
        _newAnnotationManager.AnnotationReference = annotationModel;
        annotationModel.annotationLocationDidChange += _newAnnotationManager.UpdateAnnotationLocation;
        annotationModel.annotationTextDidChange += _newAnnotationManager.UpdateAnnotationText;

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
    
    
}
