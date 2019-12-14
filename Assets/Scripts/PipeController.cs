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
