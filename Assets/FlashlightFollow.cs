using UnityEngine;

public class FlashlightFollow : MonoBehaviour
{
    public Transform target;  // Assign your camera here
    public float followSpeed = 5f;  // Speed of following
    public float rotationSpeed = 5f;  // Speed of rotation matching

    void LateUpdate()
    {
        // Smoothly follow position with Lerp
        transform.position = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);

        // Smoothly follow rotation with Lerp
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotationSpeed * Time.deltaTime);
    }
}
