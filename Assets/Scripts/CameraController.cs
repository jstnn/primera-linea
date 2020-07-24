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
        if (GetComponent<GameManager>().players.Count > 0)
        {
            GameObject target = GetComponent<GameManager>().players[0];
            Vector3 targetCamPos = target.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}