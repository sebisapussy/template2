using UnityEngine;

public class HideUIOnLook : MonoBehaviour
{
    public string computerTag = "ComputerArea";
    public static bool crosshairEnabled = true;
    public GameObject uiElement; // Reference to the UI element you want to hide

    void Update()
    {
        // Cast a ray from the center of the screen to check what the player is looking at
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the player is looking at an object with the specified tag
            if (hit.collider.CompareTag(computerTag))
            {
                // If looking at the computer area, hide the UI element
                uiElement.SetActive(false);
                crosshairEnabled = false;
            }
            else
            {
                // Otherwise, ensure the UI element is visible
                uiElement.SetActive(true);
                crosshairEnabled = true;
            }
        }
    }


}
