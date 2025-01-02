using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DestroyObjectByCollision : MonoBehaviour
{
    [SerializeField, Tooltip("The object will be destroy when it collides with this objects")]
    GameObject[] destroyObjects;
    void OnCollisionEnter(Collision collision)
    {
        foreach (GameObject destroyObject in destroyObjects)
        {
            if (collision.gameObject == destroyObject)
            {
                Destroy(gameObject);
                Debug.Log(gameObject.name + " destroy by collision with " + collision.gameObject.name);
            }
        }
    }
}
