using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class DeleteObjectByCollisionWithTag : MonoBehaviour
{
    [SerializeField, Tooltip("The object will be destroy when collides with this tag")]
    string[] destroyObjectTags;

    void OnCollisionEnter(Collision collision)
    {
        if (destroyObjectTags.Length == 0) return;
         if (destroyObjectTags.Contains(collision.gameObject.tag))
        {
            actOnCollision(collision.gameObject);
        }
    }
    private void actOnCollision(GameObject collision)
    {
        Destroy(gameObject);
        Debug.Log(gameObject.name + " destroyed by collision with " + collision.name);
    }
}
