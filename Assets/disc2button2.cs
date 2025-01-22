using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disc2button2 : MonoBehaviour
{
    // The position to teleport the player to
    public SpriteRenderer spriteRenderer;
    public Collider2D collider2D;

    public GameObject diddy;

    // Trigger when the player enters the teleporter's collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with the teleporter is the player
        if (other.CompareTag("Player"))
        {
            collider2D.enabled = false;
            spriteRenderer.enabled = false;
            diddy.SetActive(true);
        }
    }
}
