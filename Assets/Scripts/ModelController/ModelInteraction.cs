using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

/*NOTE: There is not really a good purpose for just allowing the teacher to rotate the object freely and having it rotate on student devices as well,
 since in AR most students will have different camera angles. Therefore, focus on making it so the teacher can get all students to see the same position if they choose
 (rotate object individually so that its respective camera is pointing at the target position on the object).
 Possibly do something where each student has ownership of their object (if possible?) until teacher takes control.
 
 Big question: It looks like really all we need are RPC type events/commands, however something like normcore isnt made for that, 
 and mirror has much more overhead cost as it must run a headless instance of Unity on the server (since school networks wont be super friendly to port forwarding I assume).
 */
[RequireComponent(typeof(Rigidbody))]
public class ModelInteraction : MonoBehaviour
{
    //Possibly make the Scene Manager a singleton later on to simplify this?

    //Set to the scene's input manager
    [SerializeField] private InputManager InputBindings;

    private Rigidbody rb;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        rb = this.GetComponent<Rigidbody>();
        //TODO: BIND TO ROTATION INPUT AND START WORKING ON TEST CODE FOR ROTATING AN OBJECT
        InputBindings.OnRotInput += HandleRotation;
        InputBindings.OnScalingInput += HandleScaling;
        InputBindings.OnAddAnnotation += PlaceAnnotation;
    }

    private void OnDisable()
    {
        InputBindings.OnRotInput -= HandleRotation;
        InputBindings.OnScalingInput -= HandleScaling;
        InputBindings.OnAddAnnotation -= PlaceAnnotation;
    }

    private void HandleRotation(InputAction.CallbackContext ctx)
    {
        Debug.Log("handling rotation in model interaction!");
        //Should I move this to a member level variable so that it isnt created and destroyed rapidly in the event loop?
        Vector2 deltaMovement = ctx.ReadValue<Vector2>();
        
        //z should be diagonal rotation?
        Vector3 target = new Vector3(deltaMovement.x, deltaMovement.y, (deltaMovement.x / deltaMovement.y));
        
        /*TODO: I believe what I need to do is take the direction we are swiping, get it flat on the same plane that the camera is on (eg a swipe up gets a vector pointing straight out from the camera forward)
         Then we just add angular velocity in that direction based on the magnitude of the swipe (Probably something with Dot products and unit vectors hmm)
        */
        
        rb.AddTorque(target, ForceMode.Acceleration);
    }

    private void HandleScaling(float scalingTarget)
    {
        Debug.Log("Scaling value: " + scalingTarget);
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
