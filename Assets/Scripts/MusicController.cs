using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MusicController : Singleton<MusicController>
{

    private StudioEventEmitter musicEmitter; // this variable stores the event
    public string parameterName; // this variable stores the name of the parameter to change

    void Start()
    {
        musicEmitter = GetComponent<StudioEventEmitter>(); // we assign a component to this variable
    }

    public void ChangeLevel(int level) // the level value is passed to this script from another script
    {
        if (musicEmitter != null)
        {
            musicEmitter.SetParameter(parameterName, level); // set the FMOD parameter
        }
    }

}