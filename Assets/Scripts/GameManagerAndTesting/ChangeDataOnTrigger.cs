using UnityEngine;
using System.Linq;
using TMPro;

public class ChangeDataOnTrigger : MonoBehaviour
{
    [SerializeField, Tooltip("Drag the TMP_Text component from your Canvas here.")]
    private TMP_Text textComponent;
    [SerializeField, Tooltip("How much to add to the data? (If you want to reduce - add a minus before the number)")]
    private float DataToAdd = 1;
    [SerializeField, Tooltip("The tags that will trigger the text change")]
    private string[] ObjectTags;

    private void Change()
    {
        if (float.TryParse(textComponent.text, out float currentData))
        {
            currentData += DataToAdd;
            textComponent.text = currentData.ToString();
        }
        else
        {
            Debug.LogWarning("Failed to parse text to a number!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (textComponent == null || ObjectTags.Length == 0)
        {
            Debug.LogWarning("In the inspector: TMP_Text component is not assigned or there are no tags in the array.");
        }
        else
        {
            if (ObjectTags.Contains(other.gameObject.tag))
            {
                Change();
                Debug.Log($"Text updated: Triggered by {other.gameObject.name}");
            }
        }
    }
}
