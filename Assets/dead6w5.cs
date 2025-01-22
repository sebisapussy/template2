using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dead6w5 : MonoBehaviour
{
    public Collider2D collider2D;
    public GameObject fire;
    public GameObject samy;
    public GameObject samyparticles;
    public GameObject key;
    public GameObject chat;

    // Trigger when the player enters the teleporter's collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with the teleporter is the player
        if (other.CompareTag("tester"))
        {
            collider2D.enabled = false;
            fire.SetActive(true);
            chat.SetActive(true);
            StartCoroutine(ActivateAfterDelay(2f));


        }
    }

    private IEnumerator ActivateAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);
        samy.SetActive(false);
        key.SetActive(true);
        samyparticles.SetActive(true);
    }


}
