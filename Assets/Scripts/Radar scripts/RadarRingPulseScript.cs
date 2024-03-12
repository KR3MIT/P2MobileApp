using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarRingPulseScript : MonoBehaviour
{
    public Animator animator;
    public void StartPulse()
    {
        animator.SetBool("RadarClicked", true);
    }
}
