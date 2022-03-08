using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.transform.GetComponent<Player>();
        // Debug.Log("if OnCollisionEnter");
        if (player)
        {
            Debug.Log(collision+", "+player);
            // Debug.Log("if player");
            player.DoDamage(50);
            // player.DoRagdoll(true);
        }
    }
}

// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;
// using MonsterLove.StateMachine;

// public class PipeController : MonoBehaviour
// {
//     public enum States
//     {
//         Idle,
//         Moving,
//         Dead
//     }
//     public ParticleSystem waterLauncher;
//     List<ParticleCollisionEvent> collisionEvents;

//     void Start()
//     {
//         collisionEvents = new List<ParticleCollisionEvent>();
//     }

//     void OnParticleCollision(GameObject other)
//     {
//         ParticlePhysicsExtensions.GetCollisionEvents(waterLauncher, other, collisionEvents);
//         for (int i = 0; i < collisionEvents.Count; i++)
//         {
//             Rigidbody rb = other.GetComponent<Rigidbody>();
//             if (rb)
//             {
//                 Player player = other.GetComponent<Player>();
//                 player.DoDamage(10);
//             }
//         }
//     }
// }