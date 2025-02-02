using UnityEngine;
using UnityEngine.InputSystem;
// A class that inherits from TemporaryObject. 
// The object will deactivate when a specific key is pressed.
// The key can be customized in the inspector.

public class DeactivateBySpecificKeyObject : TemporaryObject
{
    [SerializeField, Tooltip("The key that Deactivate the object")] InputAction keyToDeactivate;
     

    private void OnEnable()
    {
        keyToDeactivate.Enable();
    }
    private void OnDisable()
    {
        keyToDeactivate.Disable();
    }
    
     void OnValidate()
    {
        if (keyToDeactivate == null)
            keyToDeactivate = new InputAction(type: InputActionType.Button);
        if (keyToDeactivate.bindings.Count == 0)
            keyToDeactivate.AddBinding("<Keyboard>/I");
    }

    private void Update()
    {
        if (keyToDeactivate.triggered)
        {
            Deactivate();
        }
    }
}
