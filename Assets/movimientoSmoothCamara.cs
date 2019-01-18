using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoSmoothCamara : MonoBehaviour
{
    public Transform target;
    public float smoothTime;
    public float posicionY;
    public float posicionX;
    public float minPosicionX;
    public float maxPosicionX;
    public float minPosicionY;
    public float maxPosicionY;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        smoothTime = 0.2F;
        velocity = Vector3.zero; //Equivale a Vector3(0, 0, 0)
    }

    void Update()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(posicionX, posicionY, -10));

        // Smoothly move the camera towards that target position
        Vector3 posicionDeseada = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.position = new Vector3(
            Mathf.Clamp(posicionDeseada.x, minPosicionX, maxPosicionX), 
            Mathf.Clamp(posicionDeseada.y, minPosicionY, maxPosicionY), 
            posicionDeseada.z
        );
    }
}
