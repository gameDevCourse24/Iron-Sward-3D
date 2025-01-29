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

    private bool alreadyDetected = false;

    [Header("These fields are for display only")]
    [SerializeField] private Vector3 targetPosition;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    void OnValidate()
    {
        if (target == null)
        {
            target = GameObject.Find(targetName);
            if (target == null)
            {
                Debug.LogWarning("Chaser: The target object is not found");
            }
        }
    }
    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (canChase()){
            targetPosition = target.transform.position;
            pprint.p("target position is:" + targetPosition, this);
            Facetarget();
            navMeshAgent.destination = targetPosition;
            pprint.p("chasing target", this);
        }
    }
    private bool canChase() {
        float distanceToPlayer = Vector3.Distance(transform.position, targetPosition);
        //distance to the target < maxDetectionDistance
       pprint.p("distance to target is:" + distanceToPlayer, this);
        
        if (distanceToPlayer < maxDetectionDistance) {
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
