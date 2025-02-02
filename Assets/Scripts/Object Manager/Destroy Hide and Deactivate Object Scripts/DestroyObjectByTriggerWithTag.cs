using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Collider))]

// This script is used to destroy this object when it is triggered by an other object with a specific tag.
public class DestroyObjectByTriggerWithTag : MonoBehaviour
{
    [SerializeField, Tooltip("The object will be destroyed when triggered by an object with one of these tags.")]
    private string[] destroyObjectTags;

    private void OnTriggerEnter(Collider other)
    {
        pprint.p($"{gameObject.name} detected trigger with {other.gameObject.name} (Tag: {other.gameObject.tag})", this);

        if (destroyObjectTags == null || destroyObjectTags.Length == 0) return;

        if (destroyObjectTags.Contains(other.gameObject.tag))
        {
            ActOnCollision(other.gameObject);
        }
    }

    private void ActOnCollision(GameObject otherObject)
    {
        pprint.p($"{gameObject.name} destroyed because it was triggered by {otherObject.name}", this);
        Destroy(gameObject);  // משמיד את האובייקט שעליו הסקריפט נמצא
    }
}
