using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolUsbOtherPC : MonoBehaviour
{
    public GameObject usb;
    public bool has_usb = false;
    public GameObject ignore_its_for_the_story_chat;
    public static bool hacked = false;
    private bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform && !finished)
            {
                if (Input.GetKeyDown(KeyCode.E) && Inventory.usb == 1)
                {
                    has_usb = true;
                    Inventory.usb = 0;
                    usb.SetActive(true);
                }
                else if (Input.GetKeyDown(KeyCode.E) && has_usb)
                {
                    if (ignore_its_for_the_story_chat != null)
                    {
                        finished = true;
                        hacked = true;
                        ignore_its_for_the_story_chat.SetActive(true);
                        return;
                    }
                    has_usb = false;
                    Inventory.usb = 1;
                    usb.SetActive(false);
                    hacked = true;
                    finished = true;
                }
            }
        }
    }
}
