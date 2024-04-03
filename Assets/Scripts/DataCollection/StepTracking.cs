using TMPro;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;
using UnityEngine.UI;
using WebSocketSharp;

public class StepTracking : MonoBehaviour
{

    [SerializeField] private TMP_Text stepText;

    internal void PermissionCallbacks_PermissionDeniedAndDontAskAgain(string permissionName)
    {
        Debug.Log($"{permissionName} PermissionDeniedAndDontAskAgain");
    }

    internal void PermissionCallbacks_PermissionGranted(string permissionName)
    {
        Debug.Log($"{permissionName} PermissionCallbacks_PermissionGranted");
    }

    internal void PermissionCallbacks_PermissionDenied(string permissionName)
    {
        Debug.Log($"{permissionName} PermissionCallbacks_PermissionDenied");
    }

    private void Awake()

    {

        if (Permission.HasUserAuthorizedPermission("android.permission.ACTIVITY_RECOGNITION"))
        {
            // The user authorized use of the microphone.
        }
        else
        {
            bool useCallbacks = false;
            if (!useCallbacks)
            {
                // We do not have permission to use the microphone.
                // Ask for permission or proceed without the functionality enabled.

                Permission.RequestUserPermission("android.permission.ACTIVITY_RECOGNITION");
            }
            else
            {
                var callbacks = new PermissionCallbacks();
                callbacks.PermissionDenied += PermissionCallbacks_PermissionDenied;
                callbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
                callbacks.PermissionDeniedAndDontAskAgain += PermissionCallbacks_PermissionDeniedAndDontAskAgain;
                Permission.RequestUserPermission("android.permission.ACTIVITY_RECOGNITION", callbacks);
            }

        }

    }

    void Start()
    {
#if PLATFORM_ANDROID
        if(Application.platform != RuntimePlatform.Android)
        {
            return;
        }
        InputSystem.EnableDevice(AndroidStepCounter.current);

        AndroidStepCounter.current.MakeCurrent();
        Debug.Log("shit" + AndroidStepCounter.current);
#endif
    }

    void Update()
    {
        if(AndroidStepCounter.current.enabled) { 
        stepText.text = AndroidStepCounter.current.stepCounter.ReadValue().ToString();
        }
    }
}