using UnityEngine;
// An abstract class for temporary objects that will deactivate under certain conditions. 
// Includes methods for both activation and deactivation of objects, with optional game pausing.

public abstract class TemporaryObject : ObjectController
{
    [SerializeField, Tooltip("Do you want to delete this object on collition exit?")] private bool deleteAfter = false;
    [Header("Game Control")]
    [SerializeField] private bool pauseGameOnActivate = false;

    public override void Activate()
    {
        base.Activate();

        if (pauseGameOnActivate)
        {
            Time.timeScale = 0f;
            pprint.p("Game paused.", this);
        }
    }

    public virtual void Deactivate()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
            pprint.p($"{objectToActivate.name} is now inactive.", this);

            if (pauseGameOnActivate)
            {
                Time.timeScale = 1f;
                pprint.p("Game resumed.", this);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (deleteAfter && other.gameObject == triggeringObject)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (deleteAfter && other.gameObject == triggeringObject)
        {
            Destroy(gameObject);
        }
    }
}
