using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject closedRoom;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;
    bool spawnRooms;

    void Start()
    {
        spawnRooms = GameObject.Find("SaveLoad").GetComponent<SaveAndLoad>().spawnRooms;
    }
    void Update()
    {
        if(spawnRooms == true)
        {
            if (waitTime <= 0 && spawnedBoss == false)
            {
                Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
                spawnedBoss = true;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }     
    }
}
