using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeaderStats : MonoBehaviour
{
    private TMP_Text namelvlText;
    private TMP_Text resourcesText;

    private Character character;


    private void Start()
    {
        namelvlText = transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
        resourcesText = transform.GetChild(0).transform.GetChild(1).GetComponent<TMP_Text>();

        character = GameObject.FindWithTag("Player").GetComponent<Character>();

        namelvlText.text = character.playerName + " Lvl " + character.lvl;
        resourcesText.text = "Wood: " + character.resources[Character.ResourceType.Wood] + "\n" +
                            "Metal: " + character.resources[Character.ResourceType.Metal] + "\n" +
                            "Diamonds: " + character.resources[Character.ResourceType.Diamonds] + "\n" +
                            "Gold: " + character.resources[Character.ResourceType.Gold];
    }


}
