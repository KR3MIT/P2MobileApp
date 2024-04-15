using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AccountGUI : MonoBehaviour
{
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
    }
    public void userInputDeactive()
    {
        Debug.Log("Balls User Deactive");
        userPanel.transform.position = new Vector3(userPanel.transform.position.x, userDefaultPos, userPanel.transform.position.z);
    }
    public void passInputActive()
    {
        Debug.Log("Balls Pass Active");
        passInput.transform.position = new Vector3(passInput.transform.position.x, passActivePos, passInput.transform.position.z);
    }
    public void passInputDeactive()
    {
        Debug.Log("Balls Pass Deactive");
        passInput.transform.position = new Vector3(passInput.transform.position.x, passDefaultPos, passInput.transform.position.z);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
