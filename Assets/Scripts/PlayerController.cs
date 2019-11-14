using System.Collections;
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
 
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        sword = FindObjectOfType<Sword>();
        speedModifier = 1;
        dashTime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        RotateCamera();

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            sword.PerformAttack();
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

    void UpdateHp(int damage)
    {
        hp = hp - damage;
        hpSlider.value = hp;
        
        if(hp <= 0 )
        {
            //GAMEOVER
            Debug.Log("GAMEOVER");
        }
    }
   
}