using UnityEngine;
using UnityEngine.InputSystem;
//This script is used to destroy the object 'destroyObjects'  when the object 'destroyTriggerObject' is collided with this collider and the key 'destroyKey' is pressed.
//If 'destroyThisColliderAlso' is true, then this collider also will be destroyed.
[RequireComponent(typeof(Collider))]
public class DestroyObjectByTriggerAndKey : MonoBehaviour
{
    [SerializeField, Tooltip("The object to destroy while I am collide with the object below")]
    private GameObject destroyObjects;

    [SerializeField, Tooltip("The object I need to collide with for the object above will be destroyed")]
    private GameObject destroyTriggerObject;

    [SerializeField, Tooltip("The key you need to press to destroy the object (while I am collide with the object above)")]
    private InputAction destroyKey;

    [SerializeField, Tooltip("Do you want to destroy this collider after the object above is destroyed?")]
    private bool destroyThisColliderAlso = false;

    private bool canDestroyTheObject = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (destroyTriggerObject && destroyObjects){
            if (other.gameObject == destroyTriggerObject)
            {
                Debug.Log(other.gameObject.name + "Entered" + name + ", now you can destroy " + destroyObjects.name);
                canDestroyTheObject = true;
            }
            else
            {
                Debug.Log(other.gameObject.name + " Enterd to " + name + ", but not " + destroyTriggerObject.name + " so you can't destroy " + destroyObjects.name);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (destroyTriggerObject && destroyObjects){
            if (other.gameObject == destroyTriggerObject)
            {
                Debug.Log(other.gameObject + "Exit from " + other.gameObject.name + ", now you can't destroy " + destroyObjects.name);
                canDestroyTheObject = false;
            }
        }
    }

    private void Update()
    {
        if (destroyTriggerObject && destroyObjects){
            if (destroyKey.WasPressedThisFrame() && canDestroyTheObject)
            {
                Destroy(destroyObjects);
                Debug.Log("Destroying object: " + destroyObjects.name);
                if (destroyThisColliderAlso)
                {
                    Debug.Log("Destroying: " + name);
                    Destroy(gameObject);
                }
            }
        }
        
    }

}
