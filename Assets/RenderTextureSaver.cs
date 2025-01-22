using System.Collections;
using System.IO;
using UnityEngine;

public class RenderTextureSaver : MonoBehaviour
{
    public RenderTexture renderTexture; // Assign your render texture here
    public float saveInterval = 5f; // Save every 5 seconds
    private int screenshotCount = 0;

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        // Start the save routine
        StartCoroutine(SaveRenderTextureRoutine());
    }

    private IEnumerator SaveRenderTextureRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(saveInterval);
            SaveRenderTextureAsPNG();
        }
    }

    private void SaveRenderTextureAsPNG()
    {
        // Create a Texture2D the size of the render texture
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

        // Copy the pixels from the RenderTexture to the Texture2D
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
        RenderTexture.active = null;

        // Convert the Texture2D to a PNG
        byte[] bytes = texture.EncodeToPNG();

        // Generate a file path and save the PNG
        string filePath = Path.Combine(Application.persistentDataPath, $"Screenshot_{screenshotCount}.png");
        File.WriteAllBytes(filePath, bytes);
        screenshotCount++;

        Debug.Log($"Saved screenshot to {filePath}");

        // Clean up
        Destroy(texture);
    }
}
