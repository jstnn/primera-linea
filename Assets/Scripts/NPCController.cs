using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{

    public Transform[] Navs;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent.velocity.magnitude == 0f)
        {
            agent.SetDestination(Navs[Random.Range(0, Navs.Length)].position);
        }
    }
}
