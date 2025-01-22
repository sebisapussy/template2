using UnityEngine;

public class FollowSprite : MonoBehaviour
{
    public Transform target;  // The target to follow (the sprite)
    public float smoothSpeed = 0.125f;  // Smoothness factor for the follow movement
    public Vector3 offset;  // Optional offset from the target

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the SpriteRenderer component on the target if it's available
        if (target != null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void FixedUpdate()
    {
        if (target != null && !target.gameObject.activeInHierarchy)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            spriteRenderer.enabled = true;
        }
            
            // Calculate the desired position of the camera
            Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate between the camera's current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera position
        transform.position = smoothedPosition;
    }
}
