using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disc1 : MonoBehaviour
{
    // The position to teleport the player to
    public SpriteRenderer spriteRenderer;
    public Collider2D collider2D;
    public Collider2D poohscollider2D;

    public GameObject parkour;
    public GameObject ground;
    public GameObject wall;
    public GameObject ground2;
    public GameObject poohbear_old;
    public GameObject pillow_old;
    public GameObject freddy_old;
    public GameObject poohbear;
    public GameObject pillow;
    public GameObject freddy;
    public AudioSource audioSource;

    // Trigger when the player enters the teleporter's collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with the teleporter is the player
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            parkour.SetActive(true);
            wall.SetActive(false);
            poohbear_old.SetActive(false);
            pillow_old.SetActive(false);
            freddy_old.SetActive(false);
            poohbear.SetActive(true);
            pillow.SetActive(true);
            freddy.SetActive(true);
            collider2D.enabled = false;
            spriteRenderer.enabled = false;

            // Start the coroutine to disable the ground after 2 seconds
            StartCoroutine(DisableGroundAfterDelay(2f));
            StartCoroutine(DisableGround2AfterDelay(5.5f));
            StartCoroutine(DisableGround3AfterDelay(3f));
        }
    }

    // Coroutine to disable ground after a specified delay
    private IEnumerator DisableGroundAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Disable the ground object
        ground.SetActive(false);
    }

    private IEnumerator DisableGround2AfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Disable the ground object
        ground2.SetActive(false);
    }

    private IEnumerator DisableGround3AfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        poohscollider2D.isTrigger = !poohscollider2D.isTrigger;
    }
}
