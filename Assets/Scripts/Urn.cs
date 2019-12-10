using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urn : MonoBehaviour
{
    GameObject saveLoad;
    GameObject drop;
    int rollTheDie;

    void Start()
    {
        saveLoad = GameObject.Find("SaveLoad");
        saveLoad.GetComponent<SaveAndLoad>().ObjectToList(this.gameObject);
        drop = GameObject.Find("drop");
        rollTheDie = Random.Range(1, 100);
    }

    void Update()
    {
        
    }

    public void BreakUrn()
    {
        if (rollTheDie <= 20)
        {
        Instantiate(drop, new Vector3(transform.position.x, 0.01f, transform.position.z), drop.transform.rotation);
        }
        saveLoad.GetComponent<SaveAndLoad>().DeleteOnLoad(this.gameObject.transform.position);
        Destroy(gameObject, 0);
    }
}
