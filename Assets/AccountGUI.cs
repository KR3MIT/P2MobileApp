using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AccountGUI : MonoBehaviour
{
    [SerializeField] private TMP_Text reqText;
    [SerializeField] private GameObject reqPanel;

    [Header("User Panel")]
    [SerializeField] private GameObject userPanel;
    [SerializeField] private int userDefaultPos = 0;
    [SerializeField] private int userActivePos = 0;
    private TMP_InputField userInput;

    [Header("Password Panel")]
    [SerializeField] private GameObject passPanel;
    [SerializeField] private int passDefaultPos = 0;
    [SerializeField] private int passActivePos = 0;
    private TMP_InputField passInput;

    // Start is called before the first frame update
    void Start()
    { 
        reqPanel.SetActive(false);

        userInput = userPanel.GetComponent<TMP_InputField>();
        passInput = passPanel.GetComponent<TMP_InputField>();

        userPanel.transform.position = new Vector3(userPanel.transform.position.x, userDefaultPos, userPanel.transform.position.z);
        passPanel.transform.position = new Vector3(passPanel.transform.position.x, passDefaultPos, passPanel.transform.position.z);

        //call method userinputactive when inputfield is active or pressed
        userInput.onSelect.AddListener(delegate { userInputActive(); });
        userInput.onDeselect.AddListener(delegate { userInputDeactive(); });

        passInput.onSelect.AddListener(delegate { passInputActive(); });
        passInput.onDeselect.AddListener(delegate { passInputDeactive(); });

    }
    // this method is called when the user clicks on the input field
    public void userInputActive()
    {
        Debug.Log("Balls User Active"); 
        userPanel.transform.position = new Vector3(userPanel.transform.position.x, userActivePos, userPanel.transform.position.z);

        reqPanel.SetActive(true);
        reqText.text = "Must <b>not</b> contain: \n<b>Spaces</b> or more then <b>30</b> characters";

    }
    public void userInputDeactive()
    {
        Debug.Log("Balls User Deactive");
        userPanel.transform.position = new Vector3(userPanel.transform.position.x, userDefaultPos, userPanel.transform.position.z);

        reqPanel.SetActive(false);
    }
    public void passInputActive()
    {
        Debug.Log("Balls Pass Active");
        passInput.transform.position = new Vector3(passInput.transform.position.x, passActivePos, passInput.transform.position.z);

        reqPanel.SetActive(true);
        reqText.text = "Case sensitive, min of <b>8</b> and max of <b>30</b> characters, at least <b>1</b> uppercase, <b>1</b> lowercase, <b>1</b> number and <b>1</b> symbol.";
    }
    public void passInputDeactive()
    {
        Debug.Log("Balls Pass Deactive");
        passInput.transform.position = new Vector3(passInput.transform.position.x, passDefaultPos, passInput.transform.position.z);

        reqPanel.SetActive(false);
    }
}
