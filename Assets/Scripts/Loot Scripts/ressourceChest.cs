using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ressourceChest : MonoBehaviour
{
    public Animator animator;
    //example of new ressources with name, value, and random amount
    ressourceBehavior wood = new ressourceBehavior("Wood", 1, 1);
    ressourceBehavior coal = new ressourceBehavior("Coal", 2, 1);
    ressourceBehavior sm = new ressourceBehavior("Scrap Metal", 3, 1);
    ressourceBehavior diamond = new ressourceBehavior("Shiny Diamond", 3, 1);
    ressourceBehavior gold = new ressourceBehavior("Gold Ingot", 3, 1);

    
    public void OpenChest()
    {
        OpenChestRessource();
    }
    //Method to open the chest and get a random ressource with a random amount
    public ressourceBehavior OpenChestRessource()
    {
        int random = Random.Range(0, 100);
        if (random < 30)
        {
            Debug.Log("Wood");
            wood.amount = Random.Range(20, 25);
            Debug.Log("wood amount: " + wood.amount);
            Debug.Log(random);
            return wood;
        }
        else if (random < 50)
        {
            Debug.Log("Coal");
            coal.amount = Random.Range(10, 15);
            Debug.Log("coal amount: " + coal.amount);
            Debug.Log(random);
            return coal;
        }
        else if (random < 70)
        {
            Debug.Log("Scrap Metal");
            sm.amount = Random.Range(20, 25);
            Debug.Log("Scrap Metal amount: " + sm.amount);
            Debug.Log(random);
            return sm;
        }
        else if (random < 90)
        {
            Debug.Log("Gold Ingot");
            gold.amount = Random.Range(6, 9);
            Debug.Log("gold amount: " + gold.amount);
            Debug.Log(random);
            return gold;
        }
        else
        {
            Debug.Log("Shiny Diamond");
            diamond.amount = Random.Range(1, 4);
            Debug.Log("diamond amount: " + diamond.amount);
            Debug.Log(random);
            return diamond;
            
        }
    }


}

