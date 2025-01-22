using System.Collections;
using UnityEngine;

public class USBActivatorRoom : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject object1;
    public GameObject object2;

    public GameObject SchoolLoginApp;

    public bool USB = false;

    public static int SchoolLoginCheck = 0;

    // Reference to the Animator components
    private Animator animator1;
    private Animator animator2;
    private Outline outline1;


    // Start is called before the first frame update
    void Start()
    {
        // Ensure GameObjects are assigned
        if (object1 != null && object2 != null)
        {
            // Get the Animator components from the GameObjects
            animator1 = object1.GetComponent<Animator>();
            animator2 = object2.GetComponent<Animator>();
            outline1 = gameObject.GetComponent<Outline>();

            // Make sure the Animator components exist
            if (animator1 == null || animator2 == null)
            {
                Debug.LogError("Animator components are missing from the GameObjects.");
                return;
            }
        }
        else
        {
            Debug.LogError("Please assign both object1 and object2 in the inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SchoolLoginCheck == 1)
        {
            SchoolLoginApp.SetActive(true);
        }

        if (Inventory.usb == 1)
        {
            outline1.enabled = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && PlayerMovement.chair)
            {
                if (hit.transform == transform)
                {
                    if (Input.GetKeyDown(KeyCode.E)) {
                        outline1.enabled = false;
                        Inventory.usb = 0;
                        if (animator1 != null && animator1.isInitialized)
                        {
                            animator1.enabled = true;
                            animator1.Rebind();
                            animator1.Update(0);
                        }

                        if (animator2 != null && animator2.isInitialized)
                        {
                            animator2.enabled = true;
                            animator2.Rebind();
                            animator2.Update(0);
                        }
                        StartCoroutine(WaitForAnimationEnd());
                    }
                }
            }
        }
    }
    private IEnumerator WaitForAnimationEnd()
    {
        // Wait until the animation is no longer playing
        while (animator1.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f ||
               animator1.IsInTransition(0))
        {
            yield return null;  // Wait until the next frame
        }

        SchoolLoginCheck = 1;
        animator1.enabled = false;

    }

}