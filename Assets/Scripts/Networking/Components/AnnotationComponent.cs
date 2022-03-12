using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

//TODO: The main question: Do I instantiate individual Annotation prefabs with these annotation components, or do I create one realtime collection and let a manager component control the display and hiding of them?

//This should contain the code for properly displaying an annotation in a scene, it will be placed on the annotation prefabs which are instantiated by the sceneManager.
public class AnnotationComponent : RealtimeComponent<AnnotationModel>
{
    
    /*This script will create an Annotation model in the datastore for each time it is instantiated
    * so we need to make sure we have some logic to add it to the RealtimeDictionary in the SceneStateModel (likely through SceneStateComponent)
    */
    
    // Start is called before the first frame update
    void Awake()
    {
        

    }

    protected override void OnRealtimeModelReplaced(AnnotationModel previousModel, AnnotationModel currentModel)
    {
        base.OnRealtimeModelReplaced(previousModel, currentModel);
        
        //Unsubscribe from any events on the previous model if there is one
        if (previousModel != null)
        {
            previousModel.annotationLocationDidChange -= OnAnnotationLocationChanged;
            previousModel.annotationTextDidChange -= OnAnnotationTextChanged;
        }

        if (currentModel == null)
        {
            Debug.Log("AnnotationComponent: Something has gone wrong! OnRealtimeModelReplaced was called without giving a replacement model!");
            
        }
        else
        {
            currentModel.annotationLocationDidChange += OnAnnotationLocationChanged;
            currentModel.annotationTextDidChange += OnAnnotationTextChanged;
        }
        
    }

    private void OnAnnotationTextChanged(AnnotationModel annotationModel, string value)
    {
        //Placeholder Code until I begin UI work
        Debug.Log("Annotation text updated: " + this.model.annotationText);
    }

    private void OnAnnotationLocationChanged(AnnotationModel annotationModel, Vector3 value)
    {
        //Placeholder code until I begin UI work
        Debug.Log("Annotation location updated: " + this.model.annotationLocation);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
