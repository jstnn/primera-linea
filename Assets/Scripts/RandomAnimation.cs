using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animator anim = GetComponent<Animator>();

        int pickAnumber = Random.Range(0, 13);//exclusive never prints the last only goes 1 to 2
        Debug.Log(pickAnumber);

        //randJumpInt is the parameter in animator
        //pickAnumber random number from 1 to 2 from above
        anim.SetInteger("idleIndex", pickAnumber);
    }
}
