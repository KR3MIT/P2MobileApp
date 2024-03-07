using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ressourceBehavior : MonoBehaviour
{
// Create a base class called Item that defines common properties and methods for all loot items, such as name, rarity and value
    public new string name;
    public int value;
    public int amount;

    public ressourceBehavior(string name, int value, int amount)
    {
        this.name = name;
        this.value = value;
        this.amount = amount;
    }

}


