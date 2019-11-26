using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    // Use this for initialization
    PlayerController player;
    public new AudioSource audio;
    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isWalking && audio.isPlaying == false)
        {
            //audio.volume = Random.Range(0.7f, 1);
            //audio.pitch = Random.Range(0.9f, 1.1f);
            audio.Play();
        }
    }
}
