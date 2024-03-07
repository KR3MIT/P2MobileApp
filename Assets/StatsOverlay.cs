using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class StatsOverlay : MonoBehaviour
{
    public Character _character;
    public UnityEngine.UI.Button _toggleStats;

    private float _health;
    private float _AP;
    private float _MP;

    [SerializeField] private TMP_Text attributesText;
    [SerializeField] private TMP_Text attacksText;
    [SerializeField] private Canvas _stats;

    void Awake()
    {

           
    }
    // Start is called before the first frame update
    void Start()
    {
        _toggleStats.onClick.AddListener(toggleStats);
    }


    // Update is called once per frame
    void Update()
    {
        if (_character != null)
        {
            updateStats();
            displayStats();
        }
        
    }

    //when a button is pressed, toggle _stats
    public void toggleStats()
    {
        _stats.enabled = !_stats.enabled;
    }

    void updateStats()
    {
        #region updateAttributes

        _health = _character.GetComponent<Character>().health;
        _AP = _character.GetComponent<Character>().AP;
        _MP = _character.GetComponent<Character>().MP;

        #endregion


        #region updateAttacks

        //get enum values from Character.cs and display them
        string[] attackNames = System.Enum.GetNames(typeof(Character.AttackType));
        string attackString = "Attacks: \n";
        for (int i = 0; i < attackNames.Length; i++)
        {
            attackString += attackNames[i] + "\n";
        }
        attacksText.text = attackString;
        
        #endregion
    }

    void displayStats()
    {
        attributesText.text = "Attacks: \n" + "Health: " + _health + "\n" + "AP: " + _AP + "\n" + "MP: " + _MP;
    }
}