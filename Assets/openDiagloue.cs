using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDialogue : MonoBehaviour
{
    public GameObject Chat;
    public float cooldownTime = 1.5f; // Time in seconds
    private float nextDialogueTime = 0f; // Tracks when the player can open dialogue again

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextDialogueTime)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    Chat.SetActive(true);
                    nextDialogueTime = Time.time + cooldownTime; // Set the next time the player can open dialogue
                }
            }
        }
    }
}
