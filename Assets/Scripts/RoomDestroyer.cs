using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDestroyer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("StartFloor"))
        {
            if(other.CompareTag("SpawnPoint"))
            {
                Destroy(other.gameObject);
            }
            else
            {
                Destroy(other.transform.parent.transform.parent.gameObject);
            }
        }
    }
}
