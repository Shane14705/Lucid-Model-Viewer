using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/*Notes: Try idea of having two different action maps (ui which registers click on finger lift up or mouse button release)
And Model_Interaction map which tracks delta of finger or mouse and rotates object. Then have a default map which detects click down and touch, 
and if the click or touch screen ray hits a ui element it switches to ui action map, otherwise it switches to model interaction. Just dont forget to switch back!
*/

//For pinch input, detect on start of multi touch and run until the event ends, keeping track of the change from original positions
public class InputManager : MonoBehaviour
{
    private Controls ControlBindings;

    
    //I made custom events for every binding, so that I can perform custom input processing if necessary (such as with pinch/zoom)
    #region Event/Delegate Declarations
    public delegate void RotationInput(InputAction.CallbackContext ctx);
    public static event RotationInput OnRotInput;

    public delegate void ScaleInput(float targetScalar);
    public static event ScaleInput OnScalingInput;

    public delegate void AnnotationInput(InputAction.CallbackContext ctx);
    public static event AnnotationInput OnAddAnnotation;
    #endregion
    
    //TODO: Setup events and delegates for UI input actions too, and decide where their logic should be handled (need to learn more about Unity UI first)    
    
    
    private void Awake()
    {
        ControlBindings = new Controls();
    }

    private void OnEnable()
    {
         ControlBindings.Enable();
         //Bind all events
         ControlBindings.Model_Interaction.Disable();
         ControlBindings.UI.Disable();
         ControlBindings.Default.Enable();

         ControlBindings.Default.Interact.started += MapInputs;
         ControlBindings.Default.Interact.canceled += MapDefaults;
         
         //Does doing things with these lambdas (and therefore trying to unsubscribe with lambdas) cause memory leakage?
         ControlBindings.Model_Interaction.RotateObject.performed += ctx =>
         {
             if (OnRotInput != null) OnRotInput(ctx);
         };
         
         ControlBindings.Model_Interaction.AnnotateObject.performed += ctx =>
         {
             if (OnAddAnnotation != null) OnAddAnnotation(ctx);
         };

         //Handle logic for pinch/zoom in scale input function and we can just pass that to our subscribers.
         ControlBindings.Model_Interaction.ScaleObject.performed += HandlePinchStretch;
         ControlBindings.Model_Interaction.ScaleObject.started += HandlePinchStretch;
         ControlBindings.Model_Interaction.ScaleObject.canceled += HandlePinchStretch;

    }

    //Responsible for turning multitouch input into a scalar value showing how pinched or stretched the fingers are, which can then be passed to subscribers
    //Method is called on action start, performed, and canceled so that it can measure the distance pinched/stretched
    private void HandlePinchStretch(InputAction.CallbackContext ctx)
    {
        float stretchValue = 0f;
        
        //TODO: FIGURE OUT HOW TO HANDLE PINCH/ZOOM
        
        if (OnScalingInput != null) OnScalingInput(stretchValue);

    }

    //When we are done interacting with either the UI or Object, disable their controls
    private void MapDefaults(InputAction.CallbackContext ctx)
    {
        ControlBindings.UI.Disable();
        ControlBindings.Model_Interaction.Disable();
        
    }

    //When we begin interacting, decide whether the model should be enabled or the UI controls should be enabled
    //NOTE: Objects that are interacted with as part of the model must be tagged "Model", and UI elements must be tagged "UI"
    private void MapInputs(InputAction.CallbackContext ctx)
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(ctx.ReadValue<Vector2>());

        RaycastHit interactionCast = new RaycastHit();
        Physics.Raycast(interactionRay, out interactionCast, 1000f);

        if (interactionCast.collider.CompareTag("Model"))
        {
            ControlBindings.UI.Disable();
            ControlBindings.Model_Interaction.Enable();
        }
        
        else if (interactionCast.collider.CompareTag("UI"))
        {
            ControlBindings.Model_Interaction.Disable();
            ControlBindings.UI.Enable();
        }
        
    }

    private void OnDisable()
    {
        ControlBindings.Disable();
        //Unbind all events
        
        ControlBindings.Default.Interact.started -= MapInputs;
        ControlBindings.Default.Interact.canceled -= MapDefaults;
        
        ControlBindings.Model_Interaction.ScaleObject.performed -= HandlePinchStretch;
        ControlBindings.Model_Interaction.ScaleObject.started -= HandlePinchStretch;
        ControlBindings.Model_Interaction.ScaleObject.canceled -= HandlePinchStretch;
    }

    // Start is called before the first frame update
    void Start()
    {
        ControlBindings.Model_Interaction.RotateObject.performed += ctx => LogDrags(ctx);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LogDrags(InputAction.CallbackContext ctx)
    {
        //For some reason this is only giving values up to (1,1). It is also firing the event every time the mouse is moved
        //Even tho it correctly does not give any actual drag value unless the mouse button is held down.
        Debug.Log(ctx.ReadValue<Vector2>());
    }
}
