using UnityEngine;

public class Teleporter : MonoBehaviour
{
    // The position to teleport the player to
    public Vector2 teleportTargetPosition;

    // Trigger when the player enters the teleporter's collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with the teleporter is the player
        if (other.CompareTag("Player"))
        {
            // Teleport the player to the target position
            other.transform.position = teleportTargetPosition;
        }
    }
}
