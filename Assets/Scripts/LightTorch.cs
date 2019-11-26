using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTorch : MonoBehaviour
{
    Transform player;
    Light torchlight;
    GameObject saveLoad;
    bool unlit;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        saveLoad = GameObject.Find("SaveLoad");
        saveLoad.GetComponent<SaveAndLoad>().AllTorches(this.gameObject);
        torchlight = GetComponentInChildren<Light>();
        Unlight();
    }
    void FixedUpdate()
    {
        if (HaveLineOfSight() && unlit)
        {
            Light();
        }
    }

    public void Unlight()
    {
        torchlight.gameObject.SetActive(false);
        unlit = true;
    }

    public void Light()
    {
        torchlight.gameObject.SetActive(true);
        unlit = false;
        saveLoad.GetComponent<SaveAndLoad>().AddLitTorch(this.gameObject.transform.position);
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
