using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// This script was developed with the help of Github Co-pilot.

public class EnemyCharacterTest : MonoBehaviour
{
    public int level;
    public float health;
    public int defensePower;
    public int attackPower;

    public void ScaleLevel(int lvl)
    {
        health = 93 + (lvl * 7);
        defensePower = 3 + (lvl * 7);
        attackPower = 3 + (lvl * 7);
    }

    public void RandomizeLevel(int lvl)
    {
       
        level = Random.Range(-2,lvl+2);
        if (level < 1)
        {
            level = 1;
        }
        ScaleLevel(level);
    }

}
