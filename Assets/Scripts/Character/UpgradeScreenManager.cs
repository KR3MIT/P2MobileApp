using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static ShipPartObject;
using System.Drawing;

public class UpgradeScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject partList;
    private Character character;

    [SerializeField] private GameObject shipPart;
    [SerializeField] private GameObject partCost;

    [SerializeField] private UnityEngine.UI.Button upgradeButton;

    private ShipPartObject selectedPart;

    private bool partIsSelected = false;

    private List<GameObject> _costList = new List<GameObject>();

    public StatsOverlay statsOverlay;

    public HeaderStats headerStats;

    // Start is called before the first frame update
    void Start()
    {
        character = Character.instance;

        foreach (ShipPartObject part in character.shipParts)
        {
            GameObject _part = Instantiate(shipPart, partList.transform);
            GameObject _cost = Instantiate(partCost, partList.transform);
            
            _costList.Add(_cost);

            part.instanciateShipCost = _cost;
            part.instanciateShipPart = _part;

            _part.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>ToggleCost(_cost, part));

            UpdateText(part);
        }

        upgradeButton.onClick.AddListener(Upgrade);
        upgradeButton.interactable = false;

    }

    private void ToggleCost(GameObject _cost, ShipPartObject part)
    {
        foreach (GameObject cost in _costList)
        {
            if(cost != _cost)
            cost.SetActive(false);
        }
        
        _cost.SetActive(!_cost.activeSelf);


        ResetStatOverlayStrings();


        if (_cost.activeSelf && character.CanLevelUp(part))
        {
            upgradeButton.interactable = true;
            partIsSelected = true;
            selectedPart = part;
        }
        else
        {
            upgradeButton.interactable = false;
            partIsSelected = false;
            selectedPart = null;
            return;//such that switch statement is not executed
        }

        if (statsOverlay == null) { return; }
        switch (part.statToUpgrade)
        {
            case StatType.health:
                statsOverlay.healthString = "<color=green>" + " + " + part.upgradeImprovement.ToString() + "</color>";
                break;
            case StatType.def:
                statsOverlay.defString = "<color=green>" + " + " + part.upgradeImprovement + "</color>";
                break;
            case StatType.AD:
                statsOverlay.adString = "<color=green>" + " + " + part.upgradeImprovement + "</color>";
                break;
        }

    }

    private void ResetStatOverlayStrings()
    {
        statsOverlay.adString = "";
        statsOverlay.defString = "";
        statsOverlay.healthString = "";
    }

    private void Upgrade()
    {
        Debug.Log($"javla name: {selectedPart.partName}  child {selectedPart.instanciateShipPart.transform.GetChild(0).name}  lvl: {selectedPart.lvl}");
        if (partIsSelected)
        {
            character.LevelUpPart(selectedPart);
            UpdateAllText();
            headerStats.UpdateTexts();

            if (character.CanLevelUp(selectedPart))
            {
                upgradeButton.interactable = true;
            }
            else
            {
                upgradeButton.interactable = false;
            }
        }
    }

    private void UpdateAllText()
    {
        foreach (ShipPartObject part in character.shipParts)
        {
            UpdateText(part);
        }
    }

    private void UpdateText(ShipPartObject selectedPart)
    {
        
        selectedPart.instanciateShipPart.transform.GetChild(0).GetComponent<TMP_Text>().text = selectedPart.partName + " lvl " + selectedPart.lvl;
        string costText = "";
        foreach (Character.ResourceType type in selectedPart.upgradeTypes)
        {
            int resourceAmount = 0;
            switch (type)
            {
                case Character.ResourceType.Wood:
                    resourceAmount = character.resources[Character.ResourceType.Wood];
                    break;
                case Character.ResourceType.Metal:
                    resourceAmount = character.resources[Character.ResourceType.Metal];
                    break;
                case Character.ResourceType.Diamonds:
                    resourceAmount = character.resources[Character.ResourceType.Diamonds];
                    break;
                case Character.ResourceType.Gold:
                    resourceAmount = character.resources[Character.ResourceType.Gold];
                    break;
            }
            costText += type.ToString() + ": " + resourceAmount + "/" + selectedPart.upgradeCost + "\n";
        }
        selectedPart.instanciateShipCost.transform.GetChild(0).GetComponent<TMP_Text>().text = costText;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
