using System.Net.Http.Headers;
using UnityEngine;

public class AttachPlayerToCube : MonoBehaviour
{
    public GameObject player; // Assign your player GameObject in the Inspector
    public bool isAttached = false;
    private bool lastvalue = true;
    public PlayerMovement playerMovementScript; // Reference to player movement script
    private int value;
    public GameObject enter;
    public GameObject exit;


    void Update()
    {
        if (isAttached && !lastvalue)
        {
            lastvalue = true;
            AttachPlayer();
        }
        else if (!isAttached && lastvalue)
        {
            lastvalue = false;
            DetachPlayer();
        }

        if (isAttached && !PlayerMovement.Freeze) // Left-click
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                
                lastvalue = false;
                DetachPlayer();
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform && !isAttached)
            {
                enter.SetActive(true);
                value = 1;
            }
            else if (value == 1)
            {
                enter.SetActive(false);
                value = 0;
            }

            if (Input.GetKeyDown(KeyCode.E) && !PlayerMovement.Freeze) // Left-click
            {
                if (hit.transform == transform && !isAttached) // If clicking on the cube
                {
                    AttachPlayer();
                }

            }
        }

    }

    void AttachPlayer()
    {
        isAttached = true;
        exit.SetActive(true);
        player.transform.position = transform.position;
        player.transform.rotation = transform.rotation;
        player.transform.SetParent(transform);
        playerMovementScript.enabled = false;
    }

    public void DetachPlayer()
    {
        isAttached = false;
        exit.SetActive(false);
        player.transform.rotation = Quaternion.identity;
        player.transform.SetParent(null);
        playerMovementScript.enabled = true;
    }
}