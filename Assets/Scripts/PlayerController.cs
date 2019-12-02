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
    CrossBow crossBow;
    public int maxHp = 100;
    public int hp = 100;
    public Slider hpSlider;
    float temps;
    public Animator animator;
    public bool isDead = false;
    public GameObject gameOverScreen;
    public GameObject shopMenu;
    public int gold = 0;
    public int healthPotions = 0;
    Text potionText;
    public int armor = 0;
    AudioSource audioSource;
    public AudioClip swordslash;
    public AudioClip specialslash;
    public AudioClip dash;
    public AudioClip drink;
    public AudioClip hit;
    public AudioClip crossbow;
    public bool isWalking = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        sword = FindObjectOfType<Sword>();
        crossBow = FindObjectOfType<CrossBow>();
        speedModifier = 1;
        dashTime = 5;
        potionText = GameObject.Find("PotionAmount").GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("NormalAttack02_SwordShield"))
        {
            RotatePlayer();
           
            CheckAttackInput();
        }
        RotateCamera();
    }

    void FixedUpdate()
    {
        cameraRotator.transform.position = transform.position;
        if (isDead == false && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("NormalAttack02_SwordShield"))
        {
            MovePlayer();
        }
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
            isWalking = true;
        }
        else
        {
            animator.SetTrigger("Stop_Moving");
            isWalking = false;
        }


        Dodge();

    }

    void Dodge()
    {
        if (Input.GetKey(KeyCode.Space) && dashTime == 5 && dashCooldown == 50)
        {
            dashTime = 0;
            dashCooldown = 0;
            audioSource.clip = dash;
            audioSource.Play();
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
        if (dashTime != 5 && damage > 0)
        {

        }
        else
        {
            if(damage > 0)
            {
                damage = damage - armor;
                if (damage < 5) { damage = 5; }
            }
            
            hp = hp - damage;
            if (hp > maxHp) { hp = maxHp; }
            hpSlider.value = hp;
        }

        if(hp <= 0 )
        {
            //GAMEOVER
            animator.SetTrigger("Die");
            isDead = true;
            gameOverScreen.SetActive(true);
        }
        else if (damage > 0)
        {
            if (!(this.animator.GetCurrentAnimatorStateInfo(1).IsName("NormalAttack01_SwordShield") || this.animator.GetCurrentAnimatorStateInfo(0).IsName("NormalAttack02_SwordShield") || this.animator.GetCurrentAnimatorStateInfo(1).IsName("ShootAttack_CrossBow")))
            { 
                // Avoid any reload.
                animator.SetTrigger("Get_Hit");
            }         
            audioSource.clip = hit;
            audioSource.Play();
        }
    }

    public void Knockback(GameObject attacker, float force)
    {
        Vector3 knock = attacker.transform.forward;
        knock *= force;
        GetComponent<Rigidbody>().AddForce(knock);
    }

    public void CheckAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            temps = Time.time;
        }
        if (!(this.animator.GetCurrentAnimatorStateInfo(0).IsName("NormalAttack02_SwordShield") || this.animator.GetCurrentAnimatorStateInfo(1).IsName("ShootAttack_CrossBow")) && !shopMenu.activeSelf)
        {
            if (Input.GetMouseButtonUp(0) && (Time.time - temps) < 0.4)
            {
                // short Click
                animator.SetTrigger("Base_Attack");
                sword.PerformAttack();
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = swordslash;
                    audioSource.Play();
                }
            }
        }

        if (!(this.animator.GetCurrentAnimatorStateInfo(1).IsName("NormalAttack01_SwordShield") || this.animator.GetCurrentAnimatorStateInfo(0).IsName("NormalAttack02_SwordShield") || this.animator.GetCurrentAnimatorStateInfo(1).IsName("ShootAttack_CrossBow")) && !shopMenu.activeSelf)
        {

            if (Input.GetMouseButtonUp(0) && (Time.time - temps) > 0.4)
            {
                // Long Click
                animator.SetTrigger("Special_Attack");
                sword.SpecialAttack();
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = specialslash;
                    audioSource.Play();
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                crossBow.PerformAttack();
                audioSource.clip = crossbow;
                audioSource.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.X) && hp < 100)
        {
            if(healthPotions > 0)
            {
                healthPotions--;
                UpdateHp(-50);
                potionText.text = "HP Pots: " + healthPotions;
                audioSource.clip = drink;
                audioSource.Play();
            }
        }
    }

    public void updateUpgrade(string valueTarget, int value)
    {
        if (valueTarget == "armor")
        {
            armor = armor + value;
        }
        else if(valueTarget == "speed")
        {
            speed = speed + value;
        }
        else if(valueTarget == "maxHp")
        {
            maxHp = maxHp + value;
        }
    }
}
