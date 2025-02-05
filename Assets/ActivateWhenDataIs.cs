
using UnityEngine;
using TMPro;
public class ActivateWhenDataIs : MonoBehaviour
{
    [SerializeField, Tooltip("The object to activate.")]
    private GameObject objectToActivate;

    [SerializeField, Tooltip("activate this object when this data")]
    private TMP_Text textComponent;
    public bool stopTheGame = false;

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
                    activate();
                }
                break;
            case CompareType.LessThanOrEqual:
                if (stringToFloatHolder <= numberToCompare)
                {
                    activate();
                }
                break;
            case CompareType.Equal:
                if (stringToFloatHolder == numberToCompare)
                {
                    activate();
                }
                break;
            case CompareType.GreaterThan:
                if (stringToFloatHolder > numberToCompare)
                {
                    activate();
                }
                break;
            case CompareType.GreaterThanOrEqual:
                if (stringToFloatHolder >= numberToCompare)
                {
                    activate();
                }
                break;
        }
    }

    private void activate()
    {
        if (stopTheGame)
        {
            Time.timeScale = 0;
        }
        objectToActivate.SetActive(true);
    }
}

