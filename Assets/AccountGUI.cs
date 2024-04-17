using TMPro;
using UnityEngine;

public class AccountGUI : MonoBehaviour
{
    [SerializeField] private TMP_Text reqText;
    [SerializeField] private GameObject reqPanel;
    [SerializeField] private GameObject activePos;

    [SerializeField] private GameObject userPanel;
    private Vector3 userDefaultPos;
    private TMP_InputField userInput;

    [SerializeField] private GameObject passPanel;
    private Vector3 passDefaultPos;
    private TMP_InputField passInput;

    [SerializeField] private GameObject namePanel;
    private Vector3 nameDefaultPos;    
    private TMP_InputField nameInput;

    void Start()
    {
        reqPanel.SetActive(false);

        userInput = userPanel.GetComponent<TMP_InputField>();
        passInput = passPanel.GetComponent<TMP_InputField>();
        nameInput = namePanel.GetComponent<TMP_InputField>();

        userDefaultPos = userPanel.transform.position;
        passDefaultPos = passPanel.transform.position;
        nameDefaultPos = namePanel.transform.position;

        //call method userinputactive when inputfield is active or pressed
        userInput.onSelect.AddListener(delegate { ActiveInput(userInput, true); });
        userInput.onDeselect.AddListener(delegate { ActiveInput(userInput, false); });
        //pass
        passInput.onSelect.AddListener(delegate { ActiveInput(passInput, true); });
        passInput.onDeselect.AddListener(delegate { ActiveInput(passInput, false); });

        nameInput.onSelect.AddListener(delegate { ActiveInput(nameInput, true); });
        nameInput.onDeselect.AddListener(delegate { ActiveInput(nameInput, false); });
    }
    private void ActiveInput(TMP_InputField type, bool active)
    {
        if (active)
        {
            if (type == userInput)
            {
                userPanel.transform.position = activePos.transform.position;
                reqText.text = "Must contain: <b>No</b> spaces, \nmin. of <b>3</b> and max. of <b>30</b> characters.";
            }
            else if (type == passInput)
            {
                passPanel.transform.position = activePos.transform.position;
                reqText.text = "Case sensitive, min. of <b>8</b> and max. of <b>30</b> characters, at least <b>1</b> uppercase, <b>1</b> lowercase, <b>1</b> number and <b>1</b> symbol.";
            }
            else if (type == nameInput)
            {
                namePanel.transform.position = activePos.transform.position;
            }
            reqPanel.SetActive(true);
        }
        else
        {
            if(type == userInput)
            {
                userPanel.transform.position = userDefaultPos;
            }
            else if (type == passInput)
            {
                passPanel.transform.position = passDefaultPos;
            }
            else if (type == nameInput)
            {
                namePanel.transform.position = nameDefaultPos;
            }
            reqPanel.SetActive(false);
        }   
    }
}
