using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField, Tooltip("The target you want to hit")] private string targetName; // The main player
    [SerializeField, Tooltip("The maximum shooting Range")] private float detectionRange = 10f; // טווח זיהוי
    [SerializeField, Tooltip("The bullet")] GameObject bulletPrefab; // קליע
    [SerializeField, Tooltip("The speed of the bullet")] float objectSpeed = 10f; // מהירות הקליע
    [SerializeField, Tooltip("The point you want the bullet will instantiate")] Transform firePoint; // נקודת ירי
    [SerializeField, Tooltip("Bullets per second")] private float fireRate = 2f; // קצב ירי
    private Transform target; // קישור לשחקן הראשי
    Vector3 directionToTarget;
     private float nextFireTime = 0f; // משתנה למעקב אחרי זמן הירי הבא

    void Start()
    {
        target = GameObject.Find(targetName).transform; // מציאת השחקן הראשי
    }

    void Update()
    {
        // בדיקת מרחק
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer < detectionRange) // אם המטרה בטווח האיתור
        {
            if (Time.time >= nextFireTime) // אם עבר הזמן שהוגדר כזמן הירי הבא
            {
                // כיוון הקרן לשחקן
                directionToTarget = (target.position - transform.position).normalized;

                // שולח קרן שממשיכה לעבור דרך אובייקטים עם isTrigger = true
                RaycastHit[] hits = Physics.RaycastAll(transform.position, directionToTarget, distanceToPlayer);

                foreach (RaycastHit hit in hits)
                {
                    Debug.DrawRay(transform.position, directionToTarget * distanceToPlayer, Color.red, 0.1f);
                    // אם ה- Collider הוא isTrigger - תתעלם ממנו ותמשיך הלאה
                    if (hit.collider.isTrigger) continue;

                    // אם הגענו לשחקן או לאחד האובייקטים שבתוך ההיררכיה שלו
                    if (IsPlayerOrChild(hit.collider.gameObject))
                    {
                        Debug.Log("EnemyShooting: I can see the target, I can shoot!");
                        if (target != null)
                        {
                            firePoint.LookAt(target.position); // מסובב את נקודת הירי לכיוון השחקן
                        }
                        Shoot();
                        nextFireTime = Time.time + (1f / fireRate); // מחושב הזמן בו אפשר לירות שוב
                        return; // יוצאים מהלולאה כדי למנוע ירי כפול
                    }
                    else
                    {
                        Debug.Log("EnemyShooting: I cant see the target, " + hit.collider.gameObject.name + " is in the way");
                        return; // אם פגענו במשהו שאינו השחקן, הקרן נעצרת כאן
                    }
                }
            }
            
        }
        else
        {
            Debug.Log("EnemyShooting: The target is out of range");
        }
    }

    void Shoot()
    {
        Debug.Log("EnemyShooting: I shoot on the target!");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = directionToTarget * objectSpeed;
        }
    }

    // פונקציה שבודקת אם האובייקט שנפגע הוא השחקן או אחד מהילדים שלו
    bool IsPlayerOrChild(GameObject hitObject)
    {
        Transform current = hitObject.transform;

        while (current != null)
        {
            if (current == target) return true; // אם זה השחקן - החזר true
            current = current.parent; // המשך לבדוק כלפי מעלה בעץ האובייקטים
        }
        return false;
    }
}
