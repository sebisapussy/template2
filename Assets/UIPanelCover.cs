using UnityEngine;

public class UIPanelCover : MonoBehaviour
{
    public GameObject cube;
    public RectTransform uiPanel;
    public Camera mainCamera;

    // Define a fallback position for the panel when the cube is not visible
    public Vector2 fallbackPosition = new Vector2(100, 100); // Adjust as needed

    void Update()
    {
        // Get the cube's renderer and its bounding box
        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        if (cubeRenderer == null) return;

        // Get the corners of the cube's bounding box in world space
        Vector3[] cubeCorners = new Vector3[8];
        Bounds bounds = cubeRenderer.bounds;

        cubeCorners[0] = bounds.min;
        cubeCorners[1] = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
        cubeCorners[2] = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
        cubeCorners[3] = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
        cubeCorners[4] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        cubeCorners[5] = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
        cubeCorners[6] = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
        cubeCorners[7] = bounds.max;

        // Initialize min and max screen positions for the UI panel
        Vector3 minScreenPos = new Vector3(float.MaxValue, float.MaxValue, 0);
        Vector3 maxScreenPos = new Vector3(float.MinValue, float.MinValue, 0);

        // Check if any corner of the cube is in view
        bool isCubeVisible = false;

        foreach (Vector3 corner in cubeCorners)
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(corner);

            // Only consider corners that are in front of the camera
            if (screenPos.z > 0)
            {
                isCubeVisible = true; // Cube is visible
                minScreenPos = Vector3.Min(minScreenPos, screenPos);
                maxScreenPos = Vector3.Max(maxScreenPos, screenPos);
            }
        }

        // If the cube is visible, adjust the panel to cover it
        if (isCubeVisible)
        {
            Vector2 panelSize = (maxScreenPos - minScreenPos);
            uiPanel.position = minScreenPos + (Vector3)(panelSize / 2); // Center the panel
            uiPanel.sizeDelta = panelSize; // Adjust size to cover the cube
        }
        else
        {
            // If the cube is not visible, move the panel to the fallback position
            uiPanel.anchoredPosition = fallbackPosition;
            uiPanel.sizeDelta = new Vector2(100, 100); // Set a default size, adjust as needed
        }
    }
}
