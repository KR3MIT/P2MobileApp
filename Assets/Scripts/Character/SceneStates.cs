using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;


public struct stats
{
    public int level;
    public float health;
    public int ad;
    public int def;
    public Vector3 pos;

    public stats(Vector3 pos, int lvl, float hp, int atk, int def)
    {
        level = lvl;
        health = hp;
        ad = atk;
        this.def = def;
        this.pos = pos;
    }
}
public class SceneStates : MonoBehaviour
{
    [HideInInspector] public Dictionary<stats, GameObject> POIdict = new Dictionary<stats, GameObject>();
    [HideInInspector] public bool isEmbarked;
    [HideInInspector] public int level, ad, def;//from poi
    [HideInInspector] public float health;

    public void ClearData()
    {
        POIdict.Clear();
        isEmbarked = false;
        level = 0;
        health = 0;
        ad = 0;
        def = 0;
    }

    public void SetPOIStats(int lvl, float hp, int atk, int def) //only used for the combatencounter
    {
        level = lvl;
        health = hp;
        ad = atk;
        this.def = def;
    }
}
