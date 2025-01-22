using System.Collections;
using UnityEngine;

public class DiaglouseSettings : MonoBehaviour
{
    public AudioSource mouseClickstart;
    public AudioSource mouseClickend;

    private bool isPlayingStartSound = false;

    public static bool inChat = false;

    // Update is called once per frame
    void Update()
    {
        // Check for left mouse button down
        if (Input.GetMouseButtonDown(0) && inChat) // 0 is the left mouse button
        {
            if (PlayerMovement.chair && PlayerMovement.Freeze)
            {
                mouseClickstart.Play(); 

            }
        }

        // Check for left mouse button up
        if (Input.GetMouseButtonUp(0) && inChat) // 0 is the left mouse button
        {
            if (PlayerMovement.chair && PlayerMovement.Freeze)
            {
                // Start coroutine to wait for the start sound to finish before playing the end sound
                StartCoroutine(PlayEndSoundAfterStart());
            }
        }
    }
    // Coroutine to wait for the start sound to finish
    private IEnumerator PlayEndSoundAfterStart()
    {
        // Wait until the mouseClickstart sound is no longer playing
        while (mouseClickstart.isPlaying)
        {
            yield return null; // Wait for the next frame
        }

        // Once the start sound is done, play the end sound
        mouseClickend.Play();
    }
}

