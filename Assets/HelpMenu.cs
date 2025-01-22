using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    public Material[] materials;  // List of materials to cycle through
    private Renderer objectRenderer;  // Reference to the Renderer component
    private int currentMaterialIndex = 0;  // Tracks the current material index
    public static bool switcher;
    void Start()
    {
        // Get the Renderer component on this GameObject
        objectRenderer = GetComponent<Renderer>();

        // Apply the first material if materials are assigned
        if (materials.Length > 0)
            objectRenderer.material = materials[currentMaterialIndex];
    }

    void Update()
    {
        // Check for button press, e.g., spacebar for this example
        if (switcher)
        {
            switcher = false;
            CycleMaterial();
        }
    }

    void CycleMaterial()
    {
        // Increment the index and loop back if it exceeds the array length
        currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;

        // Apply the next material
        objectRenderer.material = materials[currentMaterialIndex];
    }
}