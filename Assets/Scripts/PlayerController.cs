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
    int speedModifier;
    double dashTime;
    public double dashCooldown = 50;
    public int dashSpeed = 4;
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        //cam = GetComponentInChildren<Camera>();
        rigidBody = GetComponent<Rigidbody>();
        camInitialPosition = cam.transform.position - transform.position;
        speedModifier = 1;
        dashTime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
    }

    void FixedUpdate()
    {
        MovePlayer();

       /* if (Input.GetKeyDown("space"))
        {
            Dodge();
               
            }
        */
    } 

    void RotatePlayer()
    {
        float h = Input.mousePosition.x - Screen.width / 2;
        float v = Input.mousePosition.y - Screen.height / 2;
        float angle = -Mathf.Atan2(v, h) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, angle + 90, 0);
    }

    void MovePlayer()
    {
        cam.transform.position = transform.position;
        cam.transform.position += camInitialPosition;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal, 0, vertical);

        rigidBody.AddForce(movement * speed * speedModifier);

        Dodge();
        
    }

    void Dodge()
    {
        if (Input.GetKey(KeyCode.Space) && dashTime == 5 && dashCooldown == 50)
        {
            dashTime = 0;
            dashCooldown = 0;
        }

        if (dashTime < 5)
        {
            speedModifier = dashSpeed;
            dashTime++;
        }

        if (dashTime == 5)
        {
            speedModifier = 1;
        }
        if (dashCooldown < 50)
        {
            dashCooldown++;
        }
    }
}