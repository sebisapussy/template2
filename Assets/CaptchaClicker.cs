using UnityEngine;

public class CapchaClicker : MonoBehaviour
{

    // Layer mask to specify which layers the raycast should interact with

    private bool cursorchecker;
    public LayerMask raycastLayerMask;
    public CursorSelector cursorSelector;

    public int num;

    private void Update()
    {
        // Check if the mouse button is clicked

        // Cast a ray from the mouse position into the scene
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast using the specified layer mask
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayerMask) && PlayerMovement.chair)
        {
            // Check if the object clicked has this script attached
            if (hit.collider.gameObject == gameObject)
            {
                cursorSelector.ChangeMaterial(2);
                cursorchecker = true;
                if (Input.GetMouseButtonDown(0))
                {
                    if (gameObject.name == "VerifyButtonDONTCHANGENAME")
                    {
                        CaptchaScript.verifynum = true;
                    }
                    else
                    {
                        CaptchaScript.gridvaluereciever = num;
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


}