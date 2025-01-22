using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptchaBox : MonoBehaviour
{
    // Object to hide and its children
    public GameObject objectToHide;

    // Layer mask to specify which layers the raycast should interact with

    private bool cursorchecker;
    public LayerMask raycastLayerMask;
    public CursorSelector cursorSelector;

    public WindowManager windowManager;
    public GameObject someWindow;



    private void Update()
    {
        // Check if the mouse button is clicked

        // Cast a ray from the mouse position into the scene
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast using the specified layer mask
        if ((Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayerMask) && PlayerMovement.chair))
        {
            GameObject clickedObject = hit.transform.gameObject;
            // Check if the object clicked has this script attached
            if (hit.collider.gameObject == gameObject && CaptchaBUttonTester.tick == false)
            {
                cursorSelector.ChangeMaterial(2);
                cursorchecker = true;
                if (Input.GetMouseButtonDown(0))
                {
                    if (objectToHide != null)
                    {
                        ShowObjectAndChildren(objectToHide);
                    }
                }
            }
            else
            {
                if (cursorchecker)
                {
                    cursorSelector.ChangeMaterial(0);
                    cursorchecker = false;
                }
            }
        }
    }

    private void ShowObjectAndChildren(GameObject obj)
    {
        // Disable the object itself
        obj.SetActive(true);
    }
}