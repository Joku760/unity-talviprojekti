using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    Transform player;
    NavMeshAgent nav;
    PlayerController playerController;

    public float timeBetweenAttacks = 0.5f;     
    public int attackDamage = 10;
    float timer;
    int hp = 100;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        playerController = player.GetComponent<PlayerController>();
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

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.position);

        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && InFront())
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
