using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
        userInput.onSelect.AddListener(delegate { userInputActive(); });
        userInput.onDeselect.AddListener(delegate { userInputDeactive(); });
        //pass
        passInput.onSelect.AddListener(delegate { passInputActive(); });
        passInput.onDeselect.AddListener(delegate { passInputDeactive(); });

        nameInput.onSelect.AddListener(delegate { nameInputActive(); });
        nameInput.onDeselect.AddListener(delegate { nameInputDeactive(); });
    }
    //user panel toggle
    #region(user panel toggle)
    public void userInputActive()
    {
        Debug.Log("Balls User Active"); 
        userPanel.transform.position = activePos.transform.position;
        reqPanel.SetActive(true);
        reqText.text = "Must contain: <b>No</b> spaces, \nmin. of <b>3</b> and max. of <b>30</b> characters.";
    }
    public void userInputDeactive()
    {
        Debug.Log("Balls User Deactive");
        userPanel.transform.position = userDefaultPos;
        reqPanel.SetActive(false);
    }
    #endregion
    //password panel toggle
    #region(password panel toggle)
    public void passInputActive()
    {
        Debug.Log("Balls Pass Active");
        passPanel.transform.position = activePos.transform.position;
        reqPanel.SetActive(true);
        reqText.text = "Case sensitive, min. of <b>8</b> and max. of <b>30</b> characters, at least <b>1</b> uppercase, <b>1</b> lowercase, <b>1</b> number and <b>1</b> symbol.";
    }
    public void passInputDeactive()
    {
        Debug.Log("Balls Pass Deactive");
        passPanel.transform.position = passDefaultPos;
        reqPanel.SetActive(false);
    }
    #endregion

    #region(name input toggle)
    public void nameInputActive()
    {
        Debug.Log("Balls Name Active");
        namePanel.transform.position = activePos.transform.position;
        reqPanel.SetActive(true);
    }
    public void nameInputDeactive()
    {
        Debug.Log("Balls Name Deactive");
        namePanel.transform.position = nameDefaultPos;
        reqPanel.SetActive(false);
    }
    #endregion

}
