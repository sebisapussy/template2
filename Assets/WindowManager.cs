using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Animations;

public class WindowManager : MonoBehaviour
{
    private GameObject lastInputGameObject = null;
    private Dictionary<GameObject, float> originalZPositions = new Dictionary<GameObject, float>();


    // Keep the oldWindow variables as before
    private GameObject FocusedWindow = null;
    public static GameObject FocusedWindow2 = null;
    private GameObject oldWindow = null;
    private GameObject oldWindow2 = null;
    private GameObject oldWindow3 = null;
    private GameObject oldWindow4 = null;
    private GameObject oldWindow5 = null;
    private GameObject oldWindow6 = null;
    private GameObject oldWindow7 = null;
    public static bool reset = false;

    private void Update()
    {
        
        if (reset)
        {
            reset = false;
            restoredefault();
        }
    }
    public void AdjustWindowPositions(GameObject inputGameObject)
    {
        //WindowsStartMenuButton.disableStartMenu();
        if (inputGameObject == lastInputGameObject)
        {
            return; // Exit if input is the same as last call
        }

        lastInputGameObject = inputGameObject;

        GameObject[] windows = GameObject.FindGameObjectsWithTag("windows");

        foreach (GameObject window in windows)
        {
            if (!originalZPositions.ContainsKey(window))
            {
                originalZPositions[window] = window.transform.position.z;
            }

            // Adjust the z position
            Vector3 newPosition = window.transform.position;

            // Reset to original z if it was adjusted before
            newPosition.z = originalZPositions[window];

            // Apply slight random offset
            newPosition.z += Random.Range(0.000001f, 0.000009f);
            // Adjust further based on whether it's an old window or the current input


            if (window != inputGameObject)
            {
                // Move forward a bit more
                newPosition.z += 0.01f;
            }
            if (window == oldWindow)
            {
                // Move backward slightly for oldWindow
                newPosition.z -= 0.001f;
            }
            else if (window == oldWindow2)
            {
                newPosition.z -= 0.00099f;
            }
            else if (window == oldWindow3)
            {
                newPosition.z -= 0.00098f;
            }
            else if (window == oldWindow4)
            {
                newPosition.z -= 0.00097f;
            }
            else if (window == oldWindow5)
            {
                newPosition.z -= 0.00096f;
            }
            else if (window == oldWindow6)
            {
                newPosition.z -= 0.00095f;
            }
            else if (window == oldWindow7)
            {
                newPosition.z -= 0.00094f;
            }
            // No adjustment needed if none of the above conditions match

            window.transform.position = newPosition;
        }

        // Update oldWindow variables
        FocusedWindow = inputGameObject;
        oldWindow7 = oldWindow6;
        oldWindow6 = oldWindow5;
        oldWindow5 = oldWindow4;
        oldWindow4 = oldWindow3;
        oldWindow3 = oldWindow2;
        oldWindow2 = oldWindow;
        oldWindow = inputGameObject;
        FocusedWindow2 = this.FocusedWindow;
        MaterialCopyScript.Updater();
    }

    public GameObject Focused()
    {
        return FocusedWindow;
    }

    public static GameObject isFocused()
    {
        return FocusedWindow2;
    }

    void restoredefault()
    {
        lastInputGameObject = null;
        originalZPositions = new Dictionary<GameObject, float>();
        FocusedWindow = null;
        FocusedWindow2 = null;
        oldWindow = null;
        oldWindow2 = null;
        oldWindow3 = null;
        oldWindow4 = null;
        oldWindow5 = null;
        oldWindow6 = null;
        oldWindow7 = null;
    }
}
