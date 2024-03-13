using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ressourceChest : MonoBehaviour
{
    public Animator animator;
    //example of new ressources with name, value, and random amount
    //ressourceBehavior wood = new ressourceBehavior("Wood", 1);
    //ressourceBehavior coal = new ressourceBehavior("Coal", 1);
    //ressourceBehavior sm = new ressourceBehavior("Scrap Metal", 1);
    //ressourceBehavior diamond = new ressourceBehavior("Shiny Diamond", 1);
    //ressourceBehavior gold = new ressourceBehavior("Gold Ingot", 1);

    private Dictionary<string, int> resourcesToGive = new Dictionary<string, int>
    {
        {"Wood", 1},
        {"Scrap Metal", 1},
        {"Shiny Diamond", 1},
        {"Gold Ingot", 1}
    };

    private Character character;

    public TMPro.TextMeshProUGUI resourceGainText;

    //public void OpenChest1()
    //{
    //    OpenChestRessource();
    //    animator.SetBool("Chest1Clicked", true);
    //}
    //public void OpenChest2()
    //{
    //    OpenChestRessource();
    //    animator.SetBool("Chest2Clicked", true);
    //}
    //public void OpenChest3()
    //{
    //    OpenChestRessource();
    //    animator.SetBool("Chest3Clicked", true);
    //}

    private void Awake()
    {
        character = FindObjectOfType<Character>();
    }
    public void OpenChest(string ChestClicked)
    {
        OpenChestRessource();
        animator.SetBool(ChestClicked, true);
    }
    //Method to open the chest and get a random ressource with a random amount
    public void OpenChestRessource()
    {
        int random = Random.Range(0, 100);
        if (random < 30)
        {
            Debug.Log("Wood");
            resourcesToGive["Wood"] = Random.Range(20, 25);
            Debug.Log("wood amount: " + resourcesToGive["Wood"]);
            Debug.Log(random);

            character.AddResource("Wood", resourcesToGive["Wood"]);
            resourceGainText.text = "You found " + resourcesToGive["Wood"] + " " + "Wood";

        }
        else if (random < 70)
        {
            Debug.Log("Scrap Metal");
            resourcesToGive["Scrap Metal"] = Random.Range(20, 25);
            Debug.Log("Scrap Metal amount: " + resourcesToGive["Scrap Metal"]);
            Debug.Log(random);

            character.AddResource("Scrap Metal", resourcesToGive["Scrap Metal"]);
            resourceGainText.text = "You found " + resourcesToGive["Scrap Metal"] + " " + "Scrap Metal";
        }
        else if (random < 95)
        {
            Debug.Log("Gold Ingot");
            resourcesToGive["Gold Ingot"] = Random.Range(6, 9);
            Debug.Log("gold amount: " + resourcesToGive["Gold Ingot"]);
            Debug.Log(random);

            character.AddResource("Gold Ingot", resourcesToGive["Gold Ingot"]);
            resourceGainText.text = "You found " + resourcesToGive["Gold Ingot"] + " " + "Gold Ingot";
        }
        else
        {
            Debug.Log("Shiny Diamond");
            resourcesToGive["Shiny Diamond"] = Random.Range(1, 4);
            Debug.Log("diamond amount: " + resourcesToGive["Shiny Diamond"]);
            Debug.Log(random);

            character.AddResource("Shiny Diamond", resourcesToGive["Shiny Diamond"]);
            resourceGainText.text = "You found " + resourcesToGive["Shiny Diamond"] + " " + "Shiny Diamond";

        }
    }


}

