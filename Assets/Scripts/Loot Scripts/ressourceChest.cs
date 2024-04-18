using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static Character;// so i doint have to write Character.ResourceType every time

public class ressourceChest : MonoBehaviour
{
    private Character character;
    [SerializeField] private TMPro.TextMeshProUGUI resourceGainText;
    [SerializeField] private GameObject winParticle;
    [SerializeField] private GameObject panel;
    [SerializeField] private Sprite wood,metal,gold,diamond; 
    private ParticleSystem particleSystem;
    

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
        int random = Random.Range(0, 100);
        if (random < 100)
        {
            Debug.Log("Wood");
            resourcesToGive[ResourceType.Wood] = Random.Range(15, 30);
            Debug.Log("wood amount: " + resourcesToGive[ResourceType.Wood]);
            Debug.Log(random);
          
          particleSystem.textureSheetAnimation.AddSprite(wood);
            

            character.AddResource(ResourceType.Wood, resourcesToGive[ResourceType.Wood]);
            StartCoroutine(TextDelay("Wood", resourcesToGive[ResourceType.Wood]));
            

        }
        else if (random < 50)
        {
            Debug.Log("Scrap Metal");
            resourcesToGive[ResourceType.Metal] = Random.Range(15, 30);
            Debug.Log("Scrap Metal amount: " + resourcesToGive[ResourceType.Metal]);
            Debug.Log(random);

            particleSystem.textureSheetAnimation.AddSprite(metal);
           

            character.AddResource(ResourceType.Metal, resourcesToGive[ResourceType.Metal]);
            StartCoroutine(TextDelay("Scrap Metal", resourcesToGive[ResourceType.Metal]));
        }
        else if (random < 75)
        {
            Debug.Log("Gold Ingot");
            resourcesToGive[ResourceType.Gold] = Random.Range(15, 30);
            Debug.Log("gold amount: " + resourcesToGive[ResourceType.Gold]);
            Debug.Log(random);

            particleSystem.textureSheetAnimation.AddSprite(gold);

            character.AddResource(ResourceType.Gold, resourcesToGive[ResourceType.Gold]);
            StartCoroutine(TextDelay("Gold Ingots", resourcesToGive[ResourceType.Gold]));
        }
        else
        {
            Debug.Log("Shiny Diamond"); 
            resourcesToGive[ResourceType.Diamonds] = Random.Range(15, 30);
            Debug.Log("diamond amount: " + resourcesToGive[ResourceType.Diamonds]);
            Debug.Log(random);

            particleSystem.textureSheetAnimation.AddSprite(diamond);

            character.AddResource(ResourceType.Diamonds, resourcesToGive[ResourceType.Diamonds]);
            StartCoroutine(TextDelay("Shiny Diamonds", resourcesToGive[ResourceType.Diamonds]));
          

        }
        
        IEnumerator TextDelay(string stringType, int resourceToGive)
        {
            yield return new WaitForSeconds(1.5f);
            AudioManager.instance.Play("chestSound");
            yield return new WaitForSeconds(0.5f);
            resourceGainText.text = resourceToGive + "x " + stringType;
            panel.SetActive(true);
          
        
        }
        // this method changes the particle material depending on the resource type


    }


}

