using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class POIscript : MonoBehaviour
{
    public bool isEncounter = false;
    public bool isResource = false;

    [HideInInspector] public int level;
    [HideInInspector] public float health;
    [HideInInspector] public int defensePower;
    [HideInInspector] public int attackPower;

    private void ScaleLevel(Character player)
    {
        health = player.defaultHealth - 7 + (player.lvl * 7);
        defensePower = player.defaultDef - 7 + (player.lvl * 7);
        attackPower = player.defaultAD - 7 + (player.lvl * 7);

        if(health < 1)
        {
            health = 1;
        }
        if(defensePower < 1)
        {
            defensePower = 1;
        }
        if(attackPower < 1)
        {
            attackPower = 1;
        }
    }

    public void RandomizeLevel(Character player)
    {

        level = Random.Range(-2 + player.lvl, player.lvl + 2);
        if (level < 1)
        {
            level = 1;
        }
        ScaleLevel(player);
    }

    void Start()
    {   
        if (isEncounter == false && isResource == false)
        {
            Debug.LogError("POI is not set to be an encounter or a resource");
        }
    }
}
