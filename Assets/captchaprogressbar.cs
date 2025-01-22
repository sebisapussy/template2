/**
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class captchaprogressbar : MonoBehaviour
{
    public Transform progressBarFill; // Drag the ProgressBarFill GameObject here in the Inspector
    public float progress = 0.5f; // Example progress value (0 to 1)

    void Update()
    {
        // Ensure progress value is clamped between 0 and 1
        progress = Mathf.Clamp01(progress);

        // Update the scale of the progressBarFill to reflect the progress
        Vector3 scale = progressBarFill.localScale;
        scale.x = progress; // Adjust x scale based on progress
        progressBarFill.localScale = scale;
    }
}
**/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class captchaprogressbar : MonoBehaviour
{
    // Object to hide and its children

    // Layer mask to specify which layers the raycast should interact with
    private bool cursorchecker;
    public LayerMask raycastLayerMask;
    public CursorSelector cursorSelector;
    public Transform progressBarFill;


    public static int progress = 0; // Variable to track the progress
    public float lastPressTime; // Last time left click was pressed
    public float lastReleaseTime; // Last time left click was released
    public bool isHolding = false; // Tracks whether the button is being held
    private void Update()
    {
        if (PlayerMovement.chair && PlayerMovement.Freeze)
        {

            Vector3 scale = progressBarFill.localScale;
            scale.x = (progress / 100f); // Adjust x scale based on progress
            progressBarFill.localScale = scale;
            // Cast a ray from the mouse position into the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast using the specified layer mask
            if ((Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayerMask) && PlayerMovement.chair))
            {
                GameObject clickedObject = hit.transform.gameObject;
                // Check if the object clicked has this script attached
                if (hit.collider.gameObject == gameObject && CaptchaBUttonTester.tick == true)
                {
                    cursorSelector.ChangeMaterial(2);
                    cursorchecker = true;

                    // Check if the left mouse button is being held down
                    if (Input.GetMouseButton(0))
                    {




                        // Increment progress every 0.1 seconds
                        if (Time.time - lastPressTime >= 0.1f)
                        {
                            lastPressTime = Time.time;
                            progress += 4;
                            progress = Mathf.Min(progress, 100); // Cap progress at 100


                        }
                        isHolding = false;
                        lastReleaseTime = Time.time; // Reset release time when button is held
                    }
                    else
                    {
                        isHolding = false;
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

            // If the button isn't being pressed for a full second, decrease progress
            if (!isHolding && Time.time - lastReleaseTime >= 0.04f)
            {
                lastReleaseTime = Time.time;
                progress -= 1;
                progress = Mathf.Max(progress, 0); // Ensure progress doesn't go below 0
            }
        }
    }
    private void OnDisable()
    {
        // Reset progress to 0 when the GameObject is disabled
        progress = 0;

        // Update the progress bar to reflect the reset
        Vector3 scale = progressBarFill.localScale;
        scale.x = 0f;
        progressBarFill.localScale = scale;
    }

}
