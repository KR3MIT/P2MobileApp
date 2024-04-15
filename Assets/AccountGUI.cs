using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AccountGUI : MonoBehaviour
{
    [Header("User Panel")]
    [SerializeField] private GameObject userPanel;
    [SerializeField] private int userDefultPos = 0;
    [SerializeField] private int userActivePos = 0;
    private TMP_InputField userInput;

    [Header("Password Panel")]
    [SerializeField] private GameObject passPanel;
    [SerializeField] private int passDefultPos = 0;
    [SerializeField] private int passActivePos = 0;
    private TMP_InputField passInput;

    // Start is called before the first frame update
    void Start()
    {
        userInput = userPanel.GetComponent<TMP_InputField>();
        passInput = passPanel.GetComponent<TMP_InputField>();

        userPanel.transform.position = new Vector3(userPanel.transform.position.x, userDefultPos, userPanel.transform.position.z);

        //call method userinputactive when inputfield is active or pressed
        userInput.onSelect.AddListener(delegate { InputActive(userPanel); });
        userInput.onDeselect.AddListener(delegate { InputDeactive(passPanel); });

        passInput.onSelect.AddListener(delegate { InputActive(passPanel); });
        passInput.onDeselect.AddListener(delegate { InputDeactive(passPanel); });

    }
    // this method is called when the user clicks on the input field
    public void InputActive(GameObject inputField)
    {
        Debug.Log("Balls " + inputField + " Active"); 
            
    }
    public void InputDeactive(GameObject inputField)
    {
        Debug.Log("Balls " + inputField + " Deactive");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
