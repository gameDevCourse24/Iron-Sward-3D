using UnityEngine;
using TMPro;

public class ChangeDataOnCollision : MonoBehaviour
{
    [SerializeField, Tooltip("Drag the TMP_Text component from your Canvas here.")]
    private TMP_Text textComponent;
    [SerializeField, Tooltip("How much to add to the data? (If you want to reduce - add a minus before the number)")]
    private float DataToAdd = 1;

    // פונקציה שממירה את הטקסט למספר, מוסיפה את הנתון ומעדכנת את הטקסט
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

    private void OnCollisionEnter(Collision collision)
    {
        if (textComponent != null)
        {
            Change();
            Debug.Log($"Text updated: Collided with {collision.gameObject.name}");
        }
        else
        {
            Debug.LogWarning("TMP_Text component is not assigned!");
        }
    }
}
