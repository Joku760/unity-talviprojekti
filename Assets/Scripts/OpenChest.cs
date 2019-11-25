using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    bool hover = false;
    AudioSource audioSource;
    public AudioClip chest;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && hover)
        {
            var hinge = GetComponent<HingeJoint>();
            //var motor = hinge.motor;
            hinge.useMotor = true;
            audioSource.clip = chest;
            audioSource.Play();
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
}
