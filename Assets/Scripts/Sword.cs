using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PerformAttack()
    {
        animator.SetTrigger("Base_Attack");
    }

    public void SpecialAttack()
    {
        animator.SetTrigger("Special_Attack");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player")
        {
            Debug.Log("Hit: " + other);
        }
        
        if(other.tag == "Enemy")
        {
            //Do damage
        }

        if(other.tag == "Breakable")
        {
            //Break object
        }
    }
}
