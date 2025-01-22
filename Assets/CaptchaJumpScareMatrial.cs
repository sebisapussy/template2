using UnityEngine;

public class CaptchaJumpScareMatrial : MonoBehaviour
{
    public Material[] materials; // Array to hold the materials
    private Renderer objectRenderer; // Reference to the object's Renderer
    private int currentMaterialIndex = 0; // Index to track the current material

    void Start()
    {
        // Initialize the Renderer and ensure we have materials assigned
        objectRenderer = GetComponent<Renderer>();

        if (materials.Length > 0)
        {
            // Set the initial material to the first one in the array
            objectRenderer.material = materials[0];
        }
        else
        {
            Debug.LogWarning("No materials assigned to MaterialSwitcher.");
        }
    }

    public void SwitchMaterial()
    {
        // Ensure we have materials assigned before switching
        if (materials.Length > 0)
        {
            // Increment the index and wrap around if needed
            currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;

            // Apply the new material
            objectRenderer.material = materials[currentMaterialIndex];

            // Check if all materials have been cycled through
            if (currentMaterialIndex == 0)
            {
                Debug.Log("Finished");
                CaptchaScript.jumpscareint = 1;
                gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("No materials assigned to MaterialSwitcher.");
        }
    }
}