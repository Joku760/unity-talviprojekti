using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    Transform player;
    GameObject playerObject;
    NavMeshAgent nav;
    PlayerController playerController;
    bool playerInRange;

    public float timeBetweenAttacks = 0.5f;     
    public int attackDamage = 10;
    float timer;
    int hp = 100;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        playerController = player.GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == playerObject)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == playerObject)
        {
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.position);

        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange)
        {
            Attack();
        }
    }

    void Attack()
    {
        timer = 0f;

        playerController.UpdateHp(attackDamage);
    }

    public void UpdateHp(int damage)
    {
        hp = hp - damage;

        if (hp <= 0)
        {
            Destroy(gameObject, 5);
        }
    }
}
