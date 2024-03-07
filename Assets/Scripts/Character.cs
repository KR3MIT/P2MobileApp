using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Attributes
    public float health = 100;
    public float AP = 100;
    public float MP = 100;

    //Resources
    public float wood = 0;
    public float metal = 0;
    public float diamonds = 0;
    public float gold = 0;

    //Energy
    public float coal = 0;

    //Start is called before the first frame update
    void Start()
    {
        
    }

    //Enum with different attacks
    public enum AttackType
    {
        Cannons,
        Rockets,
        Magic
    }

    //Update is called once per frame
    void Update()
    {
        
    }
}
