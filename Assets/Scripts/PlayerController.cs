using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    Vector3 lookPos;
    Vector3 movement;
    Rigidbody rigidBody;

    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        //cam = GetComponentInChildren<Camera>();
        rigidBody = GetComponent<Rigidbody>();     
    }

    // Update is called once per frame
    void Update()
    {
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            lookPos = hit.point;
        }

        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;

        transform.LookAt(transform.position + lookDir, Vector3.up);
    }

    void FixedUpdate()
    {
        cam.transform.position = transform.position;
        Vector3 temp = new Vector3(0, 6, -4);
        cam.transform.position += temp;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal, 0, vertical);

        rigidBody.velocity = movement * speed;
    } 
}
