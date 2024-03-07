using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ressourceBehavior : MonoBehaviour
{
// Create a base class called Item that defines common properties and methods for all loot items, such as name, rarity and value
    public new string name;
    public int rarity;
    public int value;

    public ressourceBehavior(string name, int rarity, int value)
    {
        this.name = name;
        this.rarity = rarity;
        this.value = value;
    }

/*
 //this is an example of how to create an item
    ressourceBehavior wood = new ressourceBehavior("Wood", 1, 5);
    ressourceBehavior coal = new ressourceBehavior("Coal", 2, 10);
    ressourceBehavior iron = new ressourceBehavior("Iron", 3, 15);
*/
    

}


