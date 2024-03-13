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

    public float upgradeCost = 5;
    public List<ResourceType> upgradeTypes = new List<ResourceType>();
    public float upgradeCostScale = 2f;

    public int upgradeImprovement = 3;

    public int statToUpgrade;


    [HideInInspector] public GameObject instanciateShipPart;
    [HideInInspector] public GameObject instanciateShipCost;

    public ShipPartObject(string partName, List<ResourceType> upgradeTypes)
    {
        this.partName = partName;
        this.upgradeTypes = upgradeTypes;
    }
}
