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

    [Header("Optional")]

    [SerializeField, Tooltip("If you want to show the data chenge, drag the TMP_Text component here.")]
    private TMP_Text UIelementToShow = null;
    [SerializeField, Tooltip("How many seconds to display the panel?")]
    private float panelDisplayTime = 1f;
    [SerializeField, Tooltip("If you want to limit the data, set this to true and set the limit.")]
    private bool thereIsALimit = false;
     [SerializeField, Tooltip("what is the limit?")]
    private float limit = 100;
    

    private void Change()
    {
        if (float.TryParse(textComponent.text, out float currentData))
        {
            if (UIelementToShow != null)
            {
                UIelementToShow.text = "+" + DataToAdd.ToString();
                UIelementToShow.gameObject.SetActive(true);
                Invoke("HidePanel", panelDisplayTime);
            }
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
    private void HidePanel()
    {
        UIelementToShow.gameObject.SetActive(false);
    }
}
