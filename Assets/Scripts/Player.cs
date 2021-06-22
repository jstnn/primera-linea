using System;
using System.Collections;
using System.Collections.Generic;
using MonsterLove.StateMachine;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(GameManager))]
public class Player : MonoBehaviour
{
    public enum States
    {
        Active,
        Dead
    }
    public CapsuleCollider MainCollider;
    public CapsuleCollider[] AllColliders;
    public int health = 50;
    public int resistance = 100;

    private NavMeshAgent agent;
    private StateMachine<States> fsm;
    private Animator anim;
    private GameManager gameManager;

    // Use this for initialization
    void Awake()
    {
        fsm = StateMachine<States>.Initialize(this, States.Active);
        gameManager = GetComponent<GameManager>();
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
        agent.isStopped = !isRagdoll;
    }

    public void DoDamage(int damage)
    {
        Debug.Log("recieved damage");
        health -= damage;
    }

    private void Active_Update()
    {
        if (health<1)
        {
            fsm.ChangeState(States.Dead);
        }
    }

    private void Dead_Enter()
    {
        Debug.Log("DIED XXXX LOL");
        DoRagdoll(true);
        gameManager.RemovePlayer(gameObject);
    }


}
