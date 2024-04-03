using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class SceneStates : MonoBehaviour
{
    [HideInInspector] public List<GameObject> POIs = new List<GameObject>();
    [HideInInspector] public Dictionary<Vector3, GameObject> POIdict = new Dictionary<Vector3, GameObject>();

    public int count = 0;
    private void Update()
    {
        count = POIs.Count;
    }
}
