using UnityEngine;

public class WindowsLoginChecker : MonoBehaviour
{
    public GameObject objectToCheck; // The GameObject to check if it's active
    public GameObject objectToEnable; // The GameObject to enable if objectToCheck is active

    public GameObject IconToCheck;
    public GameObject IconToEnable;
    public WindowManager windowManager;


    void Update()
    {
        /**
        // Check if the specified GameObject is active
        if (objectToCheck.activeInHierarchy && WindowsLoginButton.loginactive == 0)
        {
            // Disable the specified GameObject

            HideObjectAndChildren(objectToCheck);

            ShowObjectAndChildren(objectToEnable);
        }
        **/
    }

    private void ShowObjectAndChildren(GameObject obj)
    {
        // Disable the object itself
        obj.SetActive(true);
        IconToEnable.SetActive(true);
        WindowsTaskbar.UpdateActiveCubes();


        // Disable all children recursively
        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void HideObjectAndChildren(GameObject obj)
    {
        // Disable the object itself
        obj.SetActive(false);
        IconToCheck.SetActive(false);
        WindowsTaskbar.UpdateActiveCubes();

        // Disable all children recursively
        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
