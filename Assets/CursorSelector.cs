using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSelector : MonoBehaviour
{
    // List of materials to choose from
    public List<Material> materials;

    // Reference to the MeshRenderer component
    private MeshRenderer meshRenderer;

    // Index of the currently applied material
    private int currentMaterialIndex = 0;

    void Start()
    {
        // Get the MeshRenderer component attached to this GameObject
        meshRenderer = GetComponent<MeshRenderer>();

        // Set the initial material if the list is not empty
        if (materials.Count > 0)
        {
            meshRenderer.material = materials[currentMaterialIndex];
        }
    }

    // Method to change the material based on index
    public void ChangeMaterial(int index)
    {
        // Check if the index is within the bounds of the list
        if (index >= 0 && index < materials.Count)
        {
            meshRenderer.material = materials[index];
            currentMaterialIndex = index; // Update current material index
        }
        else
        {
            Debug.LogWarning("Index out of bounds: " + index);
        }
    }

    // Method to change the material based on a reference
    public void ChangeMaterial(Material newMaterial)
    {
        // Check if the material exists in the list
        if (materials.Contains(newMaterial))
        {
            meshRenderer.material = newMaterial;
            currentMaterialIndex = materials.IndexOf(newMaterial); // Update current material index
        }
        else
        {
            Debug.LogWarning("Material not found in list: " + newMaterial.name);
        }
    }

    // Method to get the index of the current material
    public int GetMaterialIndex()
    {
        return currentMaterialIndex;
    }
}
