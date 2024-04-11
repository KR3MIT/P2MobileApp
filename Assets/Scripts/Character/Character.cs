using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ShipPartObject;

public class Character : MonoBehaviour
{
    //Attributes
    [Header("Attributes")]
    public string playerName = "";

    [Tooltip("Level")]
    public int lvl = 1;

    [Tooltip("Experience")]
    public int exp = 0;

    [Tooltip("Health")]
    public int health = 0;
    
    [Tooltip("Attack Damage")]
    public int AD = 0;
    
    [Tooltip("Defence")]
    public int def = 0;

    [Tooltip("Level up stat scale")]
    public int lvlScale = 5;

    public int xpToLevel = 100;

    [Header("Actual start values changing the above wont do anything :) also modified by lvl")]
    public int defaultHealth = 100;
    public int defaultDef = 0;
    public int defaultAD = 10;

    //[Header("Resources")]
    //public int wood, metal, diamonds, gold;



    [Header("ShipParts")]
    public List<ShipPartObject> shipParts = new List<ShipPartObject>();

    public List<ShipPart> shipPartList = new List<ShipPart>();
    //Energy
    [Header("Energy")]
    public int coal = 0;

    [Header("Resource")]

    public Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int> 
    { 
        { ResourceType.Wood, 0 }, 
        { ResourceType.Metal, 0 }, 
        { ResourceType.Diamonds, 0 }, 
        { ResourceType.Gold, 0 } 
    };

    public enum ResourceType
    {
        Wood,
        Metal,
        Diamonds,
        Gold
    }

    private CloudSave cloudSave;

    //singleton
    public static Character instance { get; private set; }


    //Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }


        

        //resources.Add(ResourceType.Wood, 0);
        //resources.Add(ResourceType.Metal, 0);
        //resources.Add(ResourceType.Diamonds, 0);
        //resources.Add(ResourceType.Gold, 0);


        ShipPartObject cannon = new ShipPartObject("Cannon", new List<ResourceType> { ResourceType.Metal, ResourceType.Gold }, ShipPartObject.StatType.AD);
        ShipPartObject engine = new ShipPartObject("Engine", new List<ResourceType> { ResourceType.Metal, ResourceType.Diamonds }, ShipPartObject.StatType.def);
        ShipPartObject balloon = new ShipPartObject("Balloon", new List<ResourceType> { ResourceType.Wood, ResourceType.Metal }, ShipPartObject.StatType.health, 15);
        ShipPartObject ballista = new ShipPartObject("Ballista", new List<ResourceType> { ResourceType.Wood, ResourceType.Gold }, ShipPartObject.StatType.AD);

        shipParts.Add(cannon);
        shipParts.Add(engine);
        shipParts.Add(balloon);
        shipParts.Add(ballista);

        MakeStructFromClass(shipParts);//makes the shippartobjects stats into a struct such that it can be used with cloudsave

