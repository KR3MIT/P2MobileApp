using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestRessource : MonoBehaviour
{
    //example of new ressources with name, value, and random amount
    ressourceBehavior wood = new ressourceBehavior("Wood", 1, Random.Range(15, 20));
    ressourceBehavior coal = new ressourceBehavior("Coal", 2, Random.Range(8, 12));
    ressourceBehavior iron = new ressourceBehavior("Iron", 3, Random.Range(5, 10));

    void Start()
    {
        OpenChestRessource();
    }
    //create a method that will chose a random item based on the rarity of the item
    public ressourceBehavior OpenChestRessource()
    {
        int random = Random.Range(0, 100);
        if (random < 50)
        {
            Debug.Log("Wood");
            Debug.Log("wood amount: " + wood.amount);
            Debug.Log(random);

            return wood;
        }
        else if (random < 80)
        {
            Debug.Log("Coal");
            Debug.Log("coal amount: " + coal.amount);
           Debug.Log(random);
            return coal;
        }
        else
        {
            Debug.Log("Iron");
            Debug.Log("iron amount: " + iron.amount);
           Debug.Log(random);
            return iron;
        }
    }
}
