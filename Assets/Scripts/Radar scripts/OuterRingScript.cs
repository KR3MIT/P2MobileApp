using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterRingScript : MonoBehaviour
{
    public Animator animator;
    public void StartPulse()
    {
        animator.SetBool("OuterRingClicked", true);
    }
    public void StartReversePulse()
    {
        animator.SetBool("Collaps", true);
    }
}
