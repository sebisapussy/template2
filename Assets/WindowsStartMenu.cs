using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class WindowsStartMenuButton : MonoBehaviour
{
    public LayerMask raycastLayerMask;
    public GameObject obj;
    public static GameObject obj2;
    private int joe = 0;
    public CursorSelector cursorSelector;
    private bool cursorchecker = false;
    public static bool reset = false;

    void Start()
    {
        obj2 = obj;
        //obj.SetActive(false);
    }

    void Update()
    {

        if (reset)
        {
            reset = false;
            obj2 = obj;
            //obj.SetActive(false);
            joe = 0;
            cursorchecker = false;
        }

        // Check if left mouse button is clicked
        if (PlayerMovement.Freeze && PlayerMovement.chair) {
                // Cast a ray from the mouse position into the scene
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, raycastLayerMask))
                {
                // Check if the object hit has the same GameObject as obj
                if (hit.collider.gameObject == gameObject)
                {
                    cursorchecker = true;
                    cursorSelector.ChangeMaterial(2);
                        if (Input.GetMouseButtonDown(0))
                        {
                            if (joe == 0)
                            {
                                obj.SetActive(true);
                                joe = 1;
                            }
                            else
                            {
                                obj.SetActive(false);
                                joe = 0;
                            }
                    }
                            // Enable the object

                }
                else
                {
                if (cursorchecker)
                    {
                        cursorchecker = false;
                        cursorSelector.ChangeMaterial(0);
                    }
                }
            }
      }
    }

    public static void disableStartMenu()
    {
        obj2.SetActive(false);
    }


}