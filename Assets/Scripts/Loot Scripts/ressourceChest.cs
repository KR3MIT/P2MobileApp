using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Character;// so i doint have to write Character.ResourceType every time

public class ressourceChest : MonoBehaviour
{
    public Animator animator;
    //example of new ressources with name, value, and random amount
    //ressourceBehavior wood = new ressourceBehavior("Wood", 1);
    //ressourceBehavior coal = new ressourceBehavior("Coal", 1);
    //ressourceBehavior sm = new ressourceBehavior("Scrap Metal", 1);
    //ressourceBehavior diamond = new ressourceBehavior("Shiny Diamond", 1);
    //ressourceBehavior gold = new ressourceBehavior("Gold Ingot", 1);

    private Dictionary<ResourceType, int> resourcesToGive = new Dictionary<ResourceType, int>
    {
        {ResourceType.Wood, 1},
        {ResourceType.Metal, 1},
        {ResourceType.Gold, 1},
        {ResourceType.Diamonds, 1}
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
            resourcesToGive[ResourceType.Wood] = Random.Range(20, 25);
            Debug.Log("wood amount: " + resourcesToGive[ResourceType.Wood]);
            Debug.Log(random);

            character.AddResource(ResourceType.Wood, resourcesToGive[ResourceType.Wood]);
            resourceGainText.text = "You found " + resourcesToGive[ResourceType.Wood] + " " + "Wood";

        }
        else if (random < 70)
        {
            Debug.Log("Scrap Metal");
            resourcesToGive[ResourceType.Metal] = Random.Range(20, 25);
            Debug.Log("Scrap Metal amount: " + resourcesToGive[ResourceType.Metal]);
            Debug.Log(random);

            character.AddResource(ResourceType.Metal, resourcesToGive[ResourceType.Metal]);
            resourceGainText.text = "You found " + resourcesToGive[ResourceType.Metal] + " " + "Scrap Metal";
        }
        else if (random < 95)
        {
            Debug.Log("Gold Ingot");
            resourcesToGive[ResourceType.Gold] = Random.Range(6, 9);
            Debug.Log("gold amount: " + resourcesToGive[ResourceType.Gold]);
            Debug.Log(random);

            character.AddResource(ResourceType.Gold, resourcesToGive[ResourceType.Gold]);
            resourceGainText.text = "You found " + resourcesToGive[ResourceType.Gold] + " " + "Gold Ingot";
        }
        else
        {
            Debug.Log("Shiny Diamond");
            resourcesToGive[ResourceType.Diamonds] = Random.Range(1, 4);
            Debug.Log("diamond amount: " + resourcesToGive[ResourceType.Diamonds]);
            Debug.Log(random);

            character.AddResource(ResourceType.Diamonds, resourcesToGive[ResourceType.Diamonds]);
            resourceGainText.text = "You found " + resourcesToGive[ResourceType.Diamonds] + " " + "Shiny Diamond";

        }
    }


}

