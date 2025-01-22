using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class uifadercutscene1 : MonoBehaviour
{
    public float fadeDuration = 1f; // Duration of the fade in/out
    private CanvasGroup canvasGroup;
    public bool fadeoutt;
    public Canvas canvasToEnable;
    public TMP_Text presentedText;
    public static string goal;
    public Animator animator;

    private float cooldownTime = 0.5f; // Cooldown time in seconds
    private float lastTabPressTime = -0.5f; // Initialize to allow immediate first press

    private void Awake()
    {
        // Add a CanvasGroup component if not already added
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        }
        StartCoroutine(FadeIn());
    }


    public IEnumerator FadeIn()
    {
        canvasToEnable.enabled = true;
        canvasGroup.alpha = 0f;
        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }
        fadeoutt = true;
        canvasGroup.alpha = 1f;
        SceneManager.LoadScene("2 Computer");
    }

    public IEnumerator FadeOut()
    {
        canvasToEnable.enabled = true;
        canvasGroup.alpha = 1f;
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        fadeoutt = false;
    }

    public void HideCanvas()
    {
        StartCoroutine(FadeOut());
    }
}
