using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class EmbarkEndStats : MonoBehaviour
{
    [SerializeField] private Button _continue;

    private SceneStates _state;

    // Start is called before the first frame update
    void Start()
    {
        _state = GameObject.FindWithTag("Player").GetComponent<SceneStates>();
        _continue.onClick.AddListener(ToHome);
        


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
