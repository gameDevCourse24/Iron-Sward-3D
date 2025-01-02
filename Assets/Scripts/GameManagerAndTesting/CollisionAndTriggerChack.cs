using UnityEngine;

public class CollisionAndTriggerChack : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name + "Collision with " + collision.gameObject.name);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + "Trigger with " + other.gameObject.name);
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(gameObject.name + "Exit Collision with " + collision.gameObject.name);
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(gameObject.name + "Exit Trigger with " + other.gameObject.name);
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(gameObject.name + "Stay Collision with " + collision.gameObject.name);
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(gameObject.name + "Stay Trigger with " + other.gameObject.name);
    }
}
