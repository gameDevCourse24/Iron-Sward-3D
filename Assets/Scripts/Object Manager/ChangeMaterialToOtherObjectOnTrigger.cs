using UnityEngine;

public class ChangeMaterialToOtherObjectOnTrigger : MonoBehaviour
{
    [SerializeField, Tooltip("I want to change the material of the object (insert the object here):")]
        private GameObject objectToChangeMaterial;

    [SerializeField, Tooltip("To this material (Insert the new material for the object above):")]
        private Material newMaterial;

    [SerializeField, Tooltip("When the following object collides with me (insert the object that will trigger the action):")]
        private GameObject objectToTrigger;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objectToTrigger)
        {
            objectToChangeMaterial.GetComponent<Renderer>().material = newMaterial;
        }
    }
}
