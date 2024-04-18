using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class StatsOverlay : MonoBehaviour
{
    private Character _character;
    public UnityEngine.UI.Button _toggleStats;

    private float _health;
    private float _AD;
    private float _def;

    [HideInInspector] public string adString;
    [HideInInspector] public string defString;
    [HideInInspector] public string healthString;

    [SerializeField] private TMP_Text attributesText;
    [SerializeField] private TMP_Text partsText;
    [SerializeField] private Canvas _stats;

    void Awake()
    {

           
    }
    // Start is called before the first frame update
    void Start()
    {
        _character = GameObject.FindWithTag("Player").GetComponent<Character>();
        _toggleStats.onClick.AddListener(toggleStats);
        _stats.enabled = true;
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

        _health = _character.health;
        _AD = _character.AD;
        _def = _character.def;

        #endregion

    }

    void displayStats()
    {

        attributesText.text = "Stats: \n" + 
            "Health: " + _health + healthString + " \n" + 
            "Attack: " + _AD + adString + "\n" + 
            "Defence: " + _def + defString;

        


        if (_character.shipParts == null)
        {
            return;   
        }
        partsText.text = "Parts: \n";
        foreach (ShipPartObject part in _character.shipParts)
        {
            partsText.text += part.partName + " level " + part.lvl + "\n";
        }
    }
}