using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limitcameraframes : MonoBehaviour
{
    public float updateInterval = 0.1f;  // Time between updates in seconds (e.g., 10 FPS)

    void Start()
    {
        // Start calling UpdateCamera() repeatedly, but at the interval specified
        InvokeRepeating("UpdateCamera", 0f, updateInterval);
    }

    void UpdateCamera()
    {
        // This is where you would normally handle camera logic
        // Right now, we are just logging it to show it's being called at a lower frequency

        Debug.Log("Camera update at: " + Time.time);
        // You can add other logic here if needed
    }

    void OnDisable()
    {
        // Always good to stop repeating when the script is disabled or destroyed
        CancelInvoke("UpdateCamera");
    }
}