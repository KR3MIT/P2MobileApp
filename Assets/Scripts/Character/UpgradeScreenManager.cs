using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject partList;
    [SerializeField] private Character character;

    public GameObject shipPart;
    public GameObject partCost;

    

    // Start is called before the first frame update
    void Start()
    {
        foreach (ShipPart part in character.shipParts)
        {
            GameObject _part = Instantiate(shipPart, partList.transform);
            GameObject _cost = Instantiate(partCost, partList.transform);
            part.SetInstanciated(_part, _cost);

            _part.transform.GetChild(0).GetComponent<TMP_Text>().text = part.partName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
