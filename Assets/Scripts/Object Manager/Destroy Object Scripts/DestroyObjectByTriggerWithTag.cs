using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class DeleteObjectByTriggerWithTag : MonoBehaviour
{
    [SerializeField, Tooltip("The object will be destroyed when triggered by an object with one of these tags.")]
    private string[] destroyObjectTags;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} detected trigger with {other.gameObject.name} (Tag: {other.gameObject.tag})");

        if (destroyObjectTags == null || destroyObjectTags.Length == 0) return;

        if (destroyObjectTags.Contains(other.gameObject.tag))
        {
            ActOnCollision(other.gameObject);
        }
    }

    private void ActOnCollision(GameObject otherObject)
    {
        Debug.Log($"{gameObject.name} destroyed because it was triggered by {otherObject.name}");
        Destroy(gameObject);  // משמיד את האובייקט שעליו הסקריפט נמצא
    }
}
