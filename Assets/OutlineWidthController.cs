using UnityEngine;

public class OutlineWidthController : MonoBehaviour
{
    // Reference to the GameObject with the Outline component
    public GameObject targetObject;

    // Desired outline width when within range
    public float inRangeOutlineWidth = 10.0f;

    // Default outline width when out of range
    public float defaultOutlineWidth = 2.0f;

    // Radius within which the outline width will change
    public float effectRadius = 10.0f;

    // Reference to the player GameObject
    public Transform player;

    void Update()
    {
        if (targetObject != null && player != null)
        {
            // Calculate the distance between the player and the target object
            float distance = Vector3.Distance(player.position, targetObject.transform.position);

            // Get the Outline component on the target object
            Outline outline = targetObject.GetComponent<Outline>();

            if (outline != null)
            {
                // Set the outline width based on distance
                if (distance <= effectRadius)
                {
                    outline.OutlineWidth = inRangeOutlineWidth;
                }
                else
                {
                    outline.OutlineWidth = defaultOutlineWidth;
                }
            }
            else
            {
                Debug.LogWarning("No Outline component found on targetObject.");
            }
        }
        else
        {
            Debug.LogWarning("No targetObject or player assigned.");
        }
    }
}
