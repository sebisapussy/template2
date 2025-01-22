using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class CaptchaScript : MonoBehaviour
{
    public int CaptchaVersion = 1;          
    public bool gameStarted = false;
    public static int gridvaluereciever = 0;
    public static bool verifynum;
    public float scaleDuration = 0.3f;

    // Predefined sequences for rounds
    private List<int> correctSequenceRound1 = new List<int> { 1, 5, 9 }; // Round 1 sequence
    private List<int> correctSequenceRound2 = new List<int> { 2, 3, 5,6 , 8,9 }; // Round 3 sequence
    private List<int> correctSequenceRound22 = new List<int> { 1, 2, 4, 5}; // Round 3 sequence
    private List<int> correctSequenceRound3 = new List<int> { 3, 6, 7 }; // Round 3 sequence

    private List<int> playerInput = new List<int>();
    public List<int> currentCorrectSequence = new List<int>(); // Stores correct images for the current round

    public CaptchaJumpScareMatrial capchamaterial;
    public Material[] round1Materials; // Materials for round 1
    public Material[] round2Materials; // Materials for round 1
    public Material[] round2Materials2; // Materials for round 1
    public Material[] round3Materials; // Materials for round 3
    public Material[] randomNamedMaterials; // Array of named materials for random rounds (round 2 and round 4)
    public GameObject[] images; // Image objects to update materials
    public GameObject verifyimage;

    public int currentRound = 1;
    private Vector3[] originalScales; // To store original scale of images
    private HashSet<int> selectedImages = new HashSet<int>(); // To track selected images
    private string currentCategory; // Category for random rounds
    public GameObject visualIndicatorPrefab; // Prefab for the visual indicator
    private List<GameObject> activeIndicators = new List<GameObject>(); // Track active indicators

    public bool jumpscare;
    public static int jumpscareint = 0;
    public GameObject JumpScareImage;
    public GameObject PreJumpScareImage;

    public TMP_Text displayText;

    public float scaleDownFactor = 0.8f;

    public static bool reset;

    void Start()
    {
        // Store the original scale of all images at the start
        originalScales = new Vector3[images.Length];
        for (int i = 0; i < images.Length; i++)
        {
            originalScales[i] = images[i].transform.localScale;
        }

        StartGame();
    }


    public void Finished()
    {
        Debug.Log("All rounds complete! Captcha finished.");
        gameStarted = false;
        jumpscare = false;
        jumpscareint = 0;
        gameObject.SetActive(false);
        CaptchaBUttonTester.starter = true;
    }

    public void OnEnable()
    {

        if (CaptchaVersion == 1)
        {
            currentRound = 5;
        }
        if (CaptchaVersion == 2)
        {
            currentRound = 1;
        }
        StartGame();
    }


    private void Update()
    {
        if (reset)
        {
            reset = false;
            Finished();
        }

        if (gameStarted)
        {
            int value;
            if (gridvaluereciever > 0)
            {
                value = gridvaluereciever;
                gridvaluereciever = 0;
                OnImageClicked(value);
            }
            if (verifynum)
            {
                verifynum = false;
                value = 0;
                gridvaluereciever = 0;
                displayText.text = "";
                StartCoroutine(ScaleImage(verifyimage, verifyimage.transform.localScale * 0.9f, 0.10f, true));
                CheckSequence();
            }
            if (jumpscare)
            {
                PreJumpScareImage.SetActive(true);
            }
            if (jumpscareint == 1)
            {
                jumpscareint = 0;
                jumpscare = false;
                PreJumpScareImage.SetActive(false);
                JumpScareImage.SetActive(true);
                NextRound();
            }
        }
    }

    IEnumerator ScaleImage(GameObject image, Vector3 targetScale, float duration, bool scaleBack = false)
    {
        Vector3 initialScale = image.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            image.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        image.transform.localScale = targetScale;

        if (scaleBack)
        {
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                image.transform.localScale = Vector3.Lerp(targetScale, initialScale, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            image.transform.localScale = initialScale;
        }
    }

    public void OnImageClicked(int gridNum)
    {
        if (gameStarted)
        {
            if (jumpscare)
            {
                capchamaterial.SwitchMaterial();
                return;
            }
            if (selectedImages.Contains(gridNum))
            {
                StartCoroutine(ScaleImage(images[gridNum - 1], originalScales[gridNum - 1], scaleDuration));
                DeselectImage(gridNum);
            }
            else
            {
                StartCoroutine(ScaleImage(images[gridNum - 1], originalScales[gridNum - 1] * scaleDownFactor, scaleDuration));
                SelectImage(gridNum);
            }
        }
    }

    void SelectImage(int gridNum)
    {
        selectedImages.Add(gridNum);
        playerInput.Add(gridNum);

        GameObject indicator = Instantiate(visualIndicatorPrefab, images[gridNum - 1].transform.position, Quaternion.identity);
        indicator.transform.SetParent(images[gridNum - 1].transform);
        activeIndicators.Add(indicator);
    }

    void DeselectImage(int gridNum)
    {
        selectedImages.Remove(gridNum);
        playerInput.Remove(gridNum);

        GameObject indicatorToRemove = activeIndicators.Find(indicator => indicator.transform.parent == images[gridNum - 1].transform);
        if (indicatorToRemove != null)
        {
            activeIndicators.Remove(indicatorToRemove);
            Destroy(indicatorToRemove);
        }
    }

    void CheckSequence()
    {
        RestoreAllScales();

        if (playerInput.Count == currentCorrectSequence.Count)
        {
            HashSet<int> playerSet = new HashSet<int>(playerInput);
            HashSet<int> correctSet = new HashSet<int>(currentCorrectSequence);

            if (playerSet.SetEquals(correctSet))
            {
                Debug.Log("Correct sequence! Moving to the next round.");
                NextRound();
                return;
            }
            else
            {
                Debug.Log("Incorrect sequence.");
            }
        }
        else
        {
            Debug.Log("Incorrect number of selections.");
        }

        // Reapply materials when sequence is incorrect
        if (currentRound == 1 || currentRound == 5 || currentRound == 6)
        {
            SetupRandomCategoryRound(); // Reset the materials for the random category round (round 2 and 4)
        }
        else if (currentRound == 2 || currentRound == 3 || currentRound == 4 || currentRound == 7)
        {
            ChangeMaterialsForRound(); // Reset predefined materials for round 1 and 3
        }

        playerInput.Clear();
        selectedImages.Clear();
        RestoreAllScales();
    }


    void NextRound()
    {
        playerInput.Clear();
        selectedImages.Clear();
        RestoreAllScales();

        currentRound++;

        if (currentRound <= 7 && CaptchaVersion==1 || currentRound <= 4 && CaptchaVersion == 2)
        {
            ChangeMaterialsForRound();
        }
        else
        {
            Finished();
        }
    }

    void ChangeMaterialsForRound()
    {
        switch (currentRound)
        {
            case 1:
                SetupRandomCategoryRound(); // Random round for round 2
                break;
            case 2:
                currentCorrectSequence = new List<int>(correctSequenceRound2);
                for (int i = 0; i < images.Length; i++)
                {
                    images[i].GetComponent<Renderer>().material = round2Materials[i];
                }
                displayText.text = "Bike";
                break;

            case 3:
                displayText.text = "doorframe";
                jumpscare = true;
                currentCorrectSequence = new List<int>(correctSequenceRound3);
                for (int i = 0; i < images.Length; i++)
                {
                    images[i].GetComponent<Renderer>().material = round3Materials[i];
                }
                break;

            case 4: //Sammy face and say clikc the pussy
                currentCorrectSequence = new List<int>(correctSequenceRound1);
                for (int i = 0; i < images.Length; i++)
                {
                    images[i].GetComponent<Renderer>().material = round1Materials[i];
                }
                displayText.text = "pussys";
                break;


            case 5:
                SetupRandomCategoryRound(); // Random round for round 4
                break;

            case 6:
                SetupRandomCategoryRound(); // Random round for round 4
                break;

            case 7:
                currentCorrectSequence = new List<int>(correctSequenceRound22);
                for (int i = 0; i < images.Length; i++)
                {
                    images[i].GetComponent<Renderer>().material = round2Materials2[i];
                }
                displayText.text = "Truck";
                break;
        }
    }

    void SetupRandomCategoryRound()
    {
        currentCategory = RemoveNumbers(randomNamedMaterials[Random.Range(0, randomNamedMaterials.Length)].name);
        Debug.Log("Random category for this round: " + currentCategory);
        displayText.text = currentCategory;
        currentCorrectSequence.Clear();
        int categoryPosition = Random.Range(0, images.Length);

        for (int i = 0; i < images.Length; i++)
        {
            Material randomMaterial;

            if (i == categoryPosition)
            {
                Material categoryMaterial;
                do
                {
                    categoryMaterial = randomNamedMaterials[Random.Range(0, randomNamedMaterials.Length)];
                } while (RemoveNumbers(categoryMaterial.name) != currentCategory);

                randomMaterial = categoryMaterial;
            }
            else
            {
                randomMaterial = randomNamedMaterials[Random.Range(0, randomNamedMaterials.Length)];
            }

            images[i].GetComponent<Renderer>().material = randomMaterial;

            if (RemoveNumbers(randomMaterial.name) == currentCategory)
            {
                currentCorrectSequence.Add(i + 1);
            }
        }
    }

    void RestoreAllScales()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].transform.localScale = originalScales[i];
        }
        foreach (GameObject indicator in activeIndicators)
        {
            if (indicator != null)
            {
                Destroy(indicator);
            }
        }
        activeIndicators.Clear();
    }

    private void StartGame()
    {
        
        playerInput.Clear();
        selectedImages.Clear();
        print("t");
        PreJumpScareImage.SetActive(false);
        JumpScareImage.SetActive(false);
        jumpscare = false;
        jumpscareint = 0;
        gameStarted = true;
        RestoreAllScales();
        ChangeMaterialsForRound();
    }

    string RemoveNumbers(string input)
    {
        string pattern = @"\d+";
        return Regex.Replace(input, pattern, "");
    }
}