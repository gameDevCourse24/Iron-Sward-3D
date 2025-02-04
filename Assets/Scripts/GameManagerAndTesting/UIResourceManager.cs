using UnityEngine;
using TMPro;

public class UIResourceManager : MonoBehaviour
{
    [SerializeField, Tooltip("The resource manager that holds the player's resources")]
    private ResourceManager resourceManager;
    [SerializeField, Tooltip("The text that displays the player's intelligence")]
    private TextMeshProUGUI intelligenceText;
    [SerializeField, Tooltip("The text that displays the player's health kits")]
    private TextMeshProUGUI healthKitsText;
    [SerializeField, Tooltip("The text that displays the player's ammo")]
    private TextMeshProUGUI ammoText;
    [SerializeField, Tooltip("The text that displays the player's lives")]
    private TextMeshProUGUI lifeText;
    [SerializeField, Tooltip("The text that displays the player's box magazines")]
    private TextMeshProUGUI boxMagazineText;
    [SerializeField, Tooltip("The text that displays the hostages in this level")]
    private TextMeshProUGUI hotagesInThisLevelText;

    [SerializeField, Tooltip("The text that displays the hostages that you already saved")]
    private TextMeshProUGUI hotagesSavedText;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        intelligenceText.text = intelligenceTextToShow();
        healthKitsText.text = healthKitsTextToShow();
        lifeText.text = lifeTextToShow();
        boxMagazineText.text = boxMagazineTextToShow();
    }

    private string intelligenceTextToShow()
    {
        return resourceManager.GetIntelligence().ToString();
    }
    private string healthKitsTextToShow()
    {
        return resourceManager.GetHealthKits().ToString();
    }
    private string lifeTextToShow()
    {
        return resourceManager.GetLife().ToString();
    }
    private string boxMagazineTextToShow()
    {
        int boxMagazine = resourceManager.GetBoxMagazine();
        int numberToShow = boxMagazine * resourceManager.GetDefaultBulletInMagazine();
        return numberToShow.ToString();
    }
    private string bulletInCurrentMagazineTextToShow()
    {
        return resourceManager.GetBulletInCurrentMagazine().ToString();
    }

}
