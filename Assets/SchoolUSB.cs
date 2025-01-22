using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SchoolUSB : MonoBehaviour
{

    public static bool ready_to_take;
    public GameObject targetObject;
    public float inRangeOutlineWidth = 10.0f;
    public float defaultOutlineWidth = 0.0f;
    public float effectRadius = 10.0f;
    public Transform player;

    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform && ready_to_take)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    gameObject.SetActive(false);
                    Inventory.usb = 1;
                }
            }
        }

        if (targetObject != null && player != null)
        {
            float distance = Vector3.Distance(player.position, targetObject.transform.position);
            Outline outline = targetObject.GetComponent<Outline>();

            if (outline != null)
            {
                if (distance <= effectRadius)
                {
                    if (ready_to_take)
                    {
                        outline.OutlineWidth = inRangeOutlineWidth;
                        return;
                    }
                }
                else
                {
                    outline.OutlineWidth = defaultOutlineWidth;
                }
            }
        }

    }
}

