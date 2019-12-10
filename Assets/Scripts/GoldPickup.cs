using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldPickup : MonoBehaviour
{
    bool hover = false;
    GameObject player;
    int addGold;
    Text goldText;
    GameObject saveLoad;
    AudioSource audioSource;
    public AudioClip money;
    MeshRenderer render;
    bool canPickUp = true;

    void Start()
    {
        player = GameObject.Find("Player");
        addGold = Random.Range(14, 26);
        goldText = GameObject.Find("GoldAmount").GetComponent<Text>();
        saveLoad = GameObject.Find("SaveLoad");
        saveLoad.GetComponent<SaveAndLoad>().ObjectToList(this.gameObject);
        audioSource = GetComponent<AudioSource>();
        render = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && hover && HaveLineOfSight() && canPickUp)
        {
            GiveGold();
            canPickUp = false;
            audioSource.clip = money;
            audioSource.Play();
            saveLoad.GetComponent<SaveAndLoad>().DeleteOnLoad(this.gameObject.transform.position);
            render.enabled = false;
            Destroy(this.gameObject, audioSource.clip.length);
            //gameObject.SetActive(false);
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
        player.GetComponent<PlayerController>().points = player.GetComponent<PlayerController>().points + addGold;
        PlayerPrefs.SetInt("Score", player.GetComponent<PlayerController>().points);
        goldText.text = "Gold: " + player.GetComponent<PlayerController>().gold.ToString();
    }
}
