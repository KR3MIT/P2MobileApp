using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestAnimatorManager : MonoBehaviour
{
    public Animator animator1;
    public Animator animator2;
    public Animator animator3;

    [SerializeField] private Button Chest1Button;
    [SerializeField] private Button Chest2Button;
    [SerializeField] private Button Chest3Button;
    // Start is called before the first frame update
    private void Start()
    {
        Chest1Button.onClick.AddListener(Chest1);
        Chest2Button.onClick.AddListener(Chest2);
        Chest3Button.onClick.AddListener(Chest3);
    }

    private void Chest1()
    {
        animator1.SetBool("ChestClicked", true);
        
    }

    private void Chest2()
    {
        animator2.SetBool("ChestClicked", true);
        
    }

    private void Chest3()
    {
        animator3.SetBool("ChestClicked", true);
        
    }

}
