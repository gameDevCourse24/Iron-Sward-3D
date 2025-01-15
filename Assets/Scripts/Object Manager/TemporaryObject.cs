using UnityEngine;
// An abstract class for temporary objects that will deactivate under certain conditions. 
// Includes methods for both activation and deactivation of objects, with optional game pausing.

public abstract class TemporaryObject : ObjectController
{
    [Header("Game Control")]
    [SerializeField] private bool pauseGameOnActivate = false;

    public override void Activate()
    {
        base.Activate();

        if (pauseGameOnActivate)
        {
            Time.timeScale = 0f;
            Debug.Log("Game paused.");
        }
    }

    public virtual void Deactivate()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
            Debug.Log($"{objectToActivate.name} is now inactive.");

            if (pauseGameOnActivate)
            {
                Time.timeScale = 1f;
                Debug.Log("Game resumed.");
            }
        }
    }
}
