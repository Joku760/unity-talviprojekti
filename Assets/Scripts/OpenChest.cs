using System.Collections;
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
        saveLoad.GetComponent<SaveAndLoad>().AllChests(this.gameObject);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && hover && canOpen && InFront())
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
            saveLoad.GetComponent<SaveAndLoad>().AddOpenedChest(this.gameObject.transform.position);
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

    bool InFront()
    {

        Vector3 directionToTarget = transform.position - player.transform.position;
        float distance = directionToTarget.magnitude;

        if (distance < 1)
        {
            return true;
        }
        return false;
    }

    void GiveGold()
    {
        player.GetComponent<PlayerController>().gold = player.GetComponent<PlayerController>().gold + addGold;
        player.GetComponent<PlayerController>().points = player.GetComponent<PlayerController>().points + addGold;
        PlayerPrefs.SetInt("Score", player.GetComponent<PlayerController>().points);
        goldText.text = "Gold: " + player.GetComponent<PlayerController>().gold.ToString();
    }
    void GivePotion()
    {
        player.GetComponent<PlayerController>().healthPotions++;
        potionText.text = "HP Pots: " + player.GetComponent<PlayerController>().healthPotions.ToString();
    }

    public void AlreadyOpen()
    {
        var hinge = GetComponent<HingeJoint>();
        hinge.useMotor = true;
        canOpen = false;
    }
}
