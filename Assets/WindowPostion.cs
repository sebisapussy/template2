using UnityEngine;

public class WindowPostion : MonoBehaviour
{
    public GameObject someWindow;
    private WindowManager windowManager;

    void Start()
    {
        // Assuming the WindowManager script is attached to a GameObject named "WindowManager"
        windowManager = GameObject.Find("WindowManager").GetComponent<WindowManager>();
    }

    void Update()
    {
        // Call the method and pass the someWindow GameObject
        windowManager.AdjustWindowPositions(someWindow);
    }
}
