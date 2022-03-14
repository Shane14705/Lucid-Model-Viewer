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
        
    }

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        //TODO: MAKE SURE THAT WE UNSUBSCRIBE FROM ALL EVENTS ON OUR MODEL BEFORE WE TELL THE OBJECTMANAGER TO REMOVE THE MODEL FROM THE DATASTORE
        //How will I be getting a reference to our model? Should I do it through the DataStore?
        
        //TODO: something to the effect of taking our reference to the model, unsubscribing our events from it, and then passing our ID up to the ObjectViewManager to remove us from the DataStore
        throw new NotImplementedException();
    }
}
