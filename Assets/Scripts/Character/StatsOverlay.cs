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
    private float _AD;
    private float _def;

    [SerializeField] private TMP_Text attributesText;
    [SerializeField] private TMP_Text partsText;
    [SerializeField] private Canvas _stats;

    private string attackString;

    void Awake()
    {

           
    }
    // Start is called before the first frame update
    void Start()
    {
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
        attributesText.text = "Stats: \n" + "Health: " + _health + "\n" + "AD: " + _AD + "\n" + "def: " + _def;


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