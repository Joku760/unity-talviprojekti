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

    void Start()
    {
        player = GameObject.Find("Player");
        addGold = Random.Range(14, 26);
        goldText = GameObject.Find("GoldAmount").GetComponent<Text>();
        saveLoad = GameObject.Find("SaveLoad");
        saveLoad.GetComponent<SaveAndLoad>().PickupToList(this.gameObject);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && hover && HaveLineOfSight())
        {
            GiveGold();
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
    void GiveGold()
    {
        player.GetComponent<PlayerController>().gold = player.GetComponent<PlayerController>().gold + addGold;
        goldText.text = "Gold: " + player.GetComponent<PlayerController>().gold.ToString();
    }
}
