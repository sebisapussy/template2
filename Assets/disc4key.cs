using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disc4key : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Collider2D collider2D;

    public GameObject gate;
    public AudioSource audioSource;

    // Trigger when the player enters the teleporter's collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with the teleporter is the player
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            collider2D.enabled = false;
            spriteRenderer.enabled = false;
            gate.SetActive(false);
        }
    }
}
