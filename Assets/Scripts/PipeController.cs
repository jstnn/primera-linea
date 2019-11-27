using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
        Player player = collision.transform.GetComponent<Player>();
        player.DoRagdoll(true);

    }
}
