using UnityEngine;
using System;
using System.IO;

public class ScreenRepeat : MonoBehaviour
{
    public GameObject cubeObject; // Reference to the cube object in your scene
    public Material cubeMaterial; // Reference to the material with Standard shader

    private Texture2D capturedTexture; // Texture to hold the captured screenshot

    void Start()
    {
        // Start capturing screenshots every second
        InvokeRepeating("CaptureAndApplyScreenshot", 0f, 0.1f);
    }

    void CaptureAndApplyScreenshot()
    {
        string screenshotName = "snapshot.png";
        string screenshotPath = Path.Combine(Application.persistentDataPath, screenshotName);

        // Capture the screenshot
        ScreenCapture.CaptureScreenshot(screenshotPath);

        Debug.Log("Screenshot captured and saved to: " + screenshotPath);

        // Load the screenshot as a texture
        LoadScreenshot(screenshotPath);
    }

    void LoadScreenshot(string path)
    {
        // Check if the file exists
        if (File.Exists(path))
        {
            // Read the file data
            byte[] fileData = File.ReadAllBytes(path);
            capturedTexture = new Texture2D(2, 2); // Create a new texture
            bool loadSuccess = capturedTexture.LoadImage(fileData);

            if (loadSuccess)
            {
                Debug.Log("Screenshot loaded as texture successfully.");

                // Apply the texture to the cube's material
                ApplyTextureToCubeMaterial();
            }
            else
            {
                Debug.LogError("Failed to load screenshot as texture.");
            }
        }
        else
        {
            Debug.LogError("Screenshot file not found at: " + path);
        }
    }

    void ApplyTextureToCubeMaterial()
    {
        if (cubeObject != null)
        {
            Renderer renderer = cubeObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Assign the texture to the cube's material
                if (cubeMaterial != null)
                {
                    cubeMaterial.mainTexture = capturedTexture;
                    renderer.material = cubeMaterial;

                    Debug.Log("Texture applied to cube material.");
                }
                else
                {
                    Debug.LogError("Cube material reference not set.");
                }
            }
            else
            {
                Debug.LogError("Cube object does not have a Renderer component.");
            }
        }
        else
        {
            Debug.LogError("Cube object reference not set in the inspector.");
        }
    }
}