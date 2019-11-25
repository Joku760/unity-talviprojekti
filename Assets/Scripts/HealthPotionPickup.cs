using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPotionPickup : MonoBehaviour
{
    bool hover = false;
    GameObject player;
    Text potionText;
    GameObject saveLoad;
    AudioSource audioSource;
    public AudioClip potion;
    MeshRenderer render;

    void Start()
    {
        player = GameObject.Find("Player");
        potionText = GameObject.Find("PotionAmount").GetComponent<Text>();
        saveLoad = GameObject.Find("SaveLoad");
        saveLoad.GetComponent<SaveAndLoad>().PickupToList(this.gameObject);
        audioSource = GetComponent<AudioSource>();
        render = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && hover && HaveLineOfSight())
        {
            GivePotion();
            audioSource.clip = potion;
            audioSource.Play();
            saveLoad.GetComponent<SaveAndLoad>().PickupToDelete(this.gameObject.transform.position);
            gameObject.SetActive(false);
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
    void GivePotion()
    {
        player.GetComponent<PlayerController>().healthPotions++;
        potionText.text = "HP Pots: " + player.GetComponent<PlayerController>().healthPotions.ToString();
    }
}
