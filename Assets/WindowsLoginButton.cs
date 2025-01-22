using TMPro; // Ensure you include this for TextMeshPro
using UnityEngine;

public class WindowsLoginButton : MonoBehaviour
{
    // Object to hide and its children
    public GameObject objectToHide; // The GameObject to check if it's active
    public GameObject objectToEnable; // The GameObject to enable if objectToCheck is active
    public TMP_Text displayText; // Using TMP_Text for TextMeshPro
    public TMP_Text presentedText;
    public GameObject IconToHide;
    public GameObject IconToEnable;
    public GameObject ScreenBlock;

    public static int loginactive;
    public static bool reset;

    // Layer mask to specify which layers the raycast should interact with
    private bool cursorchecker;
    public LayerMask raycastLayerMask;
    public CursorSelector cursorSelector;

    public WindowManager windowManager;

    private void Start()
    {
        loginactive = 0;
    }

    private void Update()
    {
        if (reset)
        {
            reset = false;
            loginactive = 0;
            displayText.text = "";
        }
        // Check if the mouse button is clicked
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast using the specified layer mask
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayerMask) && PlayerMovement.chair)
        {
            GameObject clickedObject = hit.transform.gameObject;
            // Check if the object clicked has this script attached
            if (hit.collider.gameObject == gameObject)
            {
                cursorSelector.ChangeMaterial(2);
                cursorchecker = true;
                if (Input.GetMouseButtonDown(0))
                {
                    cursorSelector.ChangeMaterial(0);
                    ShowObjectAndChildren(objectToEnable);
                    windowManager.AdjustWindowPositions(objectToEnable);

                    if (loginactive != 1)
                    {
                        // Normalize and compare the texst
                        if (IsTextMatchingTarget(displayText.text, "password"))
                        {
                            loginactive = 1;
                            ScreenBlock.SetActive(false);

                        }
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

    private bool IsTextMatchingTarget(string inputText, string target)
    {
        // Normalize and compare the text
        string normalizedText = NormalizeText(inputText);
        return normalizedText == target;
    }

    private string NormalizeText(string text)
    {
        // Convert to lowercase
        string lowercasedText = text.ToLower();

        // Remove all non-letter characters
        char[] lettersOnly = System.Array.FindAll(lowercasedText.ToCharArray(), c => char.IsLetter(c));

        // Return the cleaned string
        return new string(lettersOnly);
    }

    private void ShowObjectAndChildren(GameObject obj)
    {
        // Enable the object itself
        obj.SetActive(true);
        IconToEnable.SetActive(true);
        WindowsTaskbar.UpdateActiveCubes();

        // Enable all children recursively
        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void HideObjectAndChildren(GameObject obj)
    {
        // Disable the object itself
        obj.SetActive(false);
        IconToHide.SetActive(false);
        WindowsTaskbar.UpdateActiveCubes();

        // Disable all children recursively
        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
