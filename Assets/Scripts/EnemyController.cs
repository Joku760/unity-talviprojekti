using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    Transform player;
    Transform enemy;
    NavMeshAgent nav;
    PlayerController playerController;
    public Animator animator;
    public float timeBetweenAttacks = 0.5f;     
    public int attackDamage = 10;
    public int playerKnockbackForce = 500;
    float timer;
    int hp = 100;
    bool isAlive = true;
    AudioSource audioSource;
    public AudioClip slash;
    GameObject saveLoad;
    Vector3 startPos;
    GameObject drop;
    bool canDropGold = true;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        nav = GetComponent<NavMeshAgent>();
        playerController = player.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        saveLoad = GameObject.Find("SaveLoad");
        saveLoad.GetComponent<SaveAndLoad>().ObjectToList(this.gameObject);
        startPos = gameObject.transform.position;
        drop = GameObject.Find("drop");
    }

    private void OnDestroy() 
    {
        //Instantiate(drop, new Vector3(enemy.position.x, 0.01f , enemy.position.z), drop.transform.rotation); 
    }

    bool InFront()
    {

        Vector3 directionToTarget = transform.position - player.position;
        float distance = directionToTarget.magnitude;

        if (distance < 0.6)
        {
            return true;
        }
        return false;
    }

    bool WithinRange()
    {
        Vector3 directionToTarget = transform.position - player.position;
        float distance = directionToTarget.magnitude;

        if (distance < 5)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive && playerController.isDead == false && WithinRange())
        {
            nav.SetDestination(player.position);

            timer += Time.deltaTime;

            if (timer >= timeBetweenAttacks && InFront())
            {
                Attack();
                audioSource.clip = slash;
                audioSource.Play();
            }
        }
        else if (playerController.isDead)
        {
            animator.SetTrigger("Victory");
        }
        else 
        {
            nav.SetDestination(enemy.position);
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        playerController.UpdateHp(attackDamage);
        playerController.Knockback(gameObject, playerKnockbackForce);
        timer = 0f;
    }

    public void UpdateHp(int damage)
    {
        hp = hp - damage;

        if (hp <= 0)
        {
            saveLoad.GetComponent<SaveAndLoad>().DeleteOnLoad(startPos);
            Destroy(gameObject, 3);
            isAlive = false;
            animator.SetTrigger("Death");
            if (canDropGold)
            {
                Instantiate(drop, new Vector3(enemy.position.x, 0.01f, enemy.position.z), drop.transform.rotation);
                canDropGold = false;
            }
        }
    }
}
