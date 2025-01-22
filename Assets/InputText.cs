using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;

public class TextInput : MonoBehaviour
{
    // Reference to the TextMeshProUGUI component where typed text will be displayed
    public TMP_Text displayText;
    public TMP_Text presentedText;

    // String to hold the current text typed by the user
    private string typedText = "";

    // Cursor symbol
    private string cursor = "|";

    // Boolean to control cursor visibility
    private bool cursorVisible = true;

    // Index to track the position of the cursor within the text
    private int cursorPosition = 0;


    public WindowManager windowManager;
    public GameObject someWindow;



    void Start()
    {
        // Start the coroutine to blink the cursor
        StartCoroutine(BlinkCursor());
    }

    void Update()
    {
        if (someWindow == windowManager.Focused() && PlayerMovement.Freeze && PlayerMovement.chair)
        {
            HandleInput();
            UpdateDisplayText();
        }

    }

    void HandleInput()
    {
        // Handle regular characters input
        foreach (char c in Input.inputString)
        {
            if (c == '\b')
            {
                // Handle backspace
                if (typedText.Length > 0 && cursorPosition > 0)
                {
                    typedText = typedText.Remove(cursorPosition - 1, 1);
                    cursorPosition--;
                }
            }
            else if (c == '\n' || c == '\r')
            {
                // Handle newline characters (optional)
                typedText = typedText.Insert(cursorPosition, "\n");
                cursorPosition++;
            }
            else if (IsCtrlPressed())
            {
                // Handle Ctrl + Backspace to remove entire word
                if (typedText.Length > 0 && cursorPosition > 0)
                {
                    int endIndex = cursorPosition - 1;
                    while (endIndex > 0 && typedText[endIndex - 1] != ' ' && typedText[endIndex - 1] != '\n')
                    {
                        endIndex--;
                    }
                    typedText = typedText.Remove(endIndex, cursorPosition - endIndex);
                    cursorPosition = endIndex;
                }
            }
            else if (IsShiftPressed())
            {
                // Handle shift behavior (capitalize letter)
                typedText = typedText.Insert(cursorPosition, char.ToUpper(c).ToString());
                cursorPosition++;
            }
            else
            {
                // Append each character to the typedText string (ignore '\b' and newline characters)
                typedText = typedText.Insert(cursorPosition, c.ToString());
                cursorPosition++;
            }
        }

        // Handle arrow key navigation
        if (Input.GetKeyDown(KeyCode.LeftArrow) && cursorPosition > 0)
        {
            cursorPosition--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && cursorPosition < typedText.Length)
        {
            cursorPosition++;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveCursorUp();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveCursorDown();
        }
    }

    void MoveCursorUp()
    {
        int currentLineStart = typedText.LastIndexOf('\n', cursorPosition - 1);
        if (currentLineStart == -1)
        {
            return; // Already at the first line
        }

        int previousLineStart = typedText.LastIndexOf('\n', currentLineStart - 1);
        int previousLineLength = currentLineStart - previousLineStart - 1;

        int column = cursorPosition - currentLineStart - 1;
        cursorPosition = Mathf.Max(previousLineStart + 1 + Mathf.Min(column, previousLineLength), 0);
    }

    void MoveCursorDown()
    {
        int currentLineEnd = typedText.IndexOf('\n', cursorPosition);
        if (currentLineEnd == -1)
        {
            return; // Already at the last line
        }

        int nextLineStart = currentLineEnd + 1;
        int nextLineEnd = typedText.IndexOf('\n', nextLineStart);
        if (nextLineEnd == -1)
        {
            nextLineEnd = typedText.Length;
        }
        int nextLineLength = nextLineEnd - nextLineStart;

        int column = cursorPosition - (typedText.LastIndexOf('\n', cursorPosition - 1) + 1);
        cursorPosition = nextLineStart + Mathf.Min(column, nextLineLength);
    }

    void UpdateDisplayText()
    {
        // Update the TextMeshProUGUI component with the current typed text and cursor
        string textWithCursor = typedText.Insert(cursorPosition, cursorVisible ? cursor : "");
        displayText.text = textWithCursor;
        if (presentedText != null)
        {
            if (WindowsLoginButton.loginactive == 1)
            {
                presentedText.text = new string("Success");
            }
            else
            {

                presentedText.text = new string('*', typedText.Length);
            }
        }
    }

    IEnumerator BlinkCursor()
    {
        while (true)
        {
            // Toggle cursor visibility
            if (someWindow == windowManager.Focused() && PlayerMovement.Freeze && PlayerMovement.chair)
            {
                cursorVisible = !cursorVisible;
            }
            else
            {
                cursorVisible = false;
            }


            // Update display text to reflect cursor visibility
            UpdateDisplayText();

            // Wait for 0.5 seconds
            yield return new WaitForSeconds(0.5f);
        }
    }

    bool IsShiftPressed()
    {
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }

    bool IsCtrlPressed()
    {
        return (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Backspace);
    }
}
