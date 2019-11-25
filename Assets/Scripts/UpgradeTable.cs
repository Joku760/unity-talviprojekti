using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTable
{

    public List<Upgrade> upgrades;
    public int upgradeCount;

    public UpgradeTable()
    {
        upgrades = new List<Upgrade>();
        upgrades.Add(new Upgrade("Elixir of health", "Grants 10 maxHP", 25, "player", "maxHp", 10));
        upgrades.Add(new Upgrade("Armor plating", "Grants 2 armor", 50, "player", "armor", 2));
        upgrades.Add(new Upgrade("Boots of speed", "Increase movement speed", 65, "player", "speed", 1));
        upgrades.Add(new Upgrade("Gauntlets of strenght", "Increase melee damage", 100, "sword", "damage", 2));
        upgrades.Add(new Upgrade("Special gem", "Strenghtens special attacks", 80, "sword", "specialMultiplier", 1));
        upgrades.Add(new Upgrade("Barbed arrows", "Increase crossbow damage", 50, "crossBow", "damage", 5));
        upgradeCount = 6;
    }
}

public class Upgrade
{
    public string title;
    public string desc;
    public int price;
    public string targetObject;
    public string targetValue;
    public int value;

    public Upgrade(string title, string desc, int price, string targetObject, string targetValue, int value)
    {
        this.title = title;
        this.desc = desc;
        this.price = price;
        this.targetObject = targetObject;
        this.targetValue = targetValue;
        this.value = value;
    }
   
}
