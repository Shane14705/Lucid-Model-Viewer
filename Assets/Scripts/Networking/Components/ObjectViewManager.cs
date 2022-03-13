using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class ObjectViewManager : RealtimeComponent<ObjectViewModel>
{
    //When we Instantiate an annotation prefab, use this variable to set the prefab's script values (ex: DictKey, model reference, etc)
    private AnnotationManager _newAnnotation;
    
    //Do I need to worry about this kind of thing for AnnotationModels that are added/removed to the collection, or is this just the equivalent of what I need to do when the Add or Remove callbacks are made?
    protected override void OnRealtimeModelReplaced(ObjectViewModel previousModel, ObjectViewModel currentModel)
    {
        base.OnRealtimeModelReplaced(previousModel, currentModel);
    }

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
