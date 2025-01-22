using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textcompdent;
    public CanvasGroup textCanvasGroup;

    public Image imageComponent; // Reference to the single Image component
    public Material defaultMaterial; // Default material for other lines
    public Material youMaterial; // Material for lines starting with '#'
    public Material otherMaterial; // Material for lines starting with '@'

    public string[] lines;
    public float textSpeed;
    public float fadeSpeed = 1f;
    public PlayerMovement playerMovementScript;
    public Transform targetToRotate; // GameObject to rotate to face the player
    public Transform playerTransform; // Reference to the player's transform

    private int index;
    private Material imageMaterial;
    private Quaternion originalRotation; // Store the initial rotation of the GameObject

    public GameObject parentGameObject;
    private string lineToDisplay;

    public AudioSource start;
    public AudioSource nextSlider;

    void OnEnable()
    {
        if (start != null)
        {
            start.Play();
        }
        else
        {
            Debug.Log("AudioSource not assigned.");
        }
        Transform parentTransform = parentGameObject.transform;

        foreach (Transform child in parentTransform)
        {
            if (child != transform)
            {
                child.gameObject.SetActive(false);
            }
        }



    originalRotation = targetToRotate.rotation;

        playerMovementScript.enabled = false;
        DiaglouseSettings.inChat = true;
        textCanvasGroup.alpha = 0f;
        textcompdent.text = string.Empty;

        // Rotate to face the player
        StartCoroutine(RotateToFacePlayer());

        // Clone the material and set initial alpha to 0 for fade-in effect
        imageMaterial = imageComponent.material = new Material(imageComponent.material);
        SetImageAlpha(0f);
        StartCoroutine(FadeIn());
        StartDialouge();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Compare with the processed line (lineToDisplay) instead of lines[index]
            if (textcompdent.text == lineToDisplay)
            {
                if (nextSlider != null)
                {
                    nextSlider.Play();
                }
                else
                {
                    Debug.Log("AudioSource not assigned.");
                }
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textcompdent.text = lineToDisplay;
            }
        }
    }


    void StartDialouge()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        // Check the first character to decide the material
        char firstChar = lines[index].Length > 0 ? lines[index][0] : '\0';
        if (firstChar == '#')
        {
            imageComponent.material = new Material(youMaterial);
        }
        else if (firstChar == '@')
        {
            imageComponent.material = new Material(otherMaterial);
        }
        else
        {
            imageComponent.material = new Material(defaultMaterial);
        }

        // Set initial alpha to 0 for fade-in effect
        imageMaterial = imageComponent.material;
        SetImageAlpha(0f);
        StartCoroutine(FadeIn());

        // Remove the first character if it’s '#' or '@' so it doesn’t display in the text
        lineToDisplay = (firstChar == '#' || firstChar == '@') ? lines[index].Substring(1) : lines[index];

        // Display each character in the line with a delay
        textcompdent.text = string.Empty;
        foreach (char c in lineToDisplay.ToCharArray())
        {
            textcompdent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }



    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textcompdent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            StartCoroutine(FadeOut());
            StartCoroutine(RotateBackToOriginal());
        }
    }

    IEnumerator FadeIn()
    {
        while (textCanvasGroup.alpha < 1f || GetImageAlpha() < 1f)
        {
            textCanvasGroup.alpha += Time.deltaTime * fadeSpeed;
            SetImageAlpha(GetImageAlpha() + Time.deltaTime * fadeSpeed);
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        while (textCanvasGroup.alpha > 0f || GetImageAlpha() > 0f)
        {
            textCanvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            SetImageAlpha(GetImageAlpha() - Time.deltaTime * fadeSpeed);
            yield return null;
        }

        // Rotate the GameObject back to its original rotation
        

        DiaglouseSettings.inChat = false;

        textCanvasGroup.alpha = 1f;
        SetImageAlpha(1f);
        gameObject.SetActive(false);
        playerMovementScript.enabled = true;
    }

    void SetImageAlpha(float alpha)
    {
        Color color = imageMaterial.color;
        color.a = Mathf.Clamp01(alpha);
        imageMaterial.color = color;
    }

    float GetImageAlpha()
    {
        return imageMaterial.color.a;
    }

    IEnumerator RotateToFacePlayer()
    {
        // Calculate direction to player
        Vector3 directionToPlayer = playerTransform.position - targetToRotate.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

        float elapsedTime = 0f;
        float rotationDuration = 0.1f; // Adjust this duration to control the rotation speed

        Quaternion initialRotation = targetToRotate.rotation;

        // Smoothly rotate the object towards the player
        while (elapsedTime < rotationDuration)
        {
            targetToRotate.rotation = Quaternion.Slerp(initialRotation, lookRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the final rotation is exactly the target rotation
        targetToRotate.rotation = lookRotation;
    }


    IEnumerator RotateBackToOriginal()
    {
        float elapsedTime = 0f;
        float rotationDuration = 1f; // Adjust rotation duration as needed
        Debug.Log("sdd");
        while (elapsedTime < rotationDuration)
        {
            targetToRotate.rotation = Quaternion.Slerp(targetToRotate.rotation, originalRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure exact restoration of original rotation
        targetToRotate.rotation = originalRotation;
    }
}
