using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePipe : MonoBehaviour
{
    GameObject pipe;
    bool rotating;

    // Start is called before the first frame update
    void Start()
    {
        pipe = GameObject.Find("WaterWuanaco");
        StartCoroutine(Flush());
    }

    void Update()
    {
        if(rotating)
        {
            pipe.transform.Rotate(0.0f, 0.8f, 0.0f, Space.Self);
        }
    }

    IEnumerator Flush()
    {
        // Print the time of when the function is first called.
        // Debug.Log("Started Coroutine at timestamp : " + Time.time);
        rotating = true;


        // yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        rotating = false;

        yield return new WaitForSeconds(2);

        // After we have waited 5 seconds print the time again.
        // Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        StartCoroutine(Flush());
    }
}
