using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    Transform player;
    Text goldText;
    Text shopGoldText;
    Text potionText;
    bool hover = false;
    public GameObject hud;
    public GameObject shopMenu;
    public int potionPrice = 20;
    bool shopOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        goldText = GameObject.Find("GoldAmount").GetComponent<Text>(); 
        
        potionText = GameObject.Find("PotionAmount").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && shopOpen == true)
        { 
            if(Input.GetKey(KeyCode.Mouse0))
            {
                return;
            }
            else
            {
                shopOpen = false;
                shopMenu.SetActive(false);
                hud.SetActive(true);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.F) && hover && HaveLineOfSight())
        {
            hud.SetActive(false);
            shopMenu.SetActive(true);
            shopOpen = true;
            shopGoldText = GameObject.Find("ShopGold").GetComponent<Text>();
            shopGoldText.text = "Gold: " + GameObject.Find("Player").GetComponent<PlayerController>().gold.ToString();
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

        if (Physics.Raycast(transform.position, direction, out hit, 100))
        {
            if (hit.collider.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    public void BuyPotion()
    {
        int gold = GameObject.Find("Player").GetComponent<PlayerController>().gold = GameObject.Find("Player").GetComponent<PlayerController>().gold;
        if(gold >= potionPrice)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().gold = GameObject.Find("Player").GetComponent<PlayerController>().gold - potionPrice;
            GameObject.Find("Player").GetComponent<PlayerController>().healthPotions++;
            goldText.text = "Gold: " + GameObject.Find("Player").GetComponent<PlayerController>().gold.ToString();
            shopGoldText.text = "Gold: " + GameObject.Find("Player").GetComponent<PlayerController>().gold.ToString();
            potionText.text = "HP Pots: " + GameObject.Find("Player").GetComponent<PlayerController>().healthPotions.ToString();
        }
    }

    public void Upgrade1()
    {
        int gold = GameObject.Find("Player").GetComponent<PlayerController>().gold = GameObject.Find("Player").GetComponent<PlayerController>().gold;
        if (gold >= 50)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().gold = GameObject.Find("Player").GetComponent<PlayerController>().gold - 50;
            goldText.text = "Gold: " + GameObject.Find("Player").GetComponent<PlayerController>().gold.ToString();
            shopGoldText.text = "Gold: " + GameObject.Find("Player").GetComponent<PlayerController>().gold.ToString();
            GameObject.Find("Player").GetComponent<PlayerController>().armor = GameObject.Find("Player").GetComponent<PlayerController>().armor + 10;
        }
    }
}
