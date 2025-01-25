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
    string targetName = "Player";//כשאני יוצר אוייב חדש ואני לא מגדיר מטרה אז עכשיו תהיה לי את האופציה להגדיר את שם האובייקט שאני רוצה לרדוף אחריו

    [SerializeField, Tooltip("The rotation speed of the chaser")]
    private float ChaserRotationSpeed = 20f;

    [SerializeField, Tooltip("The maximum distance the chaser can detect the target")]
    private float maxDetectionDistance = 80f;

    [Header("These fields are for display only")]
    [SerializeField] private Vector3 targetPosition;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    void OnValidate()
    {
        if (target == null)
        {
            target = GameObject.Find(targetName);
        }
    }
    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (canChase()){
            targetPosition = target.transform.position;
            Facetarget();
            navMeshAgent.destination = targetPosition;
        }
    }
    private bool canChase() {
        float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);
        //distance to the target < maxDetectionDistance
        Debug.Log("chaser: distance to target is:" + distanceToPlayer);
        
        return distanceToPlayer < maxDetectionDistance;
    }
    private void Facetarget() {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0 , direction.z));
        // transform.rotation = lookRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * ChaserRotationSpeed);
    }

    internal Vector3 TargetObjectPosition() {
        return target.transform.position;
    }

    private void FaceDirection() {
        Vector3 direction = (navMeshAgent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // transform.rotation = lookRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

}
