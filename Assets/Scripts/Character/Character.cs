using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Attributes
    private string playerName = "Player";
    
    [Tooltip("Health")]
    public int health = 0;
    
    [Tooltip("Attack Damage")]
    public int AD = 0;
    
    [Tooltip("Defence")]
    public int def = 0;

    [Tooltip("Level")]
    public int lvl = 1;

    [Tooltip("Experience")]
    public int exp = 0;

    //[Header("Resources")]
    //public int wood, metal, diamonds, gold;

    public enum ResourceType
    {
        Wood,
        Metal,
        Diamonds,
        Gold
    }

    [Header("ShipParts")]
    public List<ShipPartObject> shipParts = new List<ShipPartObject>();
    //Energy
    [Header("Energy")]
    public int coal = 0;

    //[Header("removelater")]

    public Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

    //Start is called before the first frame update
    void Awake()
    {

        resources.Add(ResourceType.Wood, 0);
        resources.Add(ResourceType.Metal, 0);
        resources.Add(ResourceType.Diamonds, 0);
        resources.Add(ResourceType.Gold, 0);


        ShipPartObject cannon = new ShipPartObject("Cannon", new List<ResourceType> { ResourceType.Metal, ResourceType.Gold });
        ShipPartObject engine = new ShipPartObject("Engine", new List<ResourceType> { ResourceType.Metal, ResourceType.Diamonds });
        ShipPartObject balloon = new ShipPartObject("Balloon", new List<ResourceType> { ResourceType.Wood, ResourceType.Metal });
        ShipPartObject bow = new ShipPartObject("Bow", new List<ResourceType> { ResourceType.Wood, ResourceType.Gold });

        shipParts.Add(cannon);
        shipParts.Add(engine);
        shipParts.Add(balloon);
        shipParts.Add(bow);


        //default resources for test
        resources[ResourceType.Wood] = 100;
        resources[ResourceType.Metal] = 100;
        resources[ResourceType.Diamonds] = 100;
        resources[ResourceType.Gold] = 100;
    }

    private void Start()
    {
        SetStats();
    }

    private void Update()
    {
        //debug log for all resources using dictionary
        Debug.Log("Wood: " + resources[ResourceType.Wood] + " Metal: " + resources[ResourceType.Metal] + " Diamonds: " + resources[ResourceType.Diamonds] + " Gold: " + resources[ResourceType.Gold]);


    }

    public void SetStats()
    {
        health = 0;
        AD = 0;
        def = 0;
        foreach (ShipPartObject part in shipParts)
        {
            health += part.health;
            AD += part.AD;
            def += part.def;
        }
    }

    public void LevelUpPart(ref int statToGrow, ShipPartObject partToUpgrade)
    {
        statToGrow += partToUpgrade.upgradeImprovement;

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
}
