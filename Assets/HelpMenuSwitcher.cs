using UnityEngine;

public class HelpMenuSwitcher : MonoBehaviour
{
    private bool cursorchecker;
    public LayerMask raycastLayerMask;
    public CursorSelector cursorSelector;

    private void Update()
    {
        // Cast a ray from the mouse position into the scene
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast using the specified layer mask
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayerMask) && PlayerMovement.chair)
        {


            if (hit.collider.gameObject == gameObject)
            {

                cursorSelector.ChangeMaterial(2);
                cursorchecker = true;
                if (Input.GetMouseButtonDown(0))
                {
                    HelpMenu.switcher = true;
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