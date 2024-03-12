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

    [Header("Resources")]
    public int wood = 0;
    public int metal = 0;
    public int diamonds = 0;
    public int gold = 0;

    //Energy
    public int coal = 0;

    public List<ShipPart> shipParts = new List<ShipPart>();

    //Start is called before the first frame update
    void Awake()
    {
        ShipPart cannon = new ShipPart("Cannon");
        ShipPart engine = new ShipPart("Engine");
        ShipPart balloon = new ShipPart("Balloon");
        ShipPart bow = new ShipPart("Bow");

        shipParts.Add(cannon);
        shipParts.Add(engine);
        shipParts.Add(balloon);
        shipParts.Add(bow);

    }

    public void SetStats()
    {
        health = 0;
        AD = 0;
        def = 0;
        foreach (ShipPart part in shipParts)
        {
            health += part.health;
            AD += part.AD;
            def += part.def;
        }
    }

    //Update is called once per frame
    void Update()
    {
        
    }
}
