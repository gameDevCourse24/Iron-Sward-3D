using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;


/**
 * This component represents an enemy NPC that chases target.
 */
[RequireComponent(typeof(NavMeshAgent))]
public class Chaser: MonoBehaviour {

    GameObject target = null;

    [SerializeField, Tooltip("The name of the target object")]
    string targetName = "Player";//כשאני יוצר רודף חדש ואני לא מגדיר מטרה אז עכשיו תהיה לי את האופציה להגדיר את שם האובייקט שאני רוצה לרדוף אחריו

    [SerializeField, Tooltip("The rotation speed of the chaser")]
    private float ChaserRotationSpeed = 20f;

    [SerializeField, Tooltip("The maximum distance the chaser can detect the target")]
    private float maxDetectionDistance = 80f;

    [SerializeField, Tooltip("After the chaser detects the target once, the 'maxDetectionDistance' will be incresed by this value")]
    private float increaseDetectionDistance = 0f;

    [SerializeField, Tooltip("The chance that the chaser will chase the target even if it's out of range (but the target is still in the sight)"), Range(0, 1)]
    private float ChanceToChaseOutOfRange = 0;

    [SerializeField, Tooltip("The number of seconds the Chaser will wait before trying to Chase again."+
                            "(If the Chaser sees you but is not Chasering you then he will wait a few seconds before trying again -"+
                            " this all depends on the condition that you are not in his Chasing range,"+
                            " and he can still see you)")] private float numberOfSeconds = 3f;
    


    private float currentChanceToChaseOutOfRange;
    // private bool chackIfCanChase = true;

    [SerializeField, Tooltip("The maximum distance the chaser can wander around while can't chasing the target")]
    private float wanderRadius = 30f; // מרחק מירבי לכל יעד אקראי
    private bool alreadyDetected = false;

