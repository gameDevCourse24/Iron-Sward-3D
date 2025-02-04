using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class BulletClickSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField, Tooltip("The speed of the object")]
    private float objectSpeed = 80f; // מהירות הקליע

    [SerializeField, Tooltip("Fire rate (bullets per second)")]
    private float fireRate = 5f; // מספר כדורים לשנייה
    [SerializeField, Tooltip("The button that will trigger the action")]
    private InputAction clickAction; // פעולה לזיהוי לחיצה
    [SerializeField, Tooltip("The maximum distance of the ray cast if it doesn't hit anything")]
    private float maxRayDistance = 50f; // מרחק הקרן

    private bool isFiring = false; // בודק אם הירי פעיל

    public bool PrintLog = true;
    private void OnEnable()
    {
        clickAction.Enable();
    }
    private void OnDisable()
    {
        clickAction.Disable();
    }
    void OnValidate()
    {
         if (clickAction == null)
            clickAction = new InputAction(type: InputActionType.Button);
        if (clickAction.bindings.Count == 0)
            clickAction.AddBinding("<Mouse>/leftButton");
    }

    // Update is called once per frame
    void Update()
    {
        if (clickAction.IsPressed() && !isFiring) // אם הלחצן נלחץ והירי לא פעיל
        {
            StartCoroutine(FireCoroutine());
        }
    }

    private IEnumerator FireCoroutine()
    {
        isFiring = true;
        while (clickAction.IsPressed()) // ירי מתמשך כל עוד הלחצן נלחץ
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
        }
        // pprint.p("Bullet created", this);
    }
    void print(string message)
    {
        if (PrintLog)
        {
            Debug.Log(message);
        }
    }
}
