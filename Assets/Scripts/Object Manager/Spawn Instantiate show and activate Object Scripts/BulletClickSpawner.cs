using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
using System;

public class BulletClickSpawner : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField, Tooltip("The object to spawn")]
    private GameObject spawnObject;
    [SerializeField, Tooltip("The speed of the object")]
    private float objectSpeed = 80f; // מהירות הקליע

    [SerializeField, Tooltip("Fire rate (bullets per second)")]
    private float fireRate = 5f; // מספר כדורים לשנייה
    [SerializeField, Tooltip("The button that will trigger the shooting")]
    private InputAction shootAction; // פעולה לזיהוי לחיצה

    [SerializeField, Tooltip("The button that will trigger the reloading")]
    private InputAction reloadAction; // פעולה לזיהוי לחיצה
    [SerializeField, Tooltip("The maximum distance of the ray cast if it doesn't hit anything")]
    private float maxRayDistance = 50f; // מרחק הקרן

    [Header("UI update settings")]
    [SerializeField, Tooltip("Drag the TMP_Text component from your Canvas here.")]
    private TMP_Text BulletTextComponent;
    [SerializeField, Tooltip("How many bullets to reduced in one shot? ")]
    private float BulletToReduce = 1;
    [SerializeField, Tooltip("How long does it take to reload? (in seconds)")]
    private float TimeToReload = 3f;
     [SerializeField, Tooltip("Drag the TMP_Text component from your Canvas here.")]
    private TMP_Text MagazineTextComponent;

    [SerializeField, Tooltip("How many bullet magazines do you want to start the game with?")]
    private float BulletMagazines = 3;

    [SerializeField, Tooltip("How many bullets does each magazine contain?")]
    private float BulletInOneMagazines = 30;

    private bool isReloading = false;
    private bool isFiring = false; // בודק אם הירי פעיל

    private float numOfBulletsInCurrentMagazine; // כמות הכדורים במחסנית הנוכחית
    private float numOfMagazines; // כמות המחסניות שיש לי (שאינן בשימוש)
    private float numOfBulletsInUnusedMagazine; // כמות הכדורים במחסניות שאינה בשימוש

    public bool PrintLog = true;
    private void OnEnable()
    {
        shootAction.Enable();
        reloadAction.Enable();
    }
    private void OnDisable()
    {
        shootAction.Disable();
        reloadAction.Disable();
    }
    void OnValidate()
    {
        if (shootAction == null)
            shootAction = new InputAction(type: InputActionType.Button);
        if (shootAction.bindings.Count == 0)
            shootAction.AddBinding("<Mouse>/leftButton");
        
        if (reloadAction == null)
            reloadAction = new InputAction(type: InputActionType.Button);
        if (reloadAction.bindings.Count == 0)
            reloadAction.AddBinding("<Keyboard>/R");
    }

    private void Start()
    {
        numOfBulletsInCurrentMagazine = BulletInOneMagazines;
        numOfMagazines = BulletMagazines;
        numOfBulletsInUnusedMagazine = BulletInOneMagazines  * BulletMagazines;
        UpdateUI();
    }
    // Update is called once per frame
    void Update()
    {
        if ((numOfMagazines > 0 && !isReloading) && (reloadAction.IsPressed() || numOfBulletsInCurrentMagazine <= 0)) // אם אין כדורים במחסנית ויש עוד מחסניות או שהלחצן נלחץ
        {
            StartCoroutine(ReloadCoroutine());
        }
        else if (shootAction.IsPressed() && !isFiring && !isReloading) // אם הלחצן נלחץ והירי לא פעיל
        {
            StartCoroutine(FireCoroutine());
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(TimeToReload);
        numOfBulletsInUnusedMagazine += numOfBulletsInCurrentMagazine;
        numOfBulletsInCurrentMagazine = numOfBulletsInUnusedMagazine >  BulletInOneMagazines? BulletInOneMagazines : numOfBulletsInUnusedMagazine;
        numOfBulletsInUnusedMagazine -= numOfBulletsInCurrentMagazine;
        UpdateUI();
        isReloading = false;
    }

    private void UpdateUI()
    {
        parseFromFloatToUIText(BulletTextComponent, numOfBulletsInCurrentMagazine);
        parseFromFloatToUIText(MagazineTextComponent, numOfBulletsInUnusedMagazine);
    }

    private IEnumerator FireCoroutine()
    {
        isFiring = true;
        while (shootAction.IsPressed() && numOfBulletsInCurrentMagazine > 0 && !reloadAction.IsPressed()) // ירי מתמשך כל עוד הלחצן נלחץ
        {
            ShootBullet();
            yield return new WaitForSeconds(1f / fireRate); // מחכה בהתאם לקצב האש
        }
        isFiring = false; // מפסיק את הירי כאשר העכבר משוחרר
    }
     private void ShootBullet()
    {
        // pprint.p("ShootBullet function", this);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            targetPoint = hitInfo.point; // אם הקרן פגעה במשהו
        }
        else
        {
            // אם אין פגיעה, משתמשים במיקום רחוק בכיוון הקרן
            targetPoint = ray.GetPoint(maxRayDistance);
        }

        // מחשבים כיוון מהאובייקט הנוכחי לנקודת הפגיעה
        Vector3 direction = (targetPoint - transform.position).normalized;

        // יוצרים את הקליע
        GameObject bullet = Instantiate(spawnObject, transform.position, Quaternion.identity);

        // מוסיפים לו כוח
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * objectSpeed;
            numOfBulletsInCurrentMagazine -= BulletToReduce;
            UpdateUI();
        }
        // pprint.p("Bullet created", this);
    }
    private void parseFromUITextToFloat(TMP_Text textComponent, ref float value)
    {
        if (float.TryParse(textComponent.text, out float currentData))
        {
            value = currentData;
        }
        else
        {
            print("Failed to parse text to a number!");
        }
    }
    private void parseFromFloatToUIText(TMP_Text textComponent, float value)
    {
        textComponent.text = value.ToString();
    }
    void print(string message)
    {
        if (PrintLog)
        {
            Debug.Log(message);
        }
    }
}
