using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    int lastAttackDmg;
    public int damage = 10;
    public int specialMultiplier = 5;
    AudioSource audioSource;
    public AudioClip glass;
    public AudioClip enemyhit;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PerformAttack()
    {
        lastAttackDmg = damage;
    }

    public void SpecialAttack()
    {
        lastAttackDmg = damage + specialMultiplier;
    }

    public void updateUpgrade(string valueTarget, int value)
    {
        if(valueTarget == "damage")
        {
            damage = damage + value;
        }
        else if (valueTarget == "specialMultiplier")
        {
            specialMultiplier = specialMultiplier + value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player")
        {
            //Debug.Log("Hit: " + other);
        }

        if(other.tag == "Enemy")
        {
            //Do damage
            other.gameObject.GetComponent<EnemyController>().UpdateHp(lastAttackDmg);
            audioSource.clip = enemyhit;
            audioSource.Play();
        }

        if(other.tag == "Breakable")
        {
            //Break object
            Destroy(other.gameObject);
            audioSource.clip = glass;
            audioSource.Play();
        }
    }
}
