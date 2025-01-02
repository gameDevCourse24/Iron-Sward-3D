using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This component lets the player show/hide the cursor by clicking ESC.
 * Initially, it hides the cursor.
 */
public class CursorHider : MonoBehaviour {
    [SerializeField] InputAction toggleCursorAction;

    void OnEnable() { toggleCursorAction.Enable(); }
    void OnDisable() { toggleCursorAction.Disable(); }
    void OnValidate() {
        // Provide default bindings for the input actions.
        // Based on answer by DMGregory: https://gamedev.stackexchange.com/a/205345/18261
        if (toggleCursorAction == null)
            toggleCursorAction = new InputAction(type: InputActionType.Button);
        if (toggleCursorAction.bindings.Count == 0)
            toggleCursorAction.AddBinding("<Keyboard>/escape");
    }


    void Start() {
        HideCursor();
    }

    void Update() {
        if (toggleCursorAction.WasPerformedThisFrame()) {
            if (!Cursor.visible) {
                ShowCursor();
            } else {
                HideCursor();
            }
        }
    }
    private void ShowCursor() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void HideCursor() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
