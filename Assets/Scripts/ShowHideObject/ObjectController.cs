using Unity.VisualScripting;
using UnityEngine;
// Base virtual class for controlling objects in the scene. 
// Handles activation of objects upon collisions.


public abstract class ObjectController : MonoBehaviour
{
    [Header("Collision Settings")]
    [SerializeField, Tooltip("Who should collide?")]
    protected  GameObject triggeringObject;

    [SerializeField, Tooltip("The object to be activated")] 
    protected  GameObject objectToActivate;

    public virtual void Activate()
    {
        if (objectToActivate != null )
        {
            objectToActivate.SetActive(true);
            Debug.Log($"{objectToActivate.name} is now active.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == triggeringObject)
        {
            Activate();
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == triggeringObject)
        {
            Activate();
        }
    }
}
