using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Player : MonoBehaviour
{
    public CapsuleCollider MainCollider;
    public CapsuleCollider[] AllColliders;
    NavMeshAgent agent;

    // Use this for initialization
    void Awake()
    {
        MainCollider = GetComponent<CapsuleCollider>();
        AllColliders = GetComponentsInChildren<CapsuleCollider>(true);
        agent = GetComponent<NavMeshAgent>();
        DoRagdoll(false);
    }

    // Update is called once per frame
    public void DoRagdoll(bool isRagdoll)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        foreach (var col in AllColliders)
            col.enabled = isRagdoll;

        MainCollider.enabled = !isRagdoll;
        GetComponent<Rigidbody>().useGravity = !isRagdoll;
        GetComponent<Animator>().enabled = !isRagdoll;
        GetComponent<LocomotionSimpleAgent>().enabled = !isRagdoll;
        GetComponent<ClickToMove>().enabled = !isRagdoll;
        agent.enabled = !isRagdoll;
        //agent.isStopped = !isRagdoll;
    }
}
