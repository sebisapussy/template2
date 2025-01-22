using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public string targetTag = "ComputerArea";
    public LayerMask layerMask;
    public float maxLookAngle = 80f; // Maximum angle the player can look up or down

    private Transform target;
    private bool isLookingAtTarget = false;

    void Start()
    {
        // Find the target object with the specified tag
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        else
        {
            Debug.LogError("Target object with tag '" + targetTag + "' not found!");
        }
    }

    void Update()
    {
        if (target != null)
        {
            // Perform raycasting to check if the player is looking at the target
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.CompareTag(targetTag))
                {
                    isLookingAtTarget = true;

                    // Calculate the direction to the target
                    Vector3 directionToTarget = target.position - transform.position;

                    // Calculate the angle between current forward direction and direction to target
                    float angle = Vector3.Angle(transform.forward, directionToTarget);

                    // Restrict camera rotation if the angle exceeds the maximum look angle
                    if (angle > maxLookAngle)
                    {
                        // Calculate the restricted rotation
                        Vector3 newDirection = Vector3.RotateTowards(transform.forward, directionToTarget, maxLookAngle * Mathf.Deg2Rad, 0f);
                        transform.rotation = Quaternion.LookRotation(newDirection);
                    }
                }
                else
                {
                    isLookingAtTarget = false;
                }
            }
            else
            {
                isLookingAtTarget = false;
            }

            // If not looking at the target, reset camera rotation to default
            if (!isLookingAtTarget)
            {
                transform.rotation = Quaternion.identity;
            }
        }
    }
}