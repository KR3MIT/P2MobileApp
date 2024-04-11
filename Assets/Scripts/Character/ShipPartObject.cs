using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Character;

public class ShipPartObject
{
    public string partName;
    public int lvl;
    public int AD;
    public int def;
    public int health;

    public enum StatType
    {
        health,
        def,
        AD
    }
    public StatType statToUpgrade;//which stat upgrades
    public int upgradeImprovement = 3; //how much stat improves


    public float upgradeCost = 5; //original upgrade cost
    public List<ResourceType> upgradeTypes = new List<ResourceType>();
    public float upgradeCostScale = 2f; //upgrade cost scale

    


    [HideInInspector] public GameObject instanciateShipPart;
    [HideInInspector] public GameObject instanciateShipCost;

    public ShipPartObject(string partName, List<ResourceType> upgradeTypes, StatType statType)
    {
        this.partName = partName;
        this.upgradeTypes = upgradeTypes;
        this.statToUpgrade = statType;
    }

    public ShipPartObject(string partName, List<ResourceType> upgradeTypes, StatType statType, int upgradeImprovement)
    {
        this.partName = partName;
        this.upgradeTypes = upgradeTypes;
        this.statToUpgrade = statType;
        this.upgradeImprovement = upgradeImprovement;
    }

    public ShipPartObject(string partName, List<ResourceType> upgradeTypes, StatType statType, int lvl, int ad, int def, int health, int upgradeImprovement)
    {
        this.partName = partName;
        this.upgradeTypes = upgradeTypes;
        this.statToUpgrade = statType;
        this.lvl = lvl;
        this.AD = ad;
        this.def = def;
        this.health = health;
        this.upgradeImprovement = upgradeImprovement;
    }
}
public struct ShipPart
{
    public string partName;
    public int lvl;
    public int AD;
    public int def;
    public int health;
    public List<ResourceType> resourceTypes;
    public ShipPartObject.StatType statType;
    public int upgradeImprovement;

    public ShipPart(string partName, int lvl, int ad, int def, int health, List<ResourceType> upgradeTypes, ShipPartObject.StatType statType, int upgradeImprovement)
    {
        this.partName = partName;
        this.lvl = lvl;
        this.AD = ad;
        this.def = def;
        this.health = health;
        this.resourceTypes = upgradeTypes;
        this.statType = statType;
        this.upgradeImprovement = upgradeImprovement;
    }
}
