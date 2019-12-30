using System;
using System.Collections;
using System.Collections.Generic;
using MonsterLove.StateMachine;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public enum States
    {
        Playing,
        Dead
    }
    public CapsuleCollider MainCollider;
    public CapsuleCollider[] AllColliders;
    public int health = 50;
    public int resistance = 100;

    private NavMeshAgent agent;
    private StateMachine<States> fsm;
    private Animator anim;

    // Use this for initialization
    void Awake()
    {
        fsm = StateMachine<States>.Initialize(this, States.Playing);
        anim = GetComponent<Animator>();
        MainCollider = GetComponent<CapsuleCollider>();
        AllColliders = GetComponentsInChildren<CapsuleCollider>(true);
        agent = GetComponent<NavMeshAgent>();
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

    public void Die()
    {
        DoRagdoll(true);
    }

    public void DoDamage(int damage)
    {
        health -= damage;
        Debug.Log("recieved damage");
    }

    private void Playing_Update()
    {
        if (health<1)
        {
            fsm.ChangeState(States.Dead);
        }
    }

    private void Dead_Enter()
    {
        Die();
        Debug.Log("DIED XXXX LOL");
    }


}
