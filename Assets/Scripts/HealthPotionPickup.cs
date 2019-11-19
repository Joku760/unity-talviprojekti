using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPotionPickup : MonoBehaviour
{
    bool hover = false;
    Transform player;
    Text potionText;
    void Start()
    {
        player = GameObject.Find("Player").transform;
        potionText = GameObject.Find("PotionAmount").GetComponent<Text>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && hover && HaveLineOfSight())
        {
            GivePotion();
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
    void GivePotion()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().healthPotions++;
        potionText.text = "HP Pots: " + GameObject.Find("Player").GetComponent<PlayerController>().healthPotions.ToString();
    }
}
