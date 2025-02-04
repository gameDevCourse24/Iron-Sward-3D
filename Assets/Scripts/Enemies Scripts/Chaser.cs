using System;
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
                Debug.LogWarning("Chaser: The target object is not found");
            }
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
        randomTargetPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (target != null)
        {
            targetPosition = target.transform.position;
        }
        if (canChase()){
            // pprint.p("target position is:" + targetPosition, this);
            Facetarget();
            navMeshAgent.destination = targetPosition;
            randomTargetPosition = transform.position;
            // pprint.p("chasing target", this);
        }
        else
        {
            if (wanderRadius > 0)
            {
                WanderAround();
                pprint.p("wandering around", this);
            }
        }
    }
    private bool canChase() {
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        //distance to the target < maxDetectionDistance
    //    pprint.p("distance to target is:" + distanceToPlayer, this);
        
        if (distanceToTarget < maxDetectionDistance) {
            if (!alreadyDetected) {
                maxDetectionDistance += increaseDetectionDistance;
                alreadyDetected = true;
            }
            return true;
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
