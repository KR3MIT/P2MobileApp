using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ressourceBehavior
{
//Constructor that creates a new ressource with a name, value, and amount
    public string name;
    public int value;
    public int amount;

    public ressourceBehavior(string name, int value, int amount)
    {
        this.name = name;
        this.value = value;
        this.amount = amount;
    }

}


