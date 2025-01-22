using UnityEngine;

public class DragSquareDrawer : MonoBehaviour
{
    public GameObject cubePrefab; // Assign the cube prefab in the inspector
    private GameObject currentCube;
    private bool isDragging = false;
    private Vector3 initialPosition;
    private Bounds windowdesktopBounds;
    public LayerMask cubeLayerMask;
    public float smoothMoveSpeed = 3f; // Adjust smooth move speed as needed
    private bool toggler = true;

    void Start()
    {
        currentCube = cubePrefab;

        // Assume the windowdesktop area has a BoxCollider
        BoxCollider windowdesktopArea = GameObject.FindWithTag("windowdesktop").GetComponent<BoxCollider>();
        if (windowdesktopArea != null)
        {
            windowdesktopBounds = windowdesktopArea.bounds;
        }
        else
        {
        }
    }

    void Update()
    {
        if (PlayerMovement.chair && PlayerMovement.Freeze)
        {
            if (HideUIOnLook.crosshairEnabled == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, cubeLayerMask))
                    {
                        GameObject clickedObject = hit.transform.gameObject;
                        if (hit.transform.CompareTag("windowdesktop"))
                        {
                            // Teleport the cube to the position
                            initialPosition = hit.point;
                            currentCube.transform.position = initialPosition;
                            currentCube.transform.localScale = Vector3.zero; // Reset scale
                            isDragging = true;
                        }
                    }
                }

                // Handle mouse drag
                if (isDragging && currentCube != null)
                {
                    
                    currentCube.SetActive(true);
                    Vector3 currentMousePosition = GetWorldPosition(Input.mousePosition);
                    Vector3 scale = currentMousePosition - initialPosition;
                    if (toggler) {
                        Vector3 zzScalenw = new Vector3(currentCube.transform.localScale.x, currentCube.transform.localScale.y, 0.05f);
                        currentCube.transform.localScale = zzScalenw;
                        currentCube.transform.position = currentMousePosition;
                        toggler = false;
                    }
                    

                    // Ensure the scale is within the bounds of the windowdesktop area
                    Vector3 clampedScale = ClampScaleWithinBounds(initialPosition, scale);
                    float xScale = Mathf.Abs(clampedScale.x);
                    float yScale = Mathf.Abs(clampedScale.y);
                    float zScale = 0.05f;

                    Vector3 newScale = new Vector3(xScale, yScale, zScale);

                    // Adjust the position of the cube to ensure correct alignment
                    Vector3 newPosition = initialPosition + new Vector3(clampedScale.x / 2, clampedScale.y / 2, 0);


                    currentCube.transform.position = Vector3.Lerp(currentCube.transform.position, newPosition, smoothMoveSpeed * Time.deltaTime);
                    currentCube.transform.localScale = Vector3.Lerp(currentCube.transform.localScale, newScale, smoothMoveSpeed * Time.deltaTime);
                }

                // Detect mouse release
                if (Input.GetMouseButtonUp(0))
                {
                    toggler = true;
                    isDragging = false;
                    currentCube.SetActive(false);
                }

            }
            else
            {
                if (currentCube != null)
                {
                    currentCube.SetActive(false);
                    toggler = true;
                    isDragging = false;
                }
            }
        }
        else
        {
            if (currentCube != null)
            {
                currentCube.SetActive(false);
                toggler = true;
                isDragging = false;
            }
        }

    }

    private Vector3 GetWorldPosition(Vector3 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    private Vector3 ClampScaleWithinBounds(Vector3 initialPos, Vector3 desiredScale)
    {
        Vector3 clampedScale = desiredScale;

        if (initialPos.x + desiredScale.x > windowdesktopBounds.max.x)
        {

            clampedScale.x = windowdesktopBounds.max.x - initialPos.x;
            currentCube.SetActive(false);
        }
        if (initialPos.x + desiredScale.x < windowdesktopBounds.min.x)
        {
            clampedScale.x = windowdesktopBounds.min.x - initialPos.x;
        }

        if (initialPos.y + desiredScale.y > windowdesktopBounds.max.y)
        {
            clampedScale.y = windowdesktopBounds.max.y - initialPos.y;
            currentCube.SetActive(false);
        }
        if (initialPos.y + desiredScale.y < windowdesktopBounds.min.y)
        {
            clampedScale.y = windowdesktopBounds.min.y - initialPos.y;
        }

        return clampedScale;
    }
}