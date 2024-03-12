using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPart : MonoBehaviour
{
    public string partName;
    public int lvl = 1;
    public int AD;
    public int def;
    public int health;

    public ShipPart(string partName)
    {
        this.partName = partName;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp(ref int StatToGrow)
    {
        StatToGrow += 3;
    }
}
