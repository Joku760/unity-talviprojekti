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
    [SerializeField]
    private List<String> pickupsDelete = new List<String>();
    List<GameObject> pickupsAll = new List<GameObject>();

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
        data.pickupsDelete = pickupsDelete;

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
            pickupsDelete = data.pickupsDelete;
            Debug.Log(pickupsDelete.Count);
            foreach (String vectorString in pickupsDelete)
            {
                foreach(GameObject obj in pickupsAll)
                {
                    if(obj.transform.position.ToString().Equals(vectorString))
                    {
                        obj.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void PickupToDelete(Vector3 pos)
    {
        String vectorString = pos.ToString();
        pickupsDelete.Add(vectorString);
        Debug.Log(vectorString);
    }

    public void PickupToList(GameObject obj)
    {
        pickupsAll.Add(obj);
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
    public List<String> pickupsDelete = new List<String>();
    public int healthPotions = 0;
    public int gold = 0;
    public float posX = 0;
    public float posY = 0;
    public float posZ = 0;
    public int playerHP = 0;
}
