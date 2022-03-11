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
    public int attack = 50;
    public bool isDead = false;
    public GameObject hitbox;
    public StateMachine<States> fsm;

    private NavMeshAgent agent;
    private Animator anim;
    private GameManager gameManager;
    private RaycastHit hitInfo = new RaycastHit();

    void Awake()
    {
        fsm = StateMachine<States>.Initialize(this, States.Idle);
        anim = GetComponent<Animator>();
        MainCollider = GetComponent<CapsuleCollider>();
        AllColliders = GetComponentsInChildren<CapsuleCollider>(true);
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameObject.Find("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        if (isDead) return;
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                agent.destination = hitInfo.point;
            }
        }
        if ((fsm.State != States.Walk) && agent.velocity.sqrMagnitude > 0.1f && agent.velocity.sqrMagnitude < 60f)
        {
             fsm.ChangeState(States.Walk);
        }
        if ((fsm.State != States.Attack || fsm.State != States.Attacked) && agent.velocity.sqrMagnitude < 0.1f)
        {
             fsm.ChangeState(States.Idle);
        }
        if ((fsm.State != States.Run) && (agent.velocity.sqrMagnitude > 60f))
        {
             fsm.ChangeState(States.Run);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fsm.ChangeState(States.Attack);
        }
        if (health<1)
        {
            fsm.ChangeState(States.Dead);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isDead && other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>(); 
            if (!enemy.isDead)
                fsm.ChangeState(States.Attack);
        }
        if (!isDead && other.gameObject.CompareTag("Hitbox_Enemy")) {
            Enemy enemy = other.gameObject.transform.parent.GetComponent<Enemy>();
            DoDamage(enemy.attack);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!isDead && other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>(); 
            if (!enemy.isDead)
                fsm.ChangeState(States.Attack);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!isDead && other.gameObject.CompareTag("Hitbox_Enemy"))
            fsm.ChangeState(States.Idle);
    }

    public void DoDamage(int damage)
    {
        fsm.ChangeState(States.Attacked);
        Debug.Log("player recieved "+damage+" damage");
        health -= damage;
    }

    public void DoRagdoll()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        foreach (var col in AllColliders)
            col.enabled = true;

        MainCollider.enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<LocomotionSimpleAgent>().enabled = false;
        agent.enabled = false;
        agent.isStopped = true;
    }

    public void CeaseAllFunctions()
    {
        hitbox.SetActive(false);
        foreach (CapsuleCollider collider in AllColliders)
            collider.enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        foreach (var col in AllColliders)
            col.enabled = false;

        MainCollider.enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<LocomotionSimpleAgent>().enabled = false;
        agent.enabled = false;
        agent.isStopped = true;
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
        if (anim) {
            anim.Play("idle");
        }
    }

    private IEnumerator Attack_Enter() {
        anim.Play("attack");
        hitbox.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        fsm.ChangeState(States.Idle);
    }

    private void Attack_Exit()
    {
        hitbox.SetActive(false);
    }

    private void Attacked_Enter() {
        anim.Play("impact");
    }

    private void Dead_Enter()
    {
        isDead=true;
        Debug.Log("DIED XXXX LOL");
        gameManager.RemovePlayer(this.gameObject);
        DoRagdoll();
        CeaseAllFunctions();
    }
}
