using UnityEngine;
using UnityEngine.InputSystem;

// A class that inherits from TemporaryObject. 
// The object will deactivate when any key is pressed.

public class AnyKeyObject : TemporaryObject
{
    [SerializeField] private InputAction anyKeyAction;

    private void OnEnable()
    {
        // Initialize the input action to listen for any key press
        anyKeyAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/anyKey");
        anyKeyAction.Enable();
    }

    private void OnDisable()
    {
        anyKeyAction.Disable();
    }

    private void Update()
    {
        if (anyKeyAction.WasPressedThisFrame())
        {
            Deactivate();
        }
    }
}
