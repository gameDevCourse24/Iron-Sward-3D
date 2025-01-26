using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem.Interactions;

// This script is used to destroy the other object (with specific tag) when it collides with this object.

[RequireComponent(typeof(Collider))]
public class DeleteOtherObjectWithTagByCollision : MonoBehaviour
{
    [SerializeField, Tooltip("The object will be destroy when collides with this tag")]
    string[] destroyObjectTags;
    public bool printMessage = true;

    GameObject objectToDestroy;
    void OnCollisionEnter(Collision other)
    {
        if (destroyObjectTags.Length == 0) return;
        if (IsTargetOrChild(other.gameObject))
        {
            actOnCollision(objectToDestroy);
        }
    }
    private void actOnCollision(GameObject otherObject)
    {
        Destroy(otherObject);
        pprint.p(otherObject.name + " destroyed by collision with " + name, this);
    }
    bool IsTargetOrChild(GameObject hitObject)
    {
        Transform current = hitObject.transform;

        while (current != null)
        {
            if (destroyObjectTags.Contains(hitObject.tag)) 
            {
                objectToDestroy = current.gameObject; // כדי שנוכל למחוק את האובייקט אב ולא את האובייקט בן (כמו הדמות וכו)
                pprint.p(objectToDestroy.name, this);
                return true; // אם זה המטרה - החזר true
            }
            current = current.parent; // המשך לבדוק כלפי מעלה בעץ האובייקטים
        }
        return false;
    }

}
