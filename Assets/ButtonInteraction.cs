using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Plane dragPlane;
    private Camera mainCamera;

    public bool allowXAxis = true;
    public bool allowYAxis = true;
    public bool allowZAxis = true;

    void Start()
    {
        mainCamera = Camera.main;
        dragPlane = new Plane(Vector3.up, transform.position);
    }

    void Update()
    {
        // Handle left-click to start dragging
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                offset = transform.position - hit.point;
            }
        }

        // Handle drag movement
        if (isDragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (dragPlane.Raycast(ray, out distance))
            {
                Vector3 targetPosition = ray.GetPoint(distance) + offset;
                if (!allowXAxis) targetPosition.x = transform.position.x;
                if (!allowYAxis) targetPosition.y = transform.position.y;
                if (!allowZAxis) targetPosition.z = transform.position.z;

                transform.position = targetPosition;
            }
        }

        // Handle right-click to disappear
        if (Input.GetMouseButtonDown(1))
        {
            gameObject.SetActive(false);
        }
    }
}