    [Header("These fields are for display only")]
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vector3 randomTargetPosition;
    [SerializeField] private float distanceToRandomTarget;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private void Start() {
        if (target == null)
        {
            target = GameObject.Find(targetName);
            if (target == null)
            {
                // Debug.LogWarning("Chaser: The target object is not found");
            }
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
        randomTargetPosition = transform.position;
        animator = GetComponent<Animator>();
        currentChanceToChaseOutOfRange = ChanceToChaseOutOfRange;
    }

    private void Update() {
        if (target != null)
        {
            targetPosition = target.transform.position;
        }
        if (canChase()){
            // chackIfCanChase = false;
            // pprint.p("target position is:" + targetPosition, this);
            Facetarget();
            navMeshAgent.destination = targetPosition;
            randomTargetPosition = transform.position;
            // StartCoroutine(ResetChackIfCanChase());
            // pprint.p("chasing target", this);
        }
        else
        {
            if (wanderRadius > 0)
            {
                WanderAround();
                // pprint.p("wandering around", this);
            }
        }
    }
    private bool canChase() {
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        //distance to the target < maxDetectionDistance
    //    pprint.p("distance to target is:" + distanceToPlayer, this);
        
        if (distanceToTarget <= maxDetectionDistance) {
            if (!alreadyDetected) {
                maxDetectionDistance += increaseDetectionDistance;
                alreadyDetected = true;
            }
            if (currentChanceToChaseOutOfRange != ChanceToChaseOutOfRange)
            {
                // pprint.p("can chase because the target in range, the currentChanceToChaseOutOfRange is:" + currentChanceToChaseOutOfRange, this);
                currentChanceToChaseOutOfRange = ChanceToChaseOutOfRange; //כדי שאם המטרה נכנסה לטווח אז הסיכוי לרדיפה יחזור להיוצ מה שהיה ולא יישאר 1
                // pprint.p("the currentChanceToChaseOutOfRange back to: " + currentChanceToChaseOutOfRange, this);
            }
            
            return true;
        }
        else
        {
            //if the enemy can see the target (even if the target is far away) so there is a "ChanceToChaseOutOfRange" that the enemy will chase the target
            if (currentChanceToChaseOutOfRange <= 0)
            {
                // pprint.p("The chance of chasing is 0.", this);
                return false;
            }
            if (!CanSeeTarget())
            {
                // pprint.p("I can't see the target.", this);
                currentChanceToChaseOutOfRange = ChanceToChaseOutOfRange;
                return false;
            }
            if (currentChanceToChaseOutOfRange >= 1)
            {
                // pprint.p("The chance of chasing is already 1.", this);
                return true;
            }
            
            float randomValue = UnityEngine.Random.value; //הגרלת מספר רנדומלי בין 0 ל1
            // pprint.p("random value is: " + randomValue + "and the currentChanceToChaseOutOfRange is:" + currentChanceToChaseOutOfRange, this);
            if (randomValue < currentChanceToChaseOutOfRange) // אם המספר שהגרלתי קטן מהסיכוי לרדיפה אז הוא ירדוף
            {
                currentChanceToChaseOutOfRange = 1; //כדי שהוא בטוח ימשיך לרדוף אחרי המטרה כל עוד הוא רואה אותה עד שיגיע לטווח רדיפה רגיל
                // pprint.p("can chase because the target is in sight, the currentChanceToChaseOutOfRange is: " + currentChanceToChaseOutOfRange, this);
                return true;
            }
            else //אם המספר שהגרלתי גדול מהסיכוי לרדיפה אז הוא לא ירדוף
            {
                // אם הרודף ראה את המטרה ולא רדף (פעם אחת) אז הוא לא ינסה לרדוף אחרי במשך כמה שניות. כי אחרת גם אם הסיכוי הוא 0.0001 אז אחרי מספיק פריימים הוא ירדוף אחרי בכל מקרה
                currentChanceToChaseOutOfRange = 0; // לא ירדוף אחרי המטרה אם היא לא בטווח למשך כמה שניות או עד שלא יראה אותה יותר או עד שיגיע לטווח רדיפה
                // pprint.p("can't chase because the random value is to low so the currentChanceToChaseOutOfRange is: " + currentChanceToChaseOutOfRange, this);
                // pprint.p("start wating: " + numberOfSeconds, this);
                StartCoroutine(ResetChaseChance()); // סופר שניות עד שהוא ינסה לרדוף שוב
                return false;
            }
        }
        
    }
    private IEnumerator ResetChaseChance()
    {
        yield return new WaitForSeconds(numberOfSeconds);
        if (currentChanceToChaseOutOfRange != ChanceToChaseOutOfRange) // אם הם עדיין שונים עד שעבר הזמן
        {
            currentChanceToChaseOutOfRange = ChanceToChaseOutOfRange; // החזרת הסיכוי לרדוף למצבו המקורי
            // pprint.p("stop wating: " + numberOfSeconds, this);
        }
        else // רק בשביל דיבאגינג
        {
            // pprint.p("the currentChanceToChaseOutOfRange is already: " + ChanceToChaseOutOfRange, this);
        }

    }
    // private IEnumerator ResetChackIfCanChase()
    // {
    //     yield return new WaitForSeconds(0.5f);
    //     chackIfCanChase = true;
    // }

    private bool CanSeeTarget()
    {   
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        Vector3 direction = (targetPosition - transform.position).normalized;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distanceToTarget);
        Debug.DrawRay(transform.position, direction * distanceToTarget, Color.red, 0.1f);
        foreach (RaycastHit hit in hits)
        {
            // דילוג על עצמו כדי לא לפגוע ביורה
            if (hit.collider.gameObject == gameObject || hit.collider.isTrigger) 
                continue; 

            // אם הקרן פגעה במטרה, אז אפשר לראות אותה
            if (hit.collider.gameObject == target) 
                return true;

            // אם הקרן פגעה באובייקט אחר לפני המטרה, המטרה מוסתרת ואין קו ראייה ישיר
            else
            {
                // pprint.p("i see: "+ hit.collider.gameObject.name, this);
                return false;
            }
        }
        return false;
    }

    private void Facetarget() {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0 , direction.z));
        // transform.rotation = lookRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * ChaserRotationSpeed);
    }

    private void WanderAround()
    {
        distanceToRandomTarget = Vector3.Distance(transform.position, randomTargetPosition);
        // אם הרודף הגיע ליעד הנוכחי, נבחר יעד חדש
        if (distanceToRandomTarget <= navMeshAgent.stoppingDistance + 1)
        {
            SetRandomDestination();
        }
        else
        {
            navMeshAgent.destination = randomTargetPosition;
        }
    }

    private void SetRandomDestination()
    {
        while (true) // לולאה אינסופית עד שמוצאים יעד תקף
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position; // הזזת הנקודה למיקום יחסית לרודף

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit randomTarget, wanderRadius, NavMesh.AllAreas))
            {
                NavMeshPath path = new NavMeshPath();
                navMeshAgent.CalculatePath(randomTarget.position, path);

                if (path.status == NavMeshPathStatus.PathComplete) // אם הנתיב חוקי
                {
                    randomTargetPosition = randomTarget.position;
                    navMeshAgent.SetDestination(randomTargetPosition);
                    return; // יציאה מהלולאה לאחר שמצאנו יעד תקף
                }
            }
        }
    }


    // internal Vector3 TargetObjectPosition() {
    //     return target.transform.position;
    // }

    // private void FaceDirection() {
    //     Vector3 direction = (navMeshAgent.destination - transform.position).normalized;
    //     Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    //     // transform.rotation = lookRotation;
    //     transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    // }

}
