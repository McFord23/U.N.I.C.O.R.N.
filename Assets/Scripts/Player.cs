using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed = 2f;
    public float speed = 1000f;
    public float gear = 2f;
    Vector3 walkDir = Vector3.zero;
    
    Rigidbody rb;

    public float sensitivity = 1f;
    Vector3 viewDir = Vector3.zero;
    GameObject view;
    Rigidbody rbView;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        view = transform.Find("Main Camera").gameObject;
        //rbView = view.GetComponent<Rigidbody>();
    }

    void Update()
    {
        RotateView();
    }

    void FixedUpdate()
    {
        Walk();
    }

    void Walk()
    {
        walkDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
        
        if (rb.velocity.magnitude < maxSpeed) rb.AddRelativeForce(gear * walkDir * speed);
        else rb.AddRelativeForce(walkDir * speed);
    }

    void RotateView()
    {
        viewDir += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0f);

        //rbView.angularVelocity += viewDir.normalized * sensitivity;
        
        //rbView.AddRelativeTorque(viewDir * sensitivity);


        if (view.transform.rotation.x > 85 && viewDir.x > 0)
        {
            view.transform.rotation = Quaternion.Euler(viewDir.x * sensitivity, viewDir.y * sensitivity, 0f);
            transform.rotation = Quaternion.Euler(0, viewDir.y * sensitivity, 0);
        }
        else if (view.transform.rotation.x < -85 && viewDir.x < 0)
        {
            view.transform.rotation = Quaternion.Euler(viewDir.x * sensitivity, viewDir.y * sensitivity, 0f);
            transform.rotation = Quaternion.Euler(0, viewDir.y * sensitivity, 0);
        }
        else if (view.transform.rotation.x > -85 && view.transform.rotation.x < 85)
        {
            view.transform.rotation = Quaternion.Euler(viewDir.x * sensitivity, viewDir.y * sensitivity, 0f);
            transform.rotation = Quaternion.Euler(0, viewDir.y * sensitivity, 0);
        }

        
    }

    void OnGUI()
    {
        GUILayout.Label("Speed: " + rb.velocity.magnitude);
    }
}
