using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    Vector3 lookPos;
    Vector3 movement;
    Rigidbody rigidBody;
    Vector3 camInitialPosition;

    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        //cam = GetComponentInChildren<Camera>();
        rigidBody = GetComponent<Rigidbody>();
        camInitialPosition = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        float h = Input.mousePosition.x - Screen.width / 2;
        float v = Input.mousePosition.y - Screen.height / 2;
        float angle = -Mathf.Atan2(v, h) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, angle +90, 0);
    }

    void FixedUpdate()
    {
        cam.transform.position = transform.position;
        cam.transform.position += camInitialPosition;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal, 0, vertical);

        rigidBody.AddForce(movement * speed);
    } 
}