using Unity.VisualScripting;
using UnityEngine;
// Base virtual class for controlling objects in the scene. 
// Handles activation of objects upon collisions.


public abstract class ObjectController : MonoBehaviour
{
    [Header("Collision Settings")]
    [SerializeField, Tooltip("Who should collide?")]
    protected  GameObject triggeringObject;

    [SerializeField, Tooltip("Do you want to activate by triggering Object tag instead by the object itself?")]
    protected  bool byTag = false;

    [SerializeField, Tooltip("The object to be activated")] 
    protected  GameObject objectToActivate;

    public virtual void Activate()
    {
        if (objectToActivate != null )
        {
            if (objectToActivate.activeSelf)
            {
                objectToActivate.SetActive(false);
            }
            objectToActivate.SetActive(true);
            pprint.p($"{objectToActivate.name} is now active.", this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // pprint.p(other.gameObject.name + " enter and activate trigger, the triggering object is " + triggeringObject.name, this);
        if (canActivate(other.gameObject))
        {
            Activate();
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        // pprint.p(other.gameObject.name + " enter and activate collision, the triggering object is " + triggeringObject.name, this);
        if (canActivate(other.gameObject))
        {
            Activate();
        }
    }
    private bool canActivate(GameObject other)
    {
        if (byTag)
        {
            if (other.CompareTag(triggeringObject.tag))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return other == triggeringObject;
        }
    }
}
