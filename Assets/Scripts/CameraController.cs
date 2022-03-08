using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameManager))]
public class CameraController : MonoBehaviour
{
    public float smoothing = 5f;
    public GameObject camera;
    Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        if (GetComponent<GameManager>().players.Count > 0)
        {
            GameObject target = GetComponent<GameManager>().players[0];
            Vector3 targetCamPos = target.transform.position + offset;
            camera.transform.position = Vector3.Lerp(camera.transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}