using Unity.VisualScripting;
using UnityEngine;

public class MoveToCenter : MonoBehaviour
{
    public bool disableX;
    public bool disableY;
    public bool disableZ;
    public bool materialsaver = false;
    public string computerAreaTag = "ComputerArea";
    public LayerMask cubeLayerMask;
    public float smoothMoveSpeed = 3f; // Adjust smooth move speed as needed

    private bool isHolding = false;
    private GameObject computerArea;
    private Collider areaCollider;
    private Collider cubeCollider;
    private Vector3 smoothTargetPosition;

    public WindowManager windowManager;
    public GameObject someWindow;
    private Vector3 initialPosition;

    public static WindowManager windowManager2;



    public CursorSelector cursorSelector;


    void Start()
    {
        initialPosition = transform.position;
        windowManager2 = windowManager;
        computerArea = GameObject.FindGameObjectWithTag(computerAreaTag);
        if (computerArea == null)
        {
        }
        else
        {
            areaCollider = computerArea.GetComponent<Collider>();
            if (areaCollider == null)
            {
            }
        }

        cubeCollider = GetComponent<Collider>();
        if (cubeCollider == null)
        {
        }
    }

    void Update()
    {


        if (PlayerMovement.Freeze && PlayerMovement.chair)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, cubeLayerMask))
                {
                    if (hit.transform == transform)
                    {
                        if (areaCollider.bounds.Contains(transform.position))
                        {
                            isHolding = true;
                            windowManager.AdjustWindowPositions(someWindow);
                            cursorSelector.ChangeMaterial(1);
                            materialsaver = true;
                        }
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (materialsaver == true)
                {
                    cursorSelector.ChangeMaterial(0);
                    materialsaver = false;
                }
                isHolding = false;
            }

            if (isHolding)
            {
                MoveToCenterOfScreen();
            }

            if (transform.CompareTag("Cursor"))  // Check if the tag is "Cursor"
            {
                MoveToCenterOfScreen();
            }
        }
    }

    void MoveToCenterOfScreen()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.WorldToScreenPoint(transform.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenCenter);
        Vector3 newPosition = transform.position;

        if (!disableX) newPosition.x = worldPosition.x;
        if (!disableY) newPosition.y = worldPosition.y;
        if (!disableZ) newPosition.z = worldPosition.z;

        Bounds areaBounds = areaCollider.bounds;
        Bounds cubeBounds = cubeCollider.bounds;
        Vector3 halfSize = cubeBounds.extents;

        if (!disableX)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, areaBounds.min.x + halfSize.x, areaBounds.max.x - halfSize.x);
        }
        if (!disableY)
        {
            newPosition.y = Mathf.Clamp(newPosition.y, areaBounds.min.y + halfSize.y, areaBounds.max.y - halfSize.y);
        }
        if (!disableZ)
        {
            newPosition.z = Mathf.Clamp(newPosition.z, areaBounds.min.z + halfSize.z, areaBounds.max.z - halfSize.z);
        }

        // Smoothly move towards the new position
        smoothTargetPosition = newPosition;
        transform.position = Vector3.Lerp(transform.position, smoothTargetPosition, smoothMoveSpeed * Time.deltaTime);
    }


    public void RestorePosition()
    {
        transform.position = initialPosition;
    }

    public void HideObject()
    {
        gameObject.SetActive(false);
    }


}
