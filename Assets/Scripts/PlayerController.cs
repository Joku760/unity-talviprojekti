﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public Camera cam;
    Vector3 movement;
    Rigidbody rigidBody;
    int speedModifier;
    double dashTime;
    public double dashCooldown = 50;
    public int dashSpeed = 4;
    public float speed = 5;
    public GameObject cameraRotator;
    Sword sword;
    public int hp = 100;
    public Slider hpSlider;
    float temps;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        sword = FindObjectOfType<Sword>();
        speedModifier = 1;
        dashTime = 5;
        //animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        RotateCamera();

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {


            temps = Time.time;
        }

        if (Input.GetMouseButtonUp(0) && (Time.time - temps) < 0.5)
        {
            // short Click
            animator.SetTrigger("Base_Attack");
            sword.PerformAttack();
        }

        if (Input.GetMouseButtonUp(0) && (Time.time - temps) > 0.5)
        {
            // Long Click
            animator.SetTrigger("Special_Attack");
            sword.SpecialAttack();
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void RotatePlayer()
    {
        /*float h = Input.mousePosition.x - Screen.width / 2;
        float v = Input.mousePosition.y - Screen.height / 2;
        float angle = -Mathf.Atan2(v, h) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, angle + 90, 0);
        */

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            if(hit.collider.tag != "Weapon")
            {
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }

        }

    }

    void MovePlayer()
    {
        cameraRotator.transform.position = transform.position;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal, 0, vertical);
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0.0f;
        movement = Vector3.ClampMagnitude(movement, 1);

        rigidBody.AddForce(movement *speed * speedModifier);
        if(Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            animator.SetTrigger("Run");
        }
        else
        {
            animator.SetTrigger("Stop_Moving");
        }
        

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

    void RotateCamera()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            cameraRotator.transform.Rotate(new Vector3(0, -1.5f, 0));
        }
        if (Input.GetKey(KeyCode.E))
        {
            cameraRotator.transform.Rotate(new Vector3(0, 1.5f, 0));
        }
    }

    public void UpdateHp(int damage)
    {
        //if(dashTime != 5 && damage > 0)
        //{

        //}
        //else
        //{
            hp = hp - damage;
            hpSlider.value = hp;
        //}

        if(hp <= 0 )
        {
            //GAMEOVER
            Debug.Log("GAMEOVER");
        }
    }

}
