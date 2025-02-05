using UnityEngine;
using TMPro;
// This script is attached to a number of GameObject with a TextMeshPro component.
public class SearchForTagAndUpdateTMP : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectToSearchForByIsTag;
    [SerializeField] private TMP_Text textComponent;
    private int numberOfObjectsFound;
    private void Update()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(gameObjectToSearchForByIsTag.tag);
        numberOfObjectsFound = gameObjects.Length;
        textComponent.text = numberOfObjectsFound.ToString();
    }
    public int GetNumberOfObjectsFound()
    {
        return numberOfObjectsFound;
    }
}
