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
    GameObject sword;
    int loadBool;
    private List<String> onLoadDelete = new List<String>();
    List<GameObject> interactablesAll = new List<GameObject>();
    private List<String> litTorches = new List<String>();
    List<GameObject> allTorches = new List<GameObject>();
    List<String> loadedLitTorches = new List<String>();
    List<int> openDirectionList = new List<int>();
    List<int> roomRandList = new List<int>();
    List<String> roomSpawnPosList = new List<String>();
    List<int> loadedOpenDirectionList = new List<int>();
    List<int> loadedRoomRandList = new List<int>();
    List<String> loadedRoomSpawnPosList = new List<String>();
    private RoomTemplates templates;
    bool roomLoaded = false;
    public Boolean spawnRooms = true;
    List<GameObject> chestsAll = new List<GameObject>();
    List<String> openChests = new List<String>();

    void Start()
    {
        player = GameObject.Find("Player");
        sword = GameObject.Find("PolyartSword");
        loadBool = PlayerPrefs.GetInt("SaveLoadBoolean");
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        if (loadBool == 1)
        {
            PlayerPrefs.SetInt("SaveLoadBoolean", 0);
            Load();
        }
    }

    void Update()
    {
        if (loadBool == 1)
        {
            loadBool = 0;
            Load();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Save();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Mainmenu");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Mainmenu");
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
        data.boltDamage = GameObject.Find("RPGHeroPolyart").GetComponent<CrossBow>().damage;
        data.swordDamage = sword.GetComponent<Sword>().damage;
        data.swordMultiplier = sword.GetComponent<Sword>().specialMultiplier;
        data.openDirectionList = openDirectionList;
        data.roomRandList = roomRandList;
        data.roomSpawnPosList = roomSpawnPosList;
        data.openChests = openChests;

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
            sword.GetComponent<Sword>().damage = data.swordDamage;
            sword.GetComponent<Sword>().specialMultiplier = data.swordMultiplier;
            GameObject.Find("RPGHeroPolyart").GetComponent<CrossBow>().damage = data.boltDamage;
            onLoadDelete = data.onLoadDelete;
            loadedLitTorches = data.litTorches;
            loadedOpenDirectionList = data.openDirectionList;
            loadedRoomRandList = data.roomRandList;
            loadedRoomSpawnPosList = data.roomSpawnPosList;
            if(roomLoaded == false)
            {
                roomLoaded = true;
                spawnRooms = false;
                RoomLoader();
            }
            openChests = data.openChests;
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
            foreach (String vectorString in openChests)
            {
                foreach (GameObject obj in chestsAll)
                {
                    if (obj.transform.position.ToString().Equals(vectorString))
                    {
                        obj.GetComponent<OpenChest>().AlreadyOpen();
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

    public void RoomSaver(int direction, int randNum, float posX, float posY, float posZ)
    {
        openDirectionList.Add(direction);
        roomRandList.Add(randNum);
        String vectorString = posX + "," + posY + "," + posZ;
        Debug.Log(vectorString + " " + posX);
        roomSpawnPosList.Add(vectorString);
    }

    public void RoomLoader()
    {
        int openDirection;
        Vector3 roomSpawnPos;
        int roomRand;
        string vectorString;
        for(int i = 0; i < loadedOpenDirectionList.Count; i++)
        {
            openDirection = loadedOpenDirectionList[i];
            roomRand = loadedRoomRandList[i];
            vectorString = loadedRoomSpawnPosList[i];
            roomSpawnPos = stringToVec(vectorString);

            if (openDirection == 1)
            {
                //spawn room with BOTTOM door
                Instantiate(templates.bottomRooms[roomRand], roomSpawnPos, templates.bottomRooms[roomRand].transform.rotation);
            }
            else if (openDirection == 2)
            {
                //spawn room with TOP door
                Instantiate(templates.topRooms[roomRand], roomSpawnPos, templates.topRooms[roomRand].transform.rotation);
            }
            else if (openDirection == 3)
            {
                //spawn room with LEFT door
                Instantiate(templates.leftRooms[roomRand], roomSpawnPos, templates.leftRooms[roomRand].transform.rotation);
            }
            else if (openDirection == 4)
            {
                //spawn room with RIGHT door
                Instantiate(templates.rightRooms[roomRand], roomSpawnPos, templates.rightRooms[roomRand].transform.rotation);
            }
        }
    }

    public Vector3 stringToVec(string s)
    {
        if (s.StartsWith("(") && s.EndsWith(")"))
        {
            s = s.Substring(1, s.Length - 2);
        }

        // split the items
        string[] sArray = s.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }

    public void AllChests(GameObject obj)
    {
        chestsAll.Add(obj);
    }

    public void AddOpenedChest(Vector3 pos)
    {
        String vectorString = pos.ToString();
        openChests.Add(vectorString);
    }

}

[Serializable]
public class SaveData
{
    public List<String> onLoadDelete = new List<String>();
    public List<String> litTorches = new List<String>();
    public List<int> openDirectionList = new List<int>();
    public List<int> roomRandList = new List<int>();
    public List<String> roomSpawnPosList = new List<String>();
    public List<String> openChests = new List<String>();
    public int healthPotions = 0;
    public int gold = 0;
    public float posX = 0;
    public float posY = 0;
    public float posZ = 0;
    public int playerHP = 0;
    public int playerMaxHP = 0;
    public int armor = 0;
    public float speed = 0;
    public int boltDamage = 0;
    public int swordDamage = 0;
    public int swordMultiplier = 0;
}
