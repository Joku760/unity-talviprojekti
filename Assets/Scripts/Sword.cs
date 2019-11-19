using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // Start is called before the first frame update
    //Animator animator;
    int lastAttackDmg;
    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    public void PerformAttack()
    {
        lastAttackDmg = 10;
        //animator.SetTrigger("Base_Attack");
    }

    public void SpecialAttack()
    {
        lastAttackDmg = 15;
        //animator.SetTrigger("Special_Attack");
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
