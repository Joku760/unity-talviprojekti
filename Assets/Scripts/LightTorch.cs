using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTorch : MonoBehaviour
{
    Transform player;
    Light torchlight;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        torchlight = GetComponentInChildren<Light>();
        torchlight.gameObject.SetActive(false);
    }
    void FixedUpdate()
    {
        if (HaveLineOfSight())
        {
            torchlight.gameObject.SetActive(true);
        }
    }

    bool HaveLineOfSight()
    {
        RaycastHit hit;
        Vector3 direction = player.transform.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out hit, 3))
        {
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
}
