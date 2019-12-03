﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenChest : MonoBehaviour
{
    GameObject player;
    bool hover = false;
    bool canOpen = true;
    AudioSource audioSource;
    public AudioClip chest;
    int addGold;
    Text goldText;
    GameObject saveLoad;
    Text potionText;
    int rollTheDie;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        rollTheDie = Random.Range(1, 100);
        goldText = GameObject.Find("GoldAmount").GetComponent<Text>();
        saveLoad = GameObject.Find("SaveLoad");
        potionText = GameObject.Find("PotionAmount").GetComponent<Text>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && hover && canOpen)
        {
            var hinge = GetComponent<HingeJoint>();
            //var motor = hinge.motor;
            hinge.useMotor = true;
            audioSource.clip = chest;
            audioSource.Play();
            canOpen = false;
            if(rollTheDie < 76)
            {
                addGold = Random.Range(14, 26);
                GiveGold();
            }
            else
            {
                GivePotion();
            }
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
    void GiveGold()
    {
        player.GetComponent<PlayerController>().gold = player.GetComponent<PlayerController>().gold + addGold;
        goldText.text = "Gold: " + player.GetComponent<PlayerController>().gold.ToString();
    }
    void GivePotion()
    {
        player.GetComponent<PlayerController>().healthPotions++;
        potionText.text = "HP Pots: " + player.GetComponent<PlayerController>().healthPotions.ToString();
    }
}
