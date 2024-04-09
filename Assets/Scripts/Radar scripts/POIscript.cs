using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class POIscript : MonoBehaviour
{
    public bool isEncounter = false;
    public bool isResource = false;

    public int level; //exposed for debug
    [HideInInspector] public float health;
    [HideInInspector] public int defensePower;
    [HideInInspector] public int attackPower;

    private void ScaleLevel(Character player, int lvl)
    {
        health = player.defaultHealth - 7 + (lvl * 7);
        defensePower = player.defaultDef - 7 + (lvl * 7);
        attackPower = player.defaultAD - 7 + (lvl * 7);

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
        ScaleLevel(player, level);
    }

    public void SetStats(float health, int ad, int def, int lvl)//set the stats when instantiated from the struct on scenestates ie. not the first time they are loaded
    {
        this.health = health;
        this.attackPower = ad;
        this.defensePower = def;
        this.level = lvl;
    }

    void Start()
    {   
        if (isEncounter == false && isResource == false)
        {
            Debug.LogError("POI is not set to be an encounter or a resource");
        }
    }
}
