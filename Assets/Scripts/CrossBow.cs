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
    void Start()
    {

    }

    private void Update()
    {
        timer++;
    }
    public void PerformAttack()
    {

        if (timer >= timeBetweenAttacks)
        {
            timer = 0;
            Quaternion rotation = projectileSpawn.rotation;
            rotation *= Quaternion.Euler(0, 90, 0);
            GameObject arrowInstance = (GameObject)Instantiate(arrow, projectileSpawn.position, rotation);
            arrowInstance.GetComponent<Arrow>().Direction = projectileSpawn.forward;
        }
       
    }


}
