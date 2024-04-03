using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;

public class DataCollector : MonoBehaviour
{
    // Variables
    public static int currentSteps;

    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        

    }
        
    // Update is called once per frame
    void Update()
    {
        currentSteps = StepCounter.current.stepCounter.ReadValue();
        Debug.Log("Number of steps: " + currentSteps);


    }

    private void OnEnable() // Runs when the Gameobject this script is on is enabled (when the game starts)
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.ACTIVITY_RECOGNITION"))
        {
            Permission.RequestUserPermission("android.permission.ACTIVITY_RECOGNITION");
        }
        // Enable the step counter
        InputSystem.AddDevice<StepCounter>();
        if (StepCounter.current == null)
        {
            Debug.LogError("StepCounter is not connected");
        }
        else
        {
            InputSystem.EnableDevice(StepCounter.current);
            currentSteps = StepCounter.current.stepCounter.ReadValue();
        }
        Debug.Log("Number of steps: " + currentSteps);
    }

    private void OnDisable() // Runs when the Gameobject this script is on is disabled
    {
        // Disable the step counter
        InputSystem.DisableDevice(StepCounter.current);
    }
}
