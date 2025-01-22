using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disc4cage : MonoBehaviour
{
    // The position to teleport the player to
    public SpriteRenderer spriteRenderer;
    public Collider2D collider2D;
    public AudioSource audioSource;

    public GameObject Silver;
    public GameObject chat;
    public GameObject oldSilver;
    public GameObject pickle;

    public float activationDelay = 1f;

    // Trigger when the player enters the teleporter's collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with the teleporter is the player
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            collider2D.enabled = false;
            spriteRenderer.enabled = false;
            oldSilver.SetActive(false);
            Silver.SetActive(true);
            StartCoroutine(ReactivateSilverAfterDelay());

        }
    }
    private IEnumerator ReactivateSilverAfterDelay()
    {
        yield return new WaitForSeconds(activationDelay);

        chat.SetActive(true);

        yield return new WaitForSeconds(15f);

        pickle.SetActive(true);
        
        yield return new WaitForSeconds(14f);
        
        Application.Quit();
    }
}