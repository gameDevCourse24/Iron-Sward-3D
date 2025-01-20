using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Transport : MonoBehaviour
{
    [SerializeField, Tooltip("The object you want to transfer")]
    private GameObject objectToTransfer;

    [SerializeField, Tooltip("The object that will trigger the transfer")]
    private GameObject triggeringObject;

    [SerializeField, Tooltip("The target location for the object")]
    private Transform receivingObject;

    private void Start()
    {
        if (objectToTransfer == null || triggeringObject == null || receivingObject == null)
        {
            Debug.LogError("Transport: Missing references!", this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} detected trigger with {other.gameObject.name}");
        

        if (other.gameObject.CompareTag(triggeringObject.tag)) // השוואה לפי תג
        {
            if (objectToTransfer != null && receivingObject != null)
            {
                // Check if the object to transfer has a CharacterController because we need to disable it to move it with 'transform.position'.
                CharacterController controller = objectToTransfer.GetComponent<CharacterController>();
                if (controller != null)
                {
                    controller.enabled = false; // Disable the controller to move the object with 'transform.position'.
                    controller.transform.position = receivingObject.position;
                    controller.enabled = true; // Enable the controller back.
                }
                Debug.Log($"{objectToTransfer.name} was teleported to {receivingObject.position}");
            }
            else
            {
                Debug.LogError("Transport: Missing objectToTransfer or receivingObject!", this);
            }
        }
        else
        {
            Debug.Log($"{other.name} does not match {triggeringObject.name}");
        }
    }
}
