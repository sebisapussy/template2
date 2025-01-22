using UnityEngine;

public class WindowOpenDesktop : MonoBehaviour
{
    // Object to hide and its children
    public GameObject objectToHide;
    public GameObject Icon;
    public static GameObject namer;

    // Layer mask to specify which layers the raycast should interact with

    private bool cursorchecker;
    public LayerMask raycastLayerMask;
    public CursorSelector cursorSelector;

    public WindowManager windowManager;
    public GameObject someWindow;
    private void Update()
    {
        // Cast a ray from the mouse position into the scene
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast using the specified layer mask
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayerMask) && PlayerMovement.chair)
        {
            if (gameObject == namer)
            {
                ShowObjectAndChildren(objectToHide);
                windowManager.AdjustWindowPositions(someWindow);
                namer = null;
            }

            // Check if the object clicked has this script attached
            if (hit.collider.gameObject == gameObject)
            {

                cursorSelector.ChangeMaterial(2);
                cursorchecker = true;
                if (Input.GetMouseButtonDown(0))
                {
                    if (objectToHide != null)
                    {
                        ShowObjectAndChildren(objectToHide);
                        windowManager.AdjustWindowPositions(someWindow);

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
        Icon.SetActive(true);
        WindowsTaskbar.UpdateActiveCubes();
        
        cursorSelector.ChangeMaterial(0);

        // Disable all children recursively
        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}