using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Controls ControlBindings;

    private void Awake()
    {
        ControlBindings = new Controls();
    }

    private void OnEnable()
    {
         ControlBindings.Enable();
    }

    private void OnDisable()
    {
        ControlBindings.Disable();
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
