using Unity.VisualScripting;
using UnityEngine;

public class bossbar : MonoBehaviour
{
    // Reference to the RectTransform component of the panel
    public RectTransform panelRectTransform;

    public float newXScale = 2.0f;
    public static bool change = false;

    public GameObject song;
    public GameObject parkour;
    public GameObject camera2;
    private bool remeber;

    void Update()
    {
     if (change)
        {
            change = false;
            ChangeXScale(newXScale);
        }
    }
    void ChangeXScale(float xScale)
    {
        // Get the current localScale
        Vector3 currentScale = panelRectTransform.localScale;

        float tester = currentScale.x - xScale;
        if (tester < 0)
        {
            tester = 0;
        }
        else if (tester < 0.50 && !remeber)
        {
            remeber = true;
            tester = 0.5f;
            parkour.SetActive(true);
            camera2.SetActive(true);
            song.SetActive(false);
        }

        panelRectTransform.localScale = new Vector3(tester, currentScale.y, currentScale.z);
    }
}
