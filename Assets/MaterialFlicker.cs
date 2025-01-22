using Unity.VisualScripting;
using UnityEngine;

public class MaterialFlicker : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public static int test = 0;                    // Variable to control flicker state
    public Material material1;               
    public Material material2;               // Alternate material for flicker
    private Renderer objectRenderer;         // Reference to the Renderer component
    private float flickerTimer = 0f;         // Timer to control flicker speed
    private float flickerInterval = 0.2f;      // Interval in seconds between flickers
    private bool useMaterial1 = true;        // Toggle flag to switch between materials
    private bool test2 = false;

    public GameObject glow;
    public GameObject postprocessing;
    public GameObject WireSharkFull;
    void Start()
    {
        glow.SetActive(false);
        postprocessing.SetActive(false);
        objectRenderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();

        // Set initial material to material1
        if (objectRenderer != null && material1 != null)
        {
            objectRenderer.material = material1;
        }
        else
        {
            Debug.LogWarning("Renderer or material1 is not set correctly.");
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetKeyDown(KeyCode.E)) // Left-click
            {
                if (hit.transform == transform) // If clicking on the cube
                {
                    test = 0;
                    WindowExitCode.namer = WireSharkFull;
                }

            }
        }

        if (test == 1)
        {
            FlickerMaterial();
        }
       else if (test == 0)
       {
            SetMaterial1();
       }
        
    }

    void FlickerMaterial()
    {
        flickerTimer += Time.deltaTime;

        if (flickerTimer >= flickerInterval && objectRenderer != null)
        {
            objectRenderer.material = useMaterial1 ? material1 : material2;

            glow.SetActive(true);
            postprocessing.SetActive(true);
            useMaterial1 = !useMaterial1;
            audioSource.enabled = true;
            flickerTimer = 0f;
            test2 = true;
        }
    }

    void SetMaterial1()
    {    
        // Only set material1 if it’s not already applied
        if (objectRenderer != null && objectRenderer.material != material1)
        {
            if (test2)
            {
                test2 = !test2;
                audioSource2.Stop();
                audioSource3.Play();
            }
            glow.SetActive(false);
            postprocessing.SetActive(false);
            audioSource.enabled = false;
            objectRenderer.material = material1;
            flickerTimer = 0f;  
            useMaterial1 = true; // Reset the flag to start with material1 again
        }
    }
}