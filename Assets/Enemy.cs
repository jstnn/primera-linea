using System;
using System.Collections;
using System.Collections.Generic;
using MonsterLove.StateMachine;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public GameObject deathSplash;
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
    public int health = 80;
    public int resistance = 100;
    public int attack = 50;
    public bool isDead = false;
    public Player target;
    public GameObject hitbox;

    private StateMachine<States> fsm;
    private NavMeshAgent agent;
    private Animator anim;
    private GameManager gameManager;

    void Awake()
    {
        fsm = StateMachine<States>.Initialize(this, States.Idle);
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        MainCollider = GetComponent<CapsuleCollider>();
        AllColliders = GetComponentsInChildren<CapsuleCollider>(true);
        gameManager = GameObject.Find("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        if (isDead) return;
        if ((fsm.State != States.Attacked) && (!target || target && target.isDead)) {
            searchTarget();
        }
        if ((fsm.State != States.Attacked) && target && !target.isDead)
        {
            agent.SetDestination(target.transform.position);
        }
        if ((fsm.State != States.Walk) && agent.velocity.sqrMagnitude > 0.1f && agent.velocity.sqrMagnitude < 60f)
        {
             fsm.ChangeState(States.Walk);
        }
        if ((fsm.State != States.Attack || fsm.State != States.Attacked) && agent.velocity.sqrMagnitude < 0.1f)
        {
             fsm.ChangeState(States.Idle);
        }
        if ((fsm.State != States.Run) && agent.velocity.sqrMagnitude > 60f)
        {
             fsm.ChangeState(States.Run);
        }
        if (health<1)
        {
            fsm.ChangeState(States.Dead);
        }
    }

    void searchTarget() {
        if (gameManager && gameManager.players.Count > 0) {
            target=null;
            foreach (Player player in gameManager.players) {
                if (!player.isDead) {
                    target = player;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isDead && other.gameObject.CompareTag("Player"))
        {
            Player otherPlayer =  other.gameObject.GetComponent<Player>();
            if (!otherPlayer.isDead)
                fsm.ChangeState(States.Attack);
        }
        if (!isDead && other.gameObject.CompareTag("Hitbox_Player"))
        {
            Player otherPlayer =  other.gameObject.transform.parent.GetComponent<Player>();
            DoDamage(otherPlayer.attack);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!isDead && other.gameObject.CompareTag("Player"))
        {
            Player otherPlayer = other.gameObject.GetComponent<Player>();
            if (otherPlayer && !otherPlayer.isDead)
                fsm.ChangeState(States.Attack);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isDead && other.gameObject.CompareTag("Hitbox_Player"))
            fsm.ChangeState(States.Idle);
    }

    public void DoDamage(int damage)
    {
        fsm.ChangeState(States.Attacked);
        Debug.Log("paco recieved "+damage+" damage");
        health -= damage;
    }

    public void DoRagdoll()
    {
        hitbox.SetActive(false);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        foreach (var col in AllColliders)
            col.enabled = true;

        MainCollider.enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<LocomotionSimpleAgent>().enabled = false;
        agent.isStopped = true;
        agent.enabled = false;
    }

    private void Run_Enter()
    {
        anim.Play("run");
    }

    private void Walk_Enter()
    {
        anim.Play("walk");
    }

    private void Idle_Enter()
    {
        if (anim)
            anim.Play("idle");
    }

    private IEnumerator Attack_Enter()
    {
        anim.Play("attack");
        hitbox.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        fsm.ChangeState(States.Idle);
    }

    private void Attack_Exit()
    {
        hitbox.SetActive(false);
    }

    private void Attacked_Enter()
    {
        anim.Play("impact");
    }

    private void Dead_Enter()
    {
        isDead=true;
        DoRagdoll();
    }
}