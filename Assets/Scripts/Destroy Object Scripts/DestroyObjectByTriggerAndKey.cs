using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class DestroyObjectByTriggerAndKey : MonoBehaviour
{
    [SerializeField, Tooltip("The object you want to destroy while I am collide")]
    private GameObject destroyObjects;

    [SerializeField, Tooltip("The object I need to collide with for the object above will be destroyed")]
    private GameObject destroyTriggerObject;

    [SerializeField, Tooltip("The key you need to press to destroy the object (while I am collide with the object above)")]
    private InputAction destroyKey;

    private void OnEnable()
    {
        destroyKey.Enable();
    }

    private void OnDisable()
    {
        destroyKey.Disable();
    }

    private void OnValidate()
    {
        if (destroyKey == null)
            destroyKey = new InputAction(type: InputActionType.Button);
        if (destroyKey.bindings.Count == 0)
            destroyKey.AddBinding("<Keyboard>/F");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == destroyTriggerObject)
        {
            if (destroyKey.triggered)
            {
                if (destroyObjects != null)
                {
                    destroyObjects.SetActive(false);
                    Debug.Log(destroyObjects.name + " destroyed by collision with " + other.gameObject.name);
                }
                else
                {
                    Debug.LogWarning("Destroy object is not assigned!");
                }
            }
        }
    }

    private void Update()
    {
        if (destroyKey.WasPressedThisFrame())
        {
            Debug.Log("Key was pressed!");
        }
    }

}
