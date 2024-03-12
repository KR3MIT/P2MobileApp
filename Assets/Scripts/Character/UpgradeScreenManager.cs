using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class UpgradeScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject partList;
    [SerializeField] private Character character;

    [SerializeField] private GameObject shipPart;
    [SerializeField] private GameObject partCost;

    [SerializeField] private UnityEngine.UI.Button upgradeButton;

    private GameObject selectedCost;
    private ShipPart selectedPart;

    private bool partIsSelected = false;

    private List<GameObject> _costList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (ShipPart part in character.shipParts)
        {
            GameObject _part = Instantiate(shipPart, partList.transform);
            GameObject _cost = Instantiate(partCost, partList.transform);
            
            _costList.Add(_cost);

            part.SetInstanciated(_part, _cost);

            _part.transform.GetChild(0).GetComponent<TMP_Text>().text = part.partName + " lvl " +part.lvl;

            _part.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>ToggleCost(_cost, part));

            _cost.transform.GetChild(0).GetComponent<TMP_Text>().text = "Cost: 6969/" + part.upgradeCost;
        }

        upgradeButton.onClick.AddListener(Upgrade);
        upgradeButton.interactable = false;

    }

    private void ToggleCost(GameObject _cost, ShipPart part)
    {
        foreach (GameObject cost in _costList)
        {
            if(cost != _cost)
            cost.SetActive(false);
        }
        
        _cost.SetActive(!_cost.activeSelf);

        if (_cost.activeSelf)
        selectedCost = _cost;
        

        if(_cost.activeSelf)
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
        }

    }

    private void Upgrade()
    {
        if(partIsSelected)
        {
            selectedPart.LevelUp(ref character.AD);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
