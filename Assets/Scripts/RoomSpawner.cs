﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public int openingDirection;
    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    public float waitTime = 6f;
    private float spawnTime;
    GameObject saveLoad;
    bool spawnRooms;
    void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        spawnTime = Random.Range(0.1f, 0.4f);
        saveLoad = GameObject.Find("SaveLoad");
        spawnRooms = GameObject.Find("SaveLoad").GetComponent<SaveAndLoad>().spawnRooms;
        if(spawnRooms == true )
        {
            Invoke("Spawn", spawnTime);
        }
    }
 
    void Spawn()
    {
        spawnRooms = GameObject.Find("SaveLoad").GetComponent<SaveAndLoad>().spawnRooms;
        if (spawned == false && spawnRooms == true)
        {
            if (openingDirection == 1)
            {
                //spawn room with BOTTOM door
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                //spawn room with TOP door
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                //spawn room with LEFT door
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                //spawn room with RIGHT door
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            spawned = true;
            saveLoad.GetComponent<SaveAndLoad>().RoomSaver(openingDirection, rand, transform.position.x, transform.position.y, transform.position.z);

        }      
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SpawnPoint"))
        {
            if(other.GetComponent<RoomSpawner>().spawned == false && spawned == false)            
            {
                spawnRooms = GameObject.Find("SaveLoad").GetComponent<SaveAndLoad>().spawnRooms;
                if (spawnRooms == true)
                {
                    Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                    saveLoad.GetComponent<SaveAndLoad>().RoomSaver(5, rand, transform.position.x, transform.position.y, transform.position.z);
                }
                Destroy(gameObject);
            }
            other.GetComponent<RoomSpawner>().spawned = true;
            spawned = true;
        }
    }
}
