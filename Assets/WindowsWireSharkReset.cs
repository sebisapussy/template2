using UnityEngine;

public class WindowsWireSharkReset : MonoBehaviour
{
    // Object to hide and its children
    public GameObject objectToHide;
    public GameObject Popup;
    public GameObject Icon;
    public WindowManager windowManager;

    // Layer mask to specify which layers the raycast should interact with

    private bool cursorchecker;
    public LayerMask raycastLayerMask;
    public CursorSelector cursorSelector;

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
                    if (objectToHide != null)
                    {
                        HideObjectAndChildren(Popup);
                        HideObjectAndChildren(objectToHide);
                        ShowObjectAndChildren(objectToHide);
                        windowManager.AdjustWindowPositions(objectToHide);
                        RandomTextGenerator.frozenWin = false;
                    }
                    else
                    {
                    }
                }
                else
                {
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

    private void HideObjectAndChildren(GameObject obj)
    {
        // Disable the object itself
        obj.SetActive(false);
        Icon.SetActive(false);
        WindowsTaskbar.UpdateActiveCubes();
        cursorSelector.ChangeMaterial(0);

        // Disable all children recursively
        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void ShowObjectAndChildren(GameObject obj)
    {
        // Disable the object itself
        obj.SetActive(true);
        Icon.SetActive(true);
        WindowsTaskbar.UpdateActiveCubes();


        // Disable all children recursively
        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}