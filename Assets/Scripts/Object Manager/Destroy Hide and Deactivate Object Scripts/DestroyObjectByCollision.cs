using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class DestroyObjectByCollision : MonoBehaviour
{
    [SerializeField, Tooltip("The object will be destroyd when it collides with this objects")]
    GameObject[] destroyObjects;

    [SerializeField, Tooltip("The object will be destroy when it collides with object with this name")]
    string[] destroyObjectNames;
    void OnCollisionEnter(Collision collision)
    {
        if (destroyObjectNames.Contains(collision.gameObject.name) || destroyObjects.Contains(collision.gameObject))
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
