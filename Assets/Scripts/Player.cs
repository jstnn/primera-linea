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
        Attack,
        Attacked,
        Run,
        Walk,
        Idle,
        Dead
    }
    public CapsuleCollider MainCollider;
    public CapsuleCollider[] AllColliders;
    public int health = 100;
    public int resistance = 100;

    private NavMeshAgent agent;
    private StateMachine<States> fsm;
    private Animator anim;
    private GameManager gameManager;
    RaycastHit hitInfo = new RaycastHit();
    public bool isAttacking = false;
    public bool isBeignAttacked = false;

    void Awake()
    {
        fsm = StateMachine<States>.Initialize(this, States.Idle);
        gameManager = GameObject.Find("GameController").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        MainCollider = GetComponent<CapsuleCollider>();
        AllColliders = GetComponentsInChildren<CapsuleCollider>(true);
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 10f;
        agent.angularSpeed = 1000;
        agent.acceleration = 60;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                agent.destination = hitInfo.point;
            }
        }
        if (agent.velocity.sqrMagnitude > 0.1f && agent.velocity.sqrMagnitude < 60f)
        {
             fsm.ChangeState(States.Walk);
        }
        if (agent.velocity.sqrMagnitude < 0.1f)
        {
             fsm.ChangeState(States.Idle);
        }
        if (agent.velocity.sqrMagnitude > 60f)
        {
             fsm.ChangeState(States.Run);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fsm.ChangeState(States.Attack);
        }
    }

    public void DoRagdoll(bool isRagdoll)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        foreach (var col in AllColliders)
            col.enabled = isRagdoll;

        MainCollider.enabled = !isRagdoll;
        GetComponent<Rigidbody>().useGravity = !isRagdoll;
        GetComponent<Animator>().enabled = !isRagdoll;
        GetComponent<LocomotionSimpleAgent>().enabled = !isRagdoll;
        agent.enabled = !isRagdoll;
        agent.isStopped = !isRagdoll;
    }

    public void DoDamage(int damage)
    {
        fsm.ChangeState(States.Attacked);
        Debug.Log("recieved damage");
        health -= damage;
    }

    private void Run_Enter()
    {
        isAttacking=false;
        isBeignAttacked=false;
        anim.Play("run");
    }

    private void Walk_Enter()
    {
        isAttacking=false;
        isBeignAttacked=false;
        anim.Play("walk");
    }

    private void Idle_Enter()
    {
        isAttacking=false;
        isBeignAttacked=false;
        if (anim) {
            anim.Play("idle");
        }
    }

    private IEnumerator Attack_Enter() {
        isAttacking=true;
        isBeignAttacked=false;
        anim.Play("attack");
        yield return new WaitForSeconds(0.5f);
        fsm.ChangeState(States.Idle);
    }

    private void Attacked_Enter() {
        isAttacking=false;
        isBeignAttacked=true;
        anim.Play("impact");
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
