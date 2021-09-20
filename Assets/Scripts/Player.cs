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
    public float smoothTime = 0.1f;

    bool takeMode = false;
    public int maxTakeDistance;
    public float force;
    public LayerMask part;
    Rigidbody objectRB;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        view = transform.Find("Main Camera").gameObject;
    }

    void Update()
    {
        ViewRotation();
        TakeObject();
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

        Quaternion bodyRotation = Quaternion.Euler(0f, inputDir.x, 0f);
        Quaternion viewRotation = Quaternion.Euler(Mathf.Clamp(inputDir.y, -70, 80), inputDir.x, 0f);
        
        view.transform.rotation = Quaternion.Slerp(view.transform.rotation, viewRotation, smoothTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, bodyRotation, smoothTime);
    }

    void TakeObject()
    {
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(view.transform.position, view.transform.forward, out hit, maxTakeDistance, part))
            {
                objectRB = hit.transform.gameObject.GetComponent<Rigidbody>();
                takeMode = true;
                Debug.Log("Take " + hit.transform.gameObject.name);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            takeMode = false;
            Debug.Log("Drop ");
        }

        if (takeMode)
        {
            objectRB.AddForce((inputDir - view.transform.forward) * force);
        }
    }

    void OnGUI()
    {
        GUILayout.Label("Speed: " + rb.velocity.magnitude);
    }
}
