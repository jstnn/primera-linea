using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AdaptiveZone : MonoBehaviour
{

    public int level; // this value will be added in the Inspector; it will be passed to the MusicController script

    void OnTriggerEnter(Collider collider)
    {
        MusicController.Instance.ChangeLevel(level);
    }

    void OnTriggerExit(Collider collider)
    {
        MusicController.Instance.ChangeLevel(1);
    }

}