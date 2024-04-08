using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIscript : MonoBehaviour
{
    public bool isEncounter = false;
    public bool isResource = false;

    [HideInInspector] public int level;
    [HideInInspector] public float health;
    [HideInInspector] public int defensePower;
    [HideInInspector] public int attackPower;

    private void ScaleLevel(int lvl)
    {
        health = 93 + (lvl * 7);
        defensePower = 3 + (lvl * 7);
        attackPower = 3 + (lvl * 7);
    }

    public void RandomizeLevel(int lvl)
    {

        level = Random.Range(-2, lvl + 2);
        if (level < 1)
        {
            level = 1;
        }
        ScaleLevel(level);
    }

    void Start()
    {
        if(isEncounter == false && isResource == false)
        {
            Debug.LogError("POI is not set to be an encounter or a resource");
        }
    }
}
