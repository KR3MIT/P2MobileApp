using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using static Character;// so i doint have to write Character.ResourceType every time

public class ressourceChest : MonoBehaviour
{
    private Character character;
    [SerializeField] private TMPro.TextMeshProUGUI resourceGainText;
    [SerializeField] private ParticleSystem winParticle;
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private GameObject panel;

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
     
    }
    public void OpenChest(string ChestClicked)
    {
        if(character != null)
        {
            OpenChestResource();
            winParticle.Play();
        }
        else Debug.Log("Character not found");
        

    }
   
    //Method to open the chest and get a random resource with a random amount
    public void OpenChestResource()
    {
        int random = Random.Range(0, 100);
        if (random < 40)
        {
            Debug.Log("Wood");
            resourcesToGive[ResourceType.Wood] = Random.Range(20, 25);
            Debug.Log("wood amount: " + resourcesToGive[ResourceType.Wood]);
            Debug.Log(random);

            ParticleSystem.MainModule settings = winParticle.main;
            settings.startColor = new ParticleSystem.MinMaxGradient(Color.green);

            character.AddResource(ResourceType.Wood, resourcesToGive[ResourceType.Wood]);
            StartCoroutine(TextDelay("Wood", resourcesToGive[ResourceType.Wood]));
            

        }
        else if (random < 70)
        {
            Debug.Log("Scrap Metal");
            resourcesToGive[ResourceType.Metal] = Random.Range(20, 25);
            Debug.Log("Scrap Metal amount: " + resourcesToGive[ResourceType.Metal]);
            Debug.Log(random);

            ParticleSystem.MainModule settings = winParticle.main;
            settings.startColor = new ParticleSystem.MinMaxGradient(Color.grey);

            character.AddResource(ResourceType.Metal, resourcesToGive[ResourceType.Metal]);
            StartCoroutine(TextDelay("Scrap Metal", resourcesToGive[ResourceType.Metal]));
        }
        else if (random < 90)
        {
            Debug.Log("Gold Ingot");
            resourcesToGive[ResourceType.Gold] = Random.Range(6, 9);
            Debug.Log("gold amount: " + resourcesToGive[ResourceType.Gold]);
            Debug.Log(random);

            ParticleSystem.MainModule settings = winParticle.main;
            settings.startColor = new ParticleSystem.MinMaxGradient(Color.yellow);

            character.AddResource(ResourceType.Gold, resourcesToGive[ResourceType.Gold]);
            StartCoroutine(TextDelay("Gold Ingots", resourcesToGive[ResourceType.Gold]));
        }
        else
        {
            Debug.Log("Shiny Diamond");
            resourcesToGive[ResourceType.Diamonds] = Random.Range(1, 4);
            Debug.Log("diamond amount: " + resourcesToGive[ResourceType.Diamonds]);
            Debug.Log(random);

            ParticleSystem.MainModule settings = winParticle.main;
            settings.startColor = new ParticleSystem.MinMaxGradient(Color.cyan);

            character.AddResource(ResourceType.Diamonds, resourcesToGive[ResourceType.Diamonds]);
            StartCoroutine(TextDelay("Shiny Diamonds", resourcesToGive[ResourceType.Diamonds]));
          

        }
        
        IEnumerator TextDelay(string stringType, int resourceToGive)
        {
            yield return new WaitForSeconds(1.5f);
            audioPlayer.Play();
            yield return new WaitForSeconds(0.5f);
            resourceGainText.text = resourceToGive + "x " + stringType;
            panel.SetActive(true);
          
        
        }
        
    }


}

