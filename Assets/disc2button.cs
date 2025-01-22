using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disc2button : MonoBehaviour
{
    // The position to teleport the player to
    public SpriteRenderer spriteRenderer;
    public Collider2D collider2D;
    public AudioSource audioSource;

    public GameObject big;
    public GameObject wall;
    public GameObject otherwall;
    public GameObject diddy;
    public GameObject button;

    // Trigger when the player enters the teleporter's collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with the teleporter is the player
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            collider2D.enabled = false;
            spriteRenderer.enabled = false;
            big.SetActive(true);
            diddy.SetActive(false);
            wall.SetActive(false);
            button.SetActive(true);
            otherwall.SetActive(false);
        }
    }
}
