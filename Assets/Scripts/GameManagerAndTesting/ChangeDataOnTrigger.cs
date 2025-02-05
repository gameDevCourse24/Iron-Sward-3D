using UnityEngine;
using System.Linq;
using TMPro;

public class ChangeDataOnTrigger : MonoBehaviour
{
    [SerializeField, Tooltip("Drag the TMP_Text component from your Canvas here.")]
    private TMP_Text textComponent;
    [SerializeField, Tooltip("How much to add to the data? (If you want to reduce - add a minus before the number)")]
    private float DataToAdd = 1;
    public bool thereIsALimit = false;
    public float limit = 100;
    [SerializeField, Tooltip("The tags that will trigger the text change")]
    private string[] ObjectTags;

    private void Change()
    {
        if (float.TryParse(textComponent.text, out float currentData))
        {
            currentData += DataToAdd;
            if (thereIsALimit && currentData > limit)
            {
                currentData = limit;
            }
            textComponent.text = currentData.ToString();
        }
        else
        {
            pprint.p("Failed to parse text to a number!", this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (textComponent == null || ObjectTags.Length == 0)
        {
            pprint.p("In the inspector: TMP_Text component is not assigned or there are no tags in the array.", this);
        }
        else
        {
            if (ObjectTags.Contains(other.gameObject.tag))
            {
                Change();
                pprint.p($"Text updated: Triggered by {other.gameObject.name}", this);
            }
        }
    }
}
