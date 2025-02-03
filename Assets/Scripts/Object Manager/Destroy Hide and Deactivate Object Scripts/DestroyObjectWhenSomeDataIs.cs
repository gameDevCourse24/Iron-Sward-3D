using UnityEngine;
using TMPro;
public class DestroyObjectWhenSomeDataIs : MonoBehaviour
{
    [SerializeField, Tooltip("The object to destroy.")]
    private GameObject objectToDestroy;

    [SerializeField, Tooltip("Destroy this object when this data")]
    private TMP_Text textComponent;

    private enum CompareType
    {
        LessThan,
        LessThanOrEqual,
        Equal,
        GreaterThan,
        GreaterThanOrEqual
    }

    [SerializeField, Tooltip("Is: <, <=, =, >, or >=")]
    private CompareType compareType;

    [SerializeField, Tooltip("The number to compare to.")]
    private float numberToCompare;

    private float stringToFloatHolder;
    private void Start()
    {
        if (textComponent == null)
        {
            pprint.p("Text component is not assigned in the inspector!");
        }
        float.TryParse(textComponent.text, out stringToFloatHolder);
    }

    private void Update()
    {
        float.TryParse(textComponent.text, out stringToFloatHolder);
        switch (compareType)
        {
            case CompareType.LessThan:
                if (stringToFloatHolder < numberToCompare)
                {
                    DestroyObject();
                }
                break;
            case CompareType.LessThanOrEqual:
                if (stringToFloatHolder <= numberToCompare)
                {
                    DestroyObject();
                }
                break;
            case CompareType.Equal:
                if (stringToFloatHolder == numberToCompare)
                {
                    DestroyObject();
                }
                break;
            case CompareType.GreaterThan:
                if (stringToFloatHolder > numberToCompare)
                {
                    DestroyObject();
                }
                break;
            case CompareType.GreaterThanOrEqual:
                if (stringToFloatHolder >= numberToCompare)
                {
                    DestroyObject();
                }
                break;
        }
    }

    private void DestroyObject()
    {
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
    }
}
