using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameManager))]
public class CameraController : MonoBehaviour
{
    public float smoothing = 5f;
    Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        GameObject[] players = GetComponent<GameManager>().players;
        if (players.Length > 0)
        {
            GameObject target = players[players.Length-1];
            Vector3 targetCamPos = target.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}