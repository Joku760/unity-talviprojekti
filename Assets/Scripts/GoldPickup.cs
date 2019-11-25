using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldPickup : MonoBehaviour
{
    bool hover = false;
    Transform player;
    int addGold;
    Text goldText;
    AudioSource audioSource;
    public AudioClip money;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        addGold = Random.Range(14, 26);
        goldText = GameObject.Find("GoldAmount").GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && hover && HaveLineOfSight())
        {
            GiveGold();
            audioSource.clip = money;
            audioSource.Play();
            Destroy(this.gameObject, 0);
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
        GameObject.Find("Player").GetComponent<PlayerController>().gold = GameObject.Find("Player").GetComponent<PlayerController>().gold + addGold;
        goldText.text = "Gold: " + GameObject.Find("Player").GetComponent<PlayerController>().gold.ToString();
    }
}
