using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        
    }

    private void OnEnable()
    {
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

    private void OnDisable()
    {
        // Disable the step counter
        InputSystem.DisableDevice(StepCounter.current);
    }
}
