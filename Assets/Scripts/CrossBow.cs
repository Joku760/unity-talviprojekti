using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject arrow;
    public Transform projectileSpawn;
    int timer;
    int timeBetweenAttacks = 250;
    public Animator animator;
    void Start()
    {

    }

    void Update()
    {
        timer++;
    }
    public void PerformAttack()
    {

        if (timer >= timeBetweenAttacks)
        {
            timer = 0;
            animator.SetTrigger("Shoot_Attack");
            
        }   
    }

    public void Shoot()
    {
        Quaternion rotation = projectileSpawn.rotation;
        rotation *= Quaternion.Euler(0, 90, 0);
        GameObject arrowInstance = (GameObject)Instantiate(arrow, projectileSpawn.position, rotation);
        arrowInstance.GetComponent<Arrow>().Direction = projectileSpawn.forward;
    }

}
