using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class RibbonManager : MonoBehaviour
{
    public TMP_Text[] ribbonTexts; // Array of TextMeshProUGUI components that display the ribbon content

    [TextArea] public string[] ribbonCollapsed; // Array to store collapsed text for each ribbon
    [TextArea] public string[] ribbonExpanded; // Array to store expanded text for each ribbon

    private bool[] isRibbonExpanded; // Array to track if each ribbon is expanded
    private Canvas canvas; // Reference to the Canvas component
    private int test = 0;
    public CursorSelector cursorSelector;
    // References to PlayerMovement conditions

    private void Start()
    {
        // Initialize the expanded state array based on the number of ribbons
        isRibbonExpanded = new bool[ribbonTexts.Length];
        canvas = GetComponentInParent<Canvas>(); // Get the Canvas component from the parent

        // Ensure that arrays for ribbonCollapsed and ribbonExpanded have the same length as ribbonTexts
        if (ribbonCollapsed.Length != ribbonTexts.Length || ribbonExpanded.Length != ribbonTexts.Length)
        {
            return;
        }

        UpdateTextVisibility(); // Initialize text visibility
    }

    private void Update()
    {
        // Check the conditions to show or hide text
        if (PlayerMovement.Freeze && PlayerMovement.chair)
        {
                
 
                HandleClick(); // Ensure the text is up-to-date

            UpdateText();
        }
        else
        {
            // Hide the text if conditions are not met
            HideText();
        }
    }

    private void HandleClick()
    {
        if (PlayerMovement.Freeze && PlayerMovement.chair)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            // Create a list to receive all raycast results
            var raycastResults = new System.Collections.Generic.List<RaycastResult>();
            var raycaster = canvas.GetComponent<GraphicRaycaster>();

            // Raycast using the GraphicRaycaster
            raycaster.Raycast(pointerData, raycastResults);
            int tester2 = 0;
            foreach (var result in raycastResults)
            {
                TMP_Text clickedText = result.gameObject.GetComponent<TMP_Text>();
                if (clickedText != null)
                {
                    
                    for (int i = 0; i < ribbonTexts.Length; i++)
                    {
                        if (clickedText == ribbonTexts[i])
                        {
                            test = i+6;
                            if (!Input.GetMouseButtonDown(0))
                            {
                                break;
                            }

                            float fontSize = ribbonTexts[i].fontSize;

                            // Calculate the number of lines in the expanded text
                            int lineCount = ribbonExpanded[i].Split('\n').Length;

                            // Calculate the offset based on the number of lines and font size
                            float yOffset = 0.75f * lineCount * fontSize;

                            // Toggle the state of the clicked ribbon
                            isRibbonExpanded[i] = !isRibbonExpanded[i];
                            if (isRibbonExpanded[i])
                            {
                                MoveRibbonsBasedOnIndex(i, -yOffset);
                            }
                            else
                            {
                                MoveRibbonsBasedOnIndex(i, yOffset);
                            }
                            UpdateText(); // Update the text display
                            break; // Exit loop once the ribbon is toggled
                        }
                    }
                    break; // Exit loop once a clickable text is found
                }
            }
        }
    }

    private void UpdateText()
    {
        // Update the displayed texat based on the expanded or collapsed state
        if (PlayerMovement.Freeze && PlayerMovement.chair)
        {
            for (int i = 0; i < ribbonTexts.Length; i++)
            {
                if (i == 6)
                {  
                    if (test > 0)
                    {
                        ribbonTexts[6].text = ribbonExpanded[test];
                    }
                    else
                    {
                        ribbonTexts[i].text = isRibbonExpanded[i] ? ribbonExpanded[i] : ribbonCollapsed[i];
                    }
                }
                else
                {
                    ribbonTexts[i].text = isRibbonExpanded[i] ? ribbonExpanded[i] : ribbonCollapsed[i];
                }

            }
            
        }
    }

    private void HideText()
    {
        // Set all ribbon texts to an empty string when conditions are not met
        for (int i = 0; i < ribbonTexts.Length; i++)
        {
            ribbonTexts[i].text = "";
        }
    }

    private void UpdateTextVisibility()
    {
        // Initially set text visibility based on the current state of PlayerMovement

            if (PlayerMovement.Freeze && PlayerMovement.chair)
            {
                UpdateText();
            }
            else
            {
                HideText();
            }
        
    }

    // Method to move specific ribbon texts up or down
    public void MoveRibbonsBasedOnIndex(int indexThreshold, float yOffset)
    {
        for (int i = indexThreshold + 1; i < ribbonTexts.Length; i++)
        {
            RectTransform rectTransform = ribbonTexts[i].GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition += new Vector2(0, yOffset);
            }
        }
    }
}
