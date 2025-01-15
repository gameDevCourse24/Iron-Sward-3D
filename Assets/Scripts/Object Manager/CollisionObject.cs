using Unity.VisualScripting;
using UnityEngine;
// A class that inherits from TemporaryObject. 
// The object will deactivate after colliding with a specified target object.

public class CollisionObject : TemporaryObject
{
    [SerializeField] private GameObject collisionTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == collisionTarget)
        {
            Deactivate();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == collisionTarget)
        {
            Deactivate();
        }
    }
}
