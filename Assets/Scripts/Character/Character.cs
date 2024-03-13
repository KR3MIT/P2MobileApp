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

    public Dictionary<string, int> resources = new Dictionary<string, int>();

    ressourceBehavior wood = new ressourceBehavior("Wood", 1);
    //ressourceBehavior coal = new ressourceBehavior("Coal", 2, 1);
    ressourceBehavior sm = new ressourceBehavior("Scrap Metal", 1);
    ressourceBehavior diamond = new ressourceBehavior("Shiny Diamond", 1);
    ressourceBehavior gold = new ressourceBehavior("Gold Ingot", 1);

    //Start is called before the first frame update
    void Awake()
    {

        resources.Add("Wood", 0);
        resources.Add("Scrap Metal", 0);
        resources.Add("Shiny Diamond", 0);
        resources.Add("Gold Ingot", 0);


        ShipPartObject cannon = new ShipPartObject("Cannon", new List<ResourceType> { ResourceType.Metal, ResourceType.Gold });
        ShipPartObject engine = new ShipPartObject("Engine", new List<ResourceType> { ResourceType.Metal, ResourceType.Diamonds });
        ShipPartObject balloon = new ShipPartObject("Balloon", new List<ResourceType> { ResourceType.Wood, ResourceType.Metal });
        ShipPartObject bow = new ShipPartObject("Bow", new List<ResourceType> { ResourceType.Wood, ResourceType.Gold });

        shipParts.Add(cannon);
        shipParts.Add(engine);
        shipParts.Add(balloon);
        shipParts.Add(bow);

        resources["Wood"] = 100;
        resources["Scrap Metal"] = 100;
        resources["Shiny Diamond"] = 100;
        resources["Gold Ingot"] = 100;
        //set resources for testing remove in build
        //wood = 100;
        //metal = 100;
        //diamonds = 100;
        //gold = 100;
    }

    private void Start()
    {
        SetStats();
    }

    private void Update()
    {
        //debug log for all resources
        Debug.Log("Wood: " + resources["Wood"] + " Metal: " + resources["Scrap Metal"] + " Diamonds: " + resources["Shiny Diamond"] + " Gold: " + resources["Gold Ingot"]);

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
                    resources["Wood"] -= (int)partToUpgrade.upgradeCost;
                    break;
                case ResourceType.Metal:
                    resources["Scrap Metal"] -= (int)partToUpgrade.upgradeCost;
                    break;
                case ResourceType.Diamonds:
                    resources["Shiny Diamond"] -= (int)partToUpgrade.upgradeCost;
                    break;
                case ResourceType.Gold:
                    resources["Gold Ingot"] -= (int)partToUpgrade.upgradeCost;
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
                    if (resources["Wood"] < partToLevel.upgradeCost) return false;
                    break;
                case ResourceType.Metal:
                    if (resources["Scrap Metal"] < partToLevel.upgradeCost) return false;
                    break;
                case ResourceType.Diamonds:
                    if (resources["Shiny Diamond"] < partToLevel.upgradeCost) return false;
                    break;
                case ResourceType.Gold:
                    if (resources["Gold Ingot"] < partToLevel.upgradeCost) return false;
                    break;
            }
        }

        return true;
    }

    public void AddResource(string resource, int amount)
    {
        resources[resource] += amount;
        Debug.Log("resource debug " + resources[resource]);
    }
}
