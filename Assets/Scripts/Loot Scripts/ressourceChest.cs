using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
// this is a singleton class that holds the resources that the player can get from the chest
using static Character;// so i doint have to write Character.ResourceType every time

//
// this script is created with the help of Github Copilot
//

public class ressourceChest : MonoBehaviour
{
    private Character character;
    [SerializeField] private TMPro.TextMeshProUGUI resourceGainText;
    [SerializeField] private GameObject winParticle;
    [SerializeField] private GameObject panel;
    [SerializeField] private Sprite wood,metal,gold,diamond; 
    private ParticleSystem particleSystem;

    private float soundsDelay = 1.5f;
    private float textDelay = 0.5f;

    private int lowerAmount = 15;
    private int upperAmount = 30;
    
    // this is a dictionary that holds the resources that the player can get from the chest and the amount of the resource is default 1
    private Dictionary<ResourceType, int> resourcesToGive = new Dictionary<ResourceType, int>
    {
        {ResourceType.Wood, 1},
        {ResourceType.Metal, 1},
        {ResourceType.Gold, 1},
        {ResourceType.Diamonds, 1}
    };


    private void Awake()
    {
        panel.SetActive(false);
        character = FindObjectOfType<Character>();
        particleSystem = winParticle.GetComponent<ParticleSystem>();
    }
    //this method is called when a chest is clicked and if the character singleton is there it calls the OpenChestResource method it plays the particle system 
    public void OpenChest(string ChestClicked)
    {
        if(character != null)
        {
            OpenChestResource();
            winParticle.GetComponent<ParticleSystem>().Play();
        }
        else Debug.Log("Character not found");
        

    }
   
    //Method to open the chest and get a random resource with a random amount
    public void OpenChestResource()
    {
        // random is set to a random number between 0 and 100
        int random = Random.Range(0, 100);
        if (random < 25)
        {
            Debug.Log("Wood");
            resourcesToGive[ResourceType.Wood] = Random.Range(lowerAmount, upperAmount);
            Debug.Log("wood amount: " + resourcesToGive[ResourceType.Wood]);
            Debug.Log(random);
          
          particleSystem.textureSheetAnimation.AddSprite(wood);
            
            // this adds the resource to the character singleton and then calls the TextDelay method to display the resource gain text
            character.AddResource(ResourceType.Wood, resourcesToGive[ResourceType.Wood]);
            StartCoroutine(TextDelay("Wood", resourcesToGive[ResourceType.Wood]));
            

        }
        else if (random < 50)
        {
            Debug.Log("Scrap Metal");
            resourcesToGive[ResourceType.Metal] = Random.Range(lowerAmount, upperAmount);
            Debug.Log("Scrap Metal amount: " + resourcesToGive[ResourceType.Metal]);
            Debug.Log(random);

            particleSystem.textureSheetAnimation.AddSprite(metal);

            // this adds the resource to the character singleton and then calls the TextDelay method to display the resource gain text
            character.AddResource(ResourceType.Metal, resourcesToGive[ResourceType.Metal]);
            StartCoroutine(TextDelay("Scrap Metal", resourcesToGive[ResourceType.Metal]));
        }
        else if (random < 75)
        {
            Debug.Log("Gold Ingot");
            resourcesToGive[ResourceType.Gold] = Random.Range(lowerAmount, upperAmount);
            Debug.Log("gold amount: " + resourcesToGive[ResourceType.Gold]);
            Debug.Log(random);

            particleSystem.textureSheetAnimation.AddSprite(gold);
            // this adds the resource to the character singleton and then calls the TextDelay method to display the resource gain text
            character.AddResource(ResourceType.Gold, resourcesToGive[ResourceType.Gold]);
            StartCoroutine(TextDelay("Gold Ingots", resourcesToGive[ResourceType.Gold]));
        }
        else
        {
            Debug.Log("Shiny Diamond"); 
            resourcesToGive[ResourceType.Diamonds] = Random.Range(lowerAmount, upperAmount);
            Debug.Log("diamond amount: " + resourcesToGive[ResourceType.Diamonds]);
            Debug.Log(random);

            particleSystem.textureSheetAnimation.AddSprite(diamond);
            // this adds the resource to the character singleton and then calls the TextDelay method to display the resource gain text
            character.AddResource(ResourceType.Diamonds, resourcesToGive[ResourceType.Diamonds]);
            StartCoroutine(TextDelay("Shiny Diamonds", resourcesToGive[ResourceType.Diamonds]));
          

        }
        
        IEnumerator TextDelay(string stringType, int resourceToGive)
        {
            // this coroutine waits for 1.5 seconds and then plays the open chest sound and then waits for 0.5 seconds and then displays the resource gain text
            yield return new WaitForSeconds(soundsDelay);
            AudioManager.instance.Play("chestSound");
            yield return new WaitForSeconds(textDelay);
            resourceGainText.text = resourceToGive + "x " + stringType;
            panel.SetActive(true);
          
        
        }
        


    }


}

