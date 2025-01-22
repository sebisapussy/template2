using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class chairmenu : MonoBehaviour
{
    public Transform Player;
    private int value;
    private int value1 = 1;
    private int value2;
    private int value3 = 1;
    public GameObject enter;
    public GameObject exit;

    public float fadeSpeed = 1f;

    public CanvasGroup textCanvasGroup;
    public AudioSource audioSource;

    void Start()
    {
        
    }

    void Update()
    {
        if (PlayerMovement.chair && PlayerMovement.Freeze)
        {
            if (value3 == 1)
            {
                if (audioSource != null)
                {
                    audioSource.Play();
                }
                else
                {
                    Debug.LogWarning("AudioSource not assigned.");
                }
                value3 = 0;
                exit.SetActive(true);
                StartCoroutine(FadeOut());
                value2 = 1;

            }

        }
        else if (value2 == 1)
        {
            textCanvasGroup.alpha = 1f;
            exit.SetActive(false);
            value1 = 1;
            value2 = 0;
            value3 = 1;
            if (audioSource != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource not assigned.");
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (Player)
            {
                if (enter)
                {
                    float dist = Vector3.Distance(Player.position, transform.position);
                    if (hit.transform == transform && dist < 3) // If clicking on the cube
                    {
                        enter.SetActive(true);
                        value = 1;
                    }
                    else if (value == 1)
                    {
                        enter.SetActive(false);
                        value = 0;
                    }

                }

            }
        }
    }


    IEnumerator FadeOut()
    {
        if (value1 == 1)
        {
            value1 = 0;
            textCanvasGroup.alpha = 1f;
            while (textCanvasGroup.alpha > 0f)
            {
                textCanvasGroup.alpha -= Time.deltaTime * fadeSpeed;
                yield return null;
            }
            exit.SetActive(false);
            textCanvasGroup.alpha = 1f;
        }
    }
}
