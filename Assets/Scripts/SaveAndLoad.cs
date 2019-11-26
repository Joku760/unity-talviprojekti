using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class SaveAndLoad : MonoBehaviour
{
    GameObject player;
    private List<String> onLoadDelete = new List<String>();
    List<GameObject> interactablesAll = new List<GameObject>();
    private List<String> litTorches = new List<String>();
    List<GameObject> allTorches = new List<GameObject>();
    List<String> loadedLitTorches = new List<String>();

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Load();
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savefile.dat");

        SaveData data = new SaveData();
        data.healthPotions = player.GetComponent<PlayerController>().healthPotions;
        data.gold = player.GetComponent<PlayerController>().gold;
        data.posX = player.transform.position.x;
        data.posY = player.transform.position.y;
        data.posZ = player.transform.position.z;
        data.playerHP = player.GetComponent<PlayerController>().hp;
        data.playerMaxHP = player.GetComponent<PlayerController>().maxHp;
        data.armor = player.GetComponent<PlayerController>().armor;
        data.speed = player.GetComponent<PlayerController>().speed;
        data.onLoadDelete = onLoadDelete;
        data.litTorches = litTorches;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/savefile.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savefile.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            player.GetComponent<PlayerController>().healthPotions = data.healthPotions;
            GameObject.Find("PotionAmount").GetComponent<Text>().text = "HP Pots: " + data.healthPotions.ToString();
            player.GetComponent<PlayerController>().gold = data.gold;
            GameObject.Find("GoldAmount").GetComponent<Text>().text = "Gold: " + data.gold.ToString();
            player.transform.position = new Vector3(data.posX, data.posY, data.posZ);
            player.GetComponent<PlayerController>().hp = 100;
            player.GetComponent<PlayerController>().UpdateHp(100 - data.playerHP);
            player.GetComponent<PlayerController>().maxHp = data.playerMaxHP;
            player.GetComponent<PlayerController>().armor = data.armor;
            player.GetComponent<PlayerController>().speed = data.speed;
            onLoadDelete = data.onLoadDelete;
            loadedLitTorches = data.litTorches;
            foreach (String vectorString in onLoadDelete)
            {
                foreach(GameObject obj in interactablesAll)
                {
                    if(obj.transform.position.ToString().Equals(vectorString))
                    {
                        obj.gameObject.SetActive(false);
                    }
                }
            }
            foreach (String vectorString in loadedLitTorches)
            {
                foreach (GameObject obj in allTorches)
                {
                    if (obj.transform.position.ToString().Equals(vectorString))
                    {
                        obj.GetComponent<LightTorch>().Light();
                    }
                }
            }
        }
    }

    public void DeleteOnLoad(Vector3 pos)
    {
        String vectorString = pos.ToString();
        onLoadDelete.Add(vectorString);
    }

    public void ObjectToList(GameObject obj)
    {
        interactablesAll.Add(obj);
    }

    public void AddLitTorch(Vector3 pos)
    {
        String vectorString = pos.ToString();
        litTorches.Add(vectorString);
    }

    public void AllTorches(GameObject obj)
    {
        allTorches.Add(obj);
    }
    /*
    public Vector3 stringToVec(string s)
    {
        string[] temp = s.Substring(1, s.Length - 2).Split(',');
        return new Vector3(float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]));
    }
    */
}

[Serializable]
public class SaveData
{
    public List<String> onLoadDelete = new List<String>();
    public List<String> litTorches = new List<String>();
    public int healthPotions = 0;
    public int gold = 0;
    public float posX = 0;
    public float posY = 0;
    public float posZ = 0;
    public int playerHP = 0;
    public int playerMaxHP = 0;
    public int armor = 0;
    public float speed = 0;
}
