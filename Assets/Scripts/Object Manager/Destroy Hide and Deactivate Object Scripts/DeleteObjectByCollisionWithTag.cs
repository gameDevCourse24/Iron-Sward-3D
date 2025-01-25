using UnityEngine;
using System.Linq;

// This script is used to destroy the object when it collides with an object with a specific tag.

[RequireComponent(typeof(Collider))]
public class DeleteObjectByCollisionWithTag : MonoBehaviour
{
    [SerializeField, Tooltip("The object will be destroy when collides with this tag")]
    string[] destroyObjectTags;

    GameObject objectToDestroy;
    void OnCollisionEnter(Collision collision)
    {
        if (destroyObjectTags.Length == 0) return;
        if (IsTargetOrChild(collision.gameObject))
        {
            actOnCollision(objectToDestroy);
        }
    }
    private void actOnCollision(GameObject gameObject)
    {
        Destroy(gameObject);
        Debug.Log(gameObject.name + " destroyed by collision with " + gameObject.name);
    }
    bool IsTargetOrChild(GameObject hitObject)
    {
        Transform current = hitObject.transform;

        while (current != null)
        {
            if (destroyObjectTags.Contains(hitObject.tag)) 
            {
                objectToDestroy = current.gameObject; // כדי שנוכל למחוק את האובייקט אב ולא את האובייקט בן (כמו הדמות וכו)
                return true; // אם זה המטרה - החזר true
            }
            current = current.parent; // המשך לבדוק כלפי מעלה בעץ האובייקטים
        }
        return false;
    }
}
