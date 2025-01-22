using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WindowsTaskbar : MonoBehaviour
{
    public GameObject finishCube2;
    public List<GameObject> cubeList2 = new List<GameObject>();
    private static List<GameObject> activeCubes = new List<GameObject>();
    private static List<GameObject> activationOrder = new List<GameObject>();
    private static Vector3 initialPosition;
    private static Dictionary<GameObject, Vector3> initialCubePositions = new Dictionary<GameObject, Vector3>();
    private static float fixedGap = 0.035f; // Adjust this gap as needed

    public static GameObject finishCube;
    public static GameObject placeholder;
    public GameObject placeholder2;
    public GameObject IGNORETHISNOTRELATEDBUTIMPORTANT2;
    public static GameObject IGNORETHISNOTRELATEDBUTIMPORTANT;
    public static List<GameObject> cubeList;
    public static bool reset;
    public GameObject KaliTaskbar;


    public void Start()
    {
        finishCube = this.finishCube2;
        placeholder = this.placeholder2;
        cubeList = this.cubeList2;
        IGNORETHISNOTRELATEDBUTIMPORTANT = this.IGNORETHISNOTRELATEDBUTIMPORTANT2;

        if (finishCube == null)
        {
            Debug.LogError("Finish cube not assigned!");
            return;
        }
        initialPosition = finishCube.transform.position;

        // Store the initial positions of all cubes
        foreach (var cube in cubeList)
        {
            initialCubePositions[cube] = cube.transform.position;
        }

        UpdateActiveCubes();
        placeholder.SetActive(false);
    }


    void Update()
    {
        if (reset)
        {
            reset = false;
            ResetToDefault();
        }
    }

    public static void UpdateActiveCubes()
    {
        bool needsReposition = false;

        foreach (var cube in cubeList)
        {
            if (cube.activeSelf && !activeCubes.Contains(cube))
            {
                activeCubes.Add(cube);
                activationOrder.Remove(cube);  // Ensure it's removed before adding to the end
                activationOrder.Add(cube);
                needsReposition = true;
            }
            else if (!cube.activeSelf && activeCubes.Contains(cube))
            {
                activeCubes.Remove(cube);
                activationOrder.Remove(cube);
                needsReposition = true;
            }
        }

        if (needsReposition)
        {
            RepositionCubes();
        }
    }

    public static void RepositionCubes()
    {
        if (activeCubes.Count(obj => obj != placeholder) == 1)
        {
            placeholder.SetActive(false);
            activeCubes.Remove(placeholder);
            activationOrder.Remove(placeholder);
        }
        else
        {
            placeholder.SetActive(true);
            activeCubes.Remove(placeholder);
            activationOrder.Remove(placeholder);
            activeCubes.Add(placeholder);
            activationOrder.Add(placeholder);
        }

        float currentX = initialPosition.x; // Start from the left edge

        // Position each cube
        foreach (var cube in activationOrder)
        {
            if (activeCubes.Contains(cube))
            {
                // Calculate the left edge of the cube
                float cubeLeftEdge = currentX - cube.transform.localScale.x / 2f;

                // Set cube position at cubeLeftEdge
                cube.transform.position = new Vector3(cubeLeftEdge, initialPosition.y, initialPosition.z);

                // Move currentX to the right for the next cube
                currentX = cubeLeftEdge + cube.transform.localScale.x + fixedGap;
            }
        }
    }

    // Method to reset to the default state
    public void ResetToDefault()
    {
        // Deactivate all active cubes
        foreach (var cube in activeCubes)
        {
            if (cube != KaliTaskbar)
            {
                cube.SetActive(false);
            }
        }

        // Reset each cube to its initial position
        foreach (var cube in cubeList)
        {
            cube.transform.position = initialCubePositions[cube];
        }

        // Clear the active cube list and activation order
        activeCubes.Clear();
        activationOrder.Clear();

        // Reset the finishCube to its initial position
        finishCube.transform.position = initialPosition;

        // Reactivate the placeholder
        
        
        placeholder.SetActive(false);



        Debug.Log("Taskbar reset to default state.");
    }
}

