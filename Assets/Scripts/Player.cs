using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1500f;
    Vector3 walkDir = Vector3.zero;
    
    Rigidbody rb;

    GameObject view;
    public float sensitivity = 100f;
    Vector3 inputDir = Vector3.zero;
    Vector3 viewDir = Vector3.zero;
    public float smoothTime = 0.1f;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        view = transform.Find("Main Camera").gameObject;
    }

    void Update()
    {
        ViewRotation();
    }

    void FixedUpdate()
    {
        Walk();
    }

    void Walk()
    {
        walkDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
        rb.AddRelativeForce(walkDir * speed * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    void ViewRotation()
    {
        inputDir.x += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        inputDir.y -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        Quaternion viewRotation = Quaternion.Euler(Mathf.Clamp(inputDir.y, -70, 80), inputDir.x, 0f);
        Quaternion bodyRotation = Quaternion.Euler(0f, inputDir.x, 0f);

        view.transform.rotation = Quaternion.Slerp(view.transform.rotation, viewRotation, smoothTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, bodyRotation, smoothTime);

        /*currentRotationX = view.transform.rotation.x;
        currentRotationY = transform.rotation.y;

        var tmp = Mathf.Clamp(currentRotationY + inputDir.y * Time.deltaTime * sensitivity, -80, 70);
        var rot = tmp - currentRotationY;
        view.transform.Rotate(-rot, 0f, 0f);

        transform.Rotate(0f, inputDir.x * Time.deltaTime * sensitivity, 0f);*/
    }

    void TakeObject()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

        }
    }

    void OnGUI()
    {
        GUILayout.Label("Speed: " + rb.velocity.magnitude);
    }
}
