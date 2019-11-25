using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    GameObject player;
    bool hover = false;
    
    void Start()
    {
        player = GameObject.Find("Player");

    }
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && hover)
        {
            var hinge = GetComponent<HingeJoint>();
            //var motor = hinge.motor;
            hinge.useMotor = true;
        }
    }
    private void OnMouseEnter()
    {
        hover = true;
    }
    private void OnMouseExit()
    {
        hover = false;
    }

    bool HaveLineOfSight()
    {
        RaycastHit hit;
        Vector3 direction = player.transform.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out hit, 1))
        {
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
}
