using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*NOTE: There is not really a good purpose for just allowing the teacher to rotate the object freely and having it rotate on student devices as well,
 since in AR most students will have different camera angles. Therefore, focus on making it so the teacher can get all students to see the same position if they choose
 (rotate object individually so that its respective camera is pointing at the target position on the object).
 Possibly do something where each student has ownership of their object (if possible?) until teacher takes control.
 
 Big question: It looks like really all we need are RPC type events/commands, however something like normcore isnt made for that, 
 and mirror has much more overhead cost as it must run a headless instance of Unity on the server (since school networks wont be super friendly to port forwarding I assume).
 */
public class ModelInteraction : MonoBehaviour
{
    //Possibly make the Scene Manager a singleton later on to simplify this?

    //Set to the scene's input manager
    [SerializeField] private InputManager InputBindings;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        //TODO: BIND TO ROTATION INPUT AND START WORKING ON TEST CODE FOR ROTATING AN OBJECT
        InputBindings.OnRotInput += HandleRotation;
        InputBindings.OnScalingInput += HandleScaling;
        InputBindings.OnAddAnnotation += PlaceAnnotation;
    }

    private void OnDisable()
    {
        InputBindings.OnRotInput -= HandleRotation;
    }

    private void HandleRotation(InputAction.CallbackContext ctx)
    {
        throw new NotImplementedException();
    }

    private void HandleScaling(float scalingTarget)
    {
        throw new NotImplementedException();
    }

    //TODO: For now, consider making it so double tapping on a part of the model during a multiplayer session causes the model to turn so that the selected position is facing the camera of every user
    private void PlaceAnnotation(InputAction.CallbackContext ctx)
    {
        Debug.Log("Placeholder log until annotation system is in place");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
