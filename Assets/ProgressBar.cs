using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    private int currentExp;
    private int lvlUpExp;
    [SerializeField] private Image mask;
    [SerializeField] private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        currentExp = Character.instance.exp;
        lvlUpExp = Character.instance.xpToLevel;
        GetCurrentFill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetCurrentFill()
    {
        text.text = "Exp: " + currentExp + "/" + lvlUpExp;

        float fillAmount = (float)currentExp / (float)lvlUpExp;
        mask.fillAmount = fillAmount;
    }
}
