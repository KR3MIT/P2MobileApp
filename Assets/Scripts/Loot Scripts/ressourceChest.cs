using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ressourceChest : MonoBehaviour
{
    //example of new ressources with name, value, and random amount
    ressourceBehavior wood = new ressourceBehavior("Wood", 1, 1);
    ressourceBehavior coal = new ressourceBehavior("Coal", 2, 1);
    ressourceBehavior iron = new ressourceBehavior("Iron", 3, 1);
    

    void Start()
    {
        OpenChestRessource();

    }
    //Method to open the chest and get a random ressource with a random amount
    public ressourceBehavior OpenChestRessource()
    {
        int random = Random.Range(0, 100);
        if (random < 50)
        {
            Debug.Log("Wood");
            wood.amount = Random.Range(15, 25);
            Debug.Log("wood amount: " + wood.amount);
            Debug.Log(random);
            return wood;
        }
        else if (random < 80)
        {
            Debug.Log("Coal");
            coal.amount = Random.Range(8, 15);
            Debug.Log("coal amount: " + coal.amount);
            Debug.Log(random);
            return coal;
        }
        else
        {
            Debug.Log("Iron");
            iron.amount = Random.Range(3, 7);
            Debug.Log("iron amount: " + iron.amount);
            Debug.Log(random);
            return iron;
        }
    }
  

}
