using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float sensitivity = 150f;
    Vector3 inputDir = Vector3.zero;
    Vector3 viewDir = Vector3.zero;
    public float smoothTime = 0.1f;

    void Start()
    {
        
    }

    void Update()
    {
        inputDir.x += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        inputDir.y -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        Quaternion viewRotation = Quaternion.Euler(Mathf.Clamp(inputDir.y, -80, 70), inputDir.x, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, viewRotation, smoothTime);
    }
}
