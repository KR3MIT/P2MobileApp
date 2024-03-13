using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lootDisplay : MonoBehaviour
{
    //The following script displays the output from ressourceChest in text form
    public ressourceChest ressourceChest;
    public TMPro.TextMeshProUGUI text;


    public void DisplayLoot()
    {
        ressourceBehavior loot = ressourceChest.OpenChestRessource();
        text.text = "You found " + loot.amount + " " + loot.name;
    }
}
