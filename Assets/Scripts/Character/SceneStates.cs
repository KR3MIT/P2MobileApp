using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using static Character;


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
    private Character _player;
    private LocationMove _locationMove;
    [HideInInspector] public int preExp;

    [HideInInspector] public Dictionary<stats, GameObject> POIdict = new Dictionary<stats, GameObject>();
    [HideInInspector] public bool isEmbarked { get; private set; } 
    [HideInInspector] public int level, ad, def;//from poi
    [HideInInspector] public float health;
    [HideInInspector] public float moved;
    

    public Dictionary<ResourceType, int> preResources = new Dictionary<ResourceType, int>
    {
        { ResourceType.Wood, 0 },
        { ResourceType.Metal, 0 },
        { ResourceType.Diamonds, 0 },
        { ResourceType.Gold, 0 }
    };

    private void Start()
    {
        _player = GetComponent<Character>();
        
        
    }

    public void SetPreRes()
    {
        preResources[ResourceType.Wood] = _player.resources[ResourceType.Wood];
        preResources[ResourceType.Metal] = _player.resources[ResourceType.Metal];
        preResources[ResourceType.Diamonds] = _player.resources[ResourceType.Diamonds];
        preResources[ResourceType.Gold] = _player.resources[ResourceType.Gold];

        preExp = _player.exp;

        Debug.Log("ass pre embark res stored. preWood " + preResources[ResourceType.Wood]);

    }

    public void SetEmbarked(bool setEmbarked)
    {
        this.isEmbarked = setEmbarked;
        
        if (!setEmbarked)
        {
            if (GameObject.Find("ShipManager") != null)
            {
                _locationMove = GameObject.Find("ShipManager").GetComponent<LocationMove>();
                moved = _locationMove.totalDistance;
                Debug.Log("bitch distance " + moved);
            }
        }
        else
        {
            SetPreRes();
        }


    }

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
