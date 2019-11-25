using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    int lastAttackDmg;
    public int damage = 10;
    public double specialMultiplier = 1.5;
    void Start()
    {

    }

    public void PerformAttack()
    {
        lastAttackDmg = damage;
    }

    public void SpecialAttack()
    {
        lastAttackDmg = Mathf.RoundToInt((float)(damage * specialMultiplier));
    }

    public void updateUpgrade(string valueTarget, int value)
    {
        Debug.Log(valueTarget + " " + value);
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
        }

        if(other.tag == "Breakable")
        {
            //Break object
            Destroy(other.gameObject);
        }
    }
}
