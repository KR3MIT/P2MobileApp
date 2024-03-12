using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPart : MonoBehaviour
{
    public string partName {  get; private set; }
    public int lvl {  get; private set; }
    public int AD { get; private set; }
    public int def { get; private set; }
    public int health { get; private set; }
    public int upgradeCost { get; private set; }

    public GameObject instanciateShipPart { get; private set; }
    public GameObject instanciateShipCost { get; private set; }

    public ShipPart(string partName)
    {
        this.partName = partName;
    }

    // Start is called before the first frame update
    void Start()
    {
        lvl = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp(ref int StatToGrow)
    {
        StatToGrow += 3;
    }

    public void SetInstanciated(GameObject part, GameObject cost)
    {
        instanciateShipPart = part;
        instanciateShipCost = cost;



    }
}
