using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    GameObject player;
    Transform playerTransform;
    Text goldText;
    Text shopGoldText;
    Text potionText;
    bool hover = false;
    public GameObject hud;
    public GameObject shopMenu;
    public int potionPrice = 20;
    bool shopOpen = false;
    Upgrade upgrade1;
    Upgrade upgrade2;
    Upgrade upgrade3;
    Sword sword;
    CrossBow crossBow;
    PlayerController playerController;
    public CanvasGroup upgradePanel1;
    public CanvasGroup upgradePanel2;
    public CanvasGroup upgradePanel3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.transform;
        goldText = GameObject.Find("GoldAmount").GetComponent<Text>(); 
        potionText = GameObject.Find("PotionAmount").GetComponent<Text>();
        sword = FindObjectOfType<Sword>();
        playerController = FindObjectOfType<PlayerController>();
        crossBow = FindObjectOfType<CrossBow>();
        GetUpgrades();
        
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
            shopGoldText.text = "Gold: " + player.GetComponent<PlayerController>().gold.ToString();
            SetUpgrades();
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
        Vector3 direction = playerTransform.transform.position - transform.position;

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
        int gold = player.GetComponent<PlayerController>().gold = player.GetComponent<PlayerController>().gold;
        if(gold >= potionPrice)
        {
            player.GetComponent<PlayerController>().gold = player.GetComponent<PlayerController>().gold - potionPrice;
            GameObject.Find("Player").GetComponent<PlayerController>().healthPotions++;
            goldText.text = "Gold: " + player.GetComponent<PlayerController>().gold.ToString();
            shopGoldText.text = "Gold: " + player.GetComponent<PlayerController>().gold.ToString();
            potionText.text = "HP Pots: " + player.GetComponent<PlayerController>().healthPotions.ToString();
        }
    }

    public void Upgrade1()
    {
        int gold = player.GetComponent<PlayerController>().gold = player.GetComponent<PlayerController>().gold;
        if (gold >= upgrade1.price)
        {
            player.GetComponent<PlayerController>().gold = player.GetComponent<PlayerController>().gold - upgrade1.price;
            goldText.text = "Gold: " + player.GetComponent<PlayerController>().gold.ToString();
            shopGoldText.text = "Gold: " + player.GetComponent<PlayerController>().gold.ToString();
            upgradePanel1.alpha = 0f;
            upgradePanel1.blocksRaycasts = false;
            if(upgrade1.targetObject == "player")
            {
                playerController.updateUpgrade(upgrade1.targetValue, upgrade1.value);
            }
            else if(upgrade1.targetObject == "sword")
            {
                sword.updateUpgrade(upgrade1.targetValue, upgrade1.value);
            }
            else if(upgrade1.targetObject == "crossBow")
            {
                crossBow.updateUpgrade(upgrade1.targetValue, upgrade1.value);
            }
        }
    }

    public void Upgrade2()
    {
        int gold = player.GetComponent<PlayerController>().gold = player.GetComponent<PlayerController>().gold;
        if (gold >= upgrade2.price)
        {
            player.GetComponent<PlayerController>().gold = player.GetComponent<PlayerController>().gold - upgrade2.price;
            goldText.text = "Gold: " + player.GetComponent<PlayerController>().gold.ToString();
            shopGoldText.text = "Gold: " + player.GetComponent<PlayerController>().gold.ToString();
            upgradePanel2.alpha = 0f;
            upgradePanel2.blocksRaycasts = false;
            if (upgrade2.targetObject == "player")
            {
                playerController.updateUpgrade(upgrade2.targetValue, upgrade2.value);
            }
            else if (upgrade2.targetObject == "sword")
            {
                sword.updateUpgrade(upgrade2.targetValue, upgrade2.value);
            }
            else if (upgrade2.targetObject == "crossBow")
            {
                crossBow.updateUpgrade(upgrade2.targetValue, upgrade2.value);
            }
        }
    }
    public void Upgrade3()
    {
        int gold = player.GetComponent<PlayerController>().gold = player.GetComponent<PlayerController>().gold;
        if (gold >= upgrade3.price)
        {
            player.GetComponent<PlayerController>().gold = player.GetComponent<PlayerController>().gold - upgrade3.price;
            goldText.text = "Gold: " + player.GetComponent<PlayerController>().gold.ToString();
            shopGoldText.text = "Gold: " + player.GetComponent<PlayerController>().gold.ToString();
            upgradePanel3.alpha = 0f;
            upgradePanel3.blocksRaycasts = false;
            if (upgrade3.targetObject == "player")
            {
                playerController.updateUpgrade(upgrade3.targetValue, upgrade3.value);
            }
            else if (upgrade3.targetObject == "sword")
            {
                sword.updateUpgrade(upgrade3.targetValue, upgrade3.value);
            }
            else if (upgrade3.targetObject == "crossBow")
            {
                crossBow.updateUpgrade(upgrade3.targetValue, upgrade3.value);
            }
        }
    }

    public void GetUpgrades()
    {
        UpgradeTable upgradeTable = new UpgradeTable();
        int upgradeCount1;
        int upgradeCount2;
        int upgradeCount3;
        do {
            upgradeCount1 = Random.Range(0, upgradeTable.upgradeCount);
            upgradeCount2 = Random.Range(0, upgradeTable.upgradeCount);
            upgradeCount3 = Random.Range(0, upgradeTable.upgradeCount);
        } while (upgradeCount1 == upgradeCount2 || upgradeCount2 == upgradeCount3 || upgradeCount1 == upgradeCount3);
        

        upgrade1 = upgradeTable.upgrades[upgradeCount1];
        upgrade2 = upgradeTable.upgrades[upgradeCount2];
        upgrade3 = upgradeTable.upgrades[upgradeCount3];

        upgradePanel1.alpha = 1f;
        upgradePanel1.blocksRaycasts = true;
        upgradePanel2.alpha = 1f;
        upgradePanel2.blocksRaycasts = true;
        upgradePanel3.alpha = 1f;
        upgradePanel3.blocksRaycasts = true;
    }
    public void SetUpgrades()
    {
        Text upgradeTitle1 = GameObject.Find("UpgradeTitle1").GetComponent<Text>();
        Text upgradeDesc1 = GameObject.Find("UpgradeDesc1").GetComponent<Text>();
        Text upgradePrice1 = GameObject.Find("Upgradeprice1").GetComponent<Text>();

        Text upgradeTitle2 = GameObject.Find("UpgradeTitle2").GetComponent<Text>();
        Text upgradeDesc2 = GameObject.Find("UpgradeDesc2").GetComponent<Text>();
        Text upgradePrice2 = GameObject.Find("Upgradeprice2").GetComponent<Text>();

        Text upgradeTitle3 = GameObject.Find("UpgradeTitle3").GetComponent<Text>();
        Text upgradeDesc3 = GameObject.Find("UpgradeDesc3").GetComponent<Text>();
        Text upgradePrice3 = GameObject.Find("Upgradeprice3").GetComponent<Text>();

        //set texts from upgrade objects
        upgradeTitle1.text = upgrade1.title;
        upgradeDesc1.text = upgrade1.desc;
        upgradePrice1.text = upgrade1.price + "g";

        upgradeTitle2.text = upgrade2.title;
        upgradeDesc2.text = upgrade2.desc;
        upgradePrice2.text = upgrade2.price + "g";

        upgradeTitle3.text = upgrade3.title;
        upgradeDesc3.text = upgrade3.desc;
        upgradePrice3.text = upgrade3.price + "g";

    }
}
