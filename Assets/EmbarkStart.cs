using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbarkStart : MonoBehaviour
{
    public RadarBehavior radar;
   public OuterRingScript outerRing;
    // Start is called before the first frame update
    void Start()
    {
        radar.spawnObjects();
        outerRing.StartPulse();
    }

 
}