        //default resources for test
        resources[ResourceType.Wood] = 5;
        resources[ResourceType.Metal] = 5;
        resources[ResourceType.Diamonds] = 3;
        resources[ResourceType.Gold] = 7;
    }

    private void Start()
    {
        if(TryGetComponent(out CloudSave _cloudSave))
        {
            cloudSave = _cloudSave;
        }
        


        SetStats();
    }

    private void Update()
    {
        //debug log for all resources using dictionary
        //Debug.Log("Wood: " + resources[ResourceType.Wood] + " Metal: " + resources[ResourceType.Metal] + " Diamonds: " + resources[ResourceType.Diamonds] + " Gold: " + resources[ResourceType.Gold]);


        //foreach (ShipPartObject part in shipParts)
        //{
        //    Debug.Log("fuck " + part.partName + " lvl " + part.lvl);
        //}

        //foreach (KeyValuePair<ResourceType, int> kvp in resources)
        //{
        //    Debug.Log("fuck "+ kvp.Key + " value " + kvp.Value);
        //}
        
    }

    public void SetShipParts(List<ShipPartObject> loadedShipParts)
    {
        shipParts.Clear();
        foreach (ShipPartObject part in loadedShipParts)
        {
            shipParts.Add(part);
        }
        Debug.Log("Ship parts set count: " + shipParts.Count);
    }

    public void CreateAndSetShipPart(List<ShipPart> loadedShipParts)
    {
        shipParts.Clear();
        foreach(ShipPart part in loadedShipParts)
        {
            ShipPartObject shipPart = new ShipPartObject(part.partName, part.resourceTypes, part.statType, part.lvl, part.AD, part.def, part.health, part.upgradeImprovement);
            shipParts.Add(shipPart);
        }
        Debug.Log("Ship parts created count: " + shipParts.Count);
    }

    public void MakeStructFromClass(List<ShipPartObject> shipParts) 
    {
        shipPartList.Clear();
        foreach(ShipPartObject part in shipParts)
        {
            ShipPart shipPart = new ShipPart(part.partName, part.lvl, part.AD, part.def, part.health, part.upgradeTypes, part.statToUpgrade, part.upgradeImprovement);
            shipPartList.Add(shipPart);
        }
        Debug.Log("Ship parts struct created count: " + shipPartList.Count);
    }

    private void SaveData()
    {
        MakeStructFromClass(shipParts);

        if(cloudSave != null)
        {
            cloudSave.SaveData();
        }
        
    }

    public void SetStats()
    {
        health = defaultHealth + (lvlScale*lvl);
        AD = defaultAD + (lvlScale * lvl);
        def = defaultDef + (lvlScale * lvl);
        foreach (ShipPartObject part in shipParts)
        {
            health += part.health;
            AD += part.AD;
            def += part.def;
        }
    }

    public void LevelUpPart(ShipPartObject partToUpgrade)
    {
        switch (partToUpgrade.statToUpgrade)
        {
            case StatType.health:
                partToUpgrade.health += partToUpgrade.upgradeImprovement;
                break;
            case StatType.def:
                partToUpgrade.def += partToUpgrade.upgradeImprovement;
                break;
            case StatType.AD:
                partToUpgrade.AD += partToUpgrade.upgradeImprovement;
                break;
        }


        foreach (ResourceType type in partToUpgrade.upgradeTypes)//subtracts cost from resources
        {
            switch (type)
            {
                case ResourceType.Wood:
                    resources[ResourceType.Wood] -= (int)partToUpgrade.upgradeCost;
                    break;
                case ResourceType.Metal:
                    resources[ResourceType.Metal] -= (int)partToUpgrade.upgradeCost;
                    break;
                case ResourceType.Diamonds:
                    resources[ResourceType.Diamonds] -= (int)partToUpgrade.upgradeCost;
                    break;
                case ResourceType.Gold:
                    resources[ResourceType.Gold] -= (int)partToUpgrade.upgradeCost;
                    break;
            }
        }
        //Debug.Log("Wood: " + wood + " Metal: " + metal + " Diamonds: " + diamonds + " Gold: " + gold);
        partToUpgrade.upgradeCost *= partToUpgrade.upgradeCostScale;
        partToUpgrade.lvl++;
        SetStats();

        SaveData();
    }

    public bool CanLevelUp(ShipPartObject partToLevel)
    {
        foreach (ResourceType type in partToLevel.upgradeTypes)
        {
            switch (type)
            {
                case ResourceType.Wood:
                    if (resources[ResourceType.Wood] < partToLevel.upgradeCost) return false;
                    break;
                case ResourceType.Metal:
                    if (resources[ResourceType.Metal] < partToLevel.upgradeCost) return false;
                    break;
                case ResourceType.Diamonds:
                    if (resources[ResourceType.Diamonds] < partToLevel.upgradeCost) return false;
                    break;
                case ResourceType.Gold:
                    if (resources[ResourceType.Gold] < partToLevel.upgradeCost) return false;
                    break;
            }
        }

        return true;
    }

    public void AddResource(ResourceType resource, int amount)
    {
        resources[resource] += amount;
        Debug.Log("resources added " + resources[resource]);
    }

    public void AddResource(int xp)
    {
        exp += xp;
        if(exp >= xpToLevel)
        {
            exp -= xpToLevel;
            lvl++;
        }
    }
}
