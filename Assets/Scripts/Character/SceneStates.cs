using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class SceneStates : MonoBehaviour
{
    [HideInInspector] public Dictionary<Vector3, GameObject> POIdict = new Dictionary<Vector3, GameObject>();
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

    public void SetPOIStats(int lvl, float hp, int atk, int def)
    {
        level = lvl;
        health = hp;
        ad = atk;
        this.def = def;
    }
}
