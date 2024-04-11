using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class EmbarkEndStats : MonoBehaviour
{
    [SerializeField] private Button _continue;

    private SceneStates _state;
    private SaveDataToCSV _saveData;

    [SerializeField] private TMP_Text expText;

    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text metalText;
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text diamondsText;
    [SerializeField] private TMP_Text distance;

    private int Wood, Metal, Gold, Diamonds, exp;

    // Start is called before the first frame update
    void Start()
    {
        _state = Character.instance.GetComponent<SceneStates>();
        _continue.onClick.AddListener(ToHome);

        Wood = Character.instance.resources[Character.ResourceType.Wood] - _state.preResources[Character.ResourceType.Wood];
        Metal = Character.instance.resources[Character.ResourceType.Metal] - _state.preResources[Character.ResourceType.Metal];
        Gold = Character.instance.resources[Character.ResourceType.Gold] - _state.preResources[Character.ResourceType.Gold];
        Diamonds = Character.instance.resources[Character.ResourceType.Diamonds] - _state.preResources[Character.ResourceType.Diamonds];

        exp = Character.instance.exp - _state.preExp;

        woodText.text = "Wood: " + Wood;
        metalText.text = "Metal: " + Metal;
        goldText.text = "Gold: " + Gold;
        diamondsText.text = "Diamonds: " + Diamonds;

        expText.text = "Exp: " + exp;

        distance.text = "Distance Traveled: \n" + _state.moved.ToString() + " m.";

        if (TryGetComponent<SaveDataToCSV>(out _saveData))
        {
            _saveData.SerializeCSV(_state.moved, "Distance traveled");
        }

        Character.instance.GetComponent<Statistics>().AddEmbark();

        Destroy(Map.instance.gameObject);
        Destroy(LocationMove.instance.gameObject);
    }

    private void ToHome()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
