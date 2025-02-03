using UnityEngine;

public class CrosPanelToShow : MonoBehaviour
{
    [SerializeField, Tooltip("Choose cross panel to show")] GameObject panelToShow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     if (panelToShow != null)
    //     {
    //         panelToShow.SetActive(true);
    //     }
    // }
    void OnEnable()
    {
        if (panelToShow != null)
        {
            panelToShow.SetActive(true);
        }
    }

   void OnDisable()
    {
        if (panelToShow != null)
        {
            panelToShow.SetActive(false);
        }
    }
}
