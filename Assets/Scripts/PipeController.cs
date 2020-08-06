using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{

    void OnTriggerEnter(Collider collision)
    {
        Player player = collision.transform.GetComponent<Player>();
        if (player)
        {
            player.DoRagdoll(true);
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