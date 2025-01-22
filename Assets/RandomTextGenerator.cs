using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class RandomTextGenerator : MonoBehaviour
{
    public TextMeshProUGUI[] textMeshProObjects;
    public int maxLines = 13;
    public GameObject PopUp;

    private string[] lines;
    private int currentLineIndex = 0;
    public LayerMask raycastLayerMask;
    private int lineNumber = 1;
    private bool isPaused = false;
    private TextMeshProUGUI previousClickedTextMeshPro = null;
    private string previousText = "";
    public static bool frozenWin = false;

    public WindowManager windowManager;
    public GameObject someWindow;

    public GameObject Icon;

    private string[] textTemplates = new string[]
    {
        /*LightBlue*/"<mark=#00bcff57><color=\"black\"> NumImpor\t192.168.%%.%% \t192.168.%%.%%\tUDP\t%% %%%%%% > %%%%%\tLen=%%%</color><color=#F9AE0000>ddddddddddddddddddddddddddd</color></mark>",
        /*DarkBlue*/"<mark=#0000FF83><color=\"black\"> NumImpor\t192.168.%%.%% \t192.168.%%.%%\tUDP\t%% %%%%%% > %%%%%\tLen=%%%</color><color=#F9AE0000>ddddddddddddddddddddddddddd</color></mark>",
        /*Orange*/"<mark=#F9AE0094><color=\"black\"> NumImpor\t192.168.%%.%% \tBroadcast    \tARP\t%% %%%%%% > %%%%%\tARP</color><color=#F9AE0000>ddddddddddddddddddddddddddd</color></mark>",
        /*Green*/"<mark=#88FF0067><color=\"black\"> NumImpor\t192.168.%%.%% \t192.168.%%.%%\tHTTP\t%% %%%%%% > %%%%%\tLen=%%%</color><color=#F9AE0000>ddddddddddddddddddddddddddd</color></mark>",
        /*Grey*/"<mark=#0000008c><color=\"black\"> NumImpor\t192.168.%%.%% \t192.168.%%.%%\tTCP\t%% %%%%%% > %%%%%\tLen=%%%</color><color=#F9AE0000>ddddddddddddddddddddddddddd</color></mark>",
        /*lightpurple*/"<mark=#0800ff4d><color=\"grey\"> NumImpor\t192.168.%%.%% \t192.168.%%.%%\tUDP\t%% %%%%%% > %%%%%\tLen=%%%</color><color=#F9AE0000>ddddddddddddddddddddddddddd</color></mark>"
    };

    private int[] templateWeights = new int[] { 35, 27, 10, 1, 30, 35 }; // Weights for each template

    private void Start()
    {
        Initialize();
        ScheduleNextUpdate();
    }

    private void Initialize()
    {
        int objectCount = textMeshProObjects.Length;
        lines = new string[objectCount];
        currentLineIndex = 0;
        lineNumber = 1;
        isPaused = false;
        frozenWin = false;
    }

    private void UpdateText()
    {
        if (isPaused) return;

        // Generate a new random line
        string newLine = GenerateRandomLine();

        // Add the new line to the array
        lines[currentLineIndex] = newLine;

        // Move to the next line index, and wrap around if necessary
        currentLineIndex = (currentLineIndex + 1) % lines.Length;

        // Increment the line number for the next iteration
        lineNumber++;

        // Update TextMeshPro objects
        UpdateTextMeshPro();

        // Schedule the next update at a random interval
        ScheduleNextUpdate();
    }

    private void UpdateTextMeshPro()
    {
        int objectCount = textMeshProObjects.Length;

        for (int i = 0; i < objectCount; i++)
        {
            int index = (currentLineIndex + i) % objectCount;
            textMeshProObjects[i].text = lines[index] ?? "";
        }
    }

    private string GenerateRandomLine()
    {
        // Select a random template based on weights
        int randomIndex = GetWeightedRandomIndex();
        string template = textTemplates[randomIndex];

        // Replace all '%' in the template with a random number and update line number
        string result = "";
        foreach (char c in template)
        {
            if (c == '%')
            {
                result += Random.Range(0, 9).ToString();
            }
            else
            {
                result += c;
            }
        }

        // Update the line number in the template
        result = result.Replace(" NumImpor", $" {lineNumber}.");

        return result;
    }

    private int GetWeightedRandomIndex()
    {
        int totalWeight = 0;
        foreach (int weight in templateWeights)
        {
            totalWeight += weight;
        }

        int randomValue = Random.Range(0, totalWeight);
        for (int i = 0; i < templateWeights.Length; i++)
        {
            if (randomValue < templateWeights[i])
            {
                return i;
            }
            randomValue -= templateWeights[i];
        }
        return templateWeights.Length - 1;
    }

    private void ScheduleNextUpdate()
    {
        int randomBool = (Random.Range(0, 27));
        if (randomBool == 0 || randomBool == 1 || randomBool == 2 || randomBool == 6 || randomBool == 8 || randomBool == 10 || randomBool == 12 || randomBool == 14 || randomBool == 17 || randomBool == 18 || randomBool == 20 || randomBool == 23 || randomBool == 25)
        {
            float randomInterval = Random.Range(0.01f, 0.30f);
            Invoke(nameof(UpdateText), randomInterval);
        }
        else if (randomBool == 3 || randomBool == 4 || randomBool == 5 || randomBool == 7 || randomBool == 9 || randomBool == 11 || randomBool == 13 || randomBool == 15 || randomBool == 16 || randomBool == 19 || randomBool == 21 || randomBool == 22 || randomBool == 24) 
        {
            float randomInterval = Random.Range(0.0001f, 0.02f);
            Invoke(nameof(UpdateText), randomInterval);
        }
        else if (randomBool == 26)
        {
            Invoke(nameof(UpdateText), 1f);
        }
    }

    private void Update()
    {
        // Check for mouse clicks and determine which TextMeshPro object was clicked
        if (Input.GetMouseButtonDown(0) && PlayerMovement.chair && !frozenWin) // Left click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastLayerMask))
            {
                TextMeshProUGUI clickedTextMeshPro = hit.transform.GetComponent<TextMeshProUGUI>();
                if (clickedTextMeshPro != null)
                {
                    // If there was a previous click, revert its color
                    if (previousClickedTextMeshPro != null)
                    {
                        previousClickedTextMeshPro.text = previousText;
                    }

                    for (int i = 0; i < textMeshProObjects.Length; i++)
                    {
                        if (textMeshProObjects[i] == clickedTextMeshPro)
                        {
                            int lineIndex = (currentLineIndex + i) % lines.Length;
                            Debug.Log($"Clicked Line {lineIndex + 1}: {lines[lineIndex]}");
                            Debug.Log($"Text: {clickedTextMeshPro.text}");
                            windowManager.AdjustWindowPositions(someWindow);

                            // Pause the text scrolling
                            isPaused = true;

                            // Save the previous text and object for reverting later
                            previousClickedTextMeshPro = clickedTextMeshPro;
                            previousText = clickedTextMeshPro.text;

                            // Replace <mark> color
                            string updatedText = clickedTextMeshPro.text.Replace("<mark=#00bcff57>", "<mark=#00b8ff99>")
                                                                        .Replace("<mark=#0000FF83>", "<mark=#00b8ff99>")
                                                                        .Replace("<mark=#F9AE0094>", "<mark=#00b8ff99>")
                                                                        .Replace("<mark=#88FF0067>", "<mark=#00b8ff99>")
                                                                        .Replace("<mark=#0000008c>", "<mark=#00b8ff99>")
                                                                        .Replace("<mark=#0800ff4d>", "<mark=#00b8ff99>");
                            clickedTextMeshPro.text = updatedText;
                            if (updatedText.Contains("\tHTTP\t") && !frozenWin)
                            {
                                ShowObjectAndChildren(PopUp);
                                windowManager.AdjustWindowPositions(PopUp);
                                frozenWin = true;
                            }
                        }
                    }
                }
            }
        }
    }

    // Reset the script
    private void OnDisable()
    {
        CancelInvoke();
    }

    private void ShowObjectAndChildren(GameObject obj)
    {
        // Disable the object itself
        obj.SetActive(true);
        Icon.SetActive(true);
        WindowsTaskbar.UpdateActiveCubes();


        // Disable all children recursively
        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        Initialize();
        ScheduleNextUpdate();
    }
}
