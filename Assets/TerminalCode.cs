using UnityEngine;
using TMPro;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System;

public class TerminalCode : MonoBehaviour
{
    // Reference to the TextMeshProUGUI component where typed text will be displayed
    public TMP_Text displayText;

    string message = "";
    private string typedText = "";

    // Three separate prefixes that will appear on new lines
    public string prefix1 = "Prefix 1";
    public string prefix2 = "Prefix 2";
    public string prefix3 = "Prefix 3";

    [TextArea]  public string bitcoindscreen = "Prefix 3";
    private string bitcoindscreendisplay = "";

    // Maximum number of lines allowed before deleting the top line
    public int maxLines = 5;

    // Index to track the position of the cursor within the text
    private int cursorPosition = 0;

    public WindowManager windowManager;
    public GameObject someWindow;

    private int progress = 0;
    private float minUpdateInterval = 0.00f;
    private float maxUpdateInterval = 0.1f;
    private bool isTimerActive = false;
    private bool display = true;

    // APP PERMS
    public bool bitcoinperms = false;
    public bool notepadperms = false;
    public bool wiresharkperms = false;
    public bool passwordcrackerperms = false;
    public bool teamsperms = false;
    public bool securityperms = false;
    public bool deautherperms = false;

    // APP DESKTOP
    public GameObject WiresharkDesktop;
    public GameObject DeAutherDesktop;
    public GameObject NotepadDesktop;
    public GameObject SecurityDesktop;
    public GameObject TeamsDesktop;


    private bool passcrackcommand = false;
    private bool helpcommand = false;
    private bool bitcoinscreencommand = false;

    private string asciiArt = "";
    private string as2ciiArt2 = @"
.___                 __         .__  .__                
|   | ____   _______/  |______  |  | |  |   ___________ 
|   |/    \ /  ___/\   __\__  \ |  | |  | _/ __ \_  __ \
|   |   |  \\___ \  |  |  / __ \|  |_|  |_\  ___/|  | \/
|___|___|  /____  > |__| (____  /____/____/\___  >__|   
         \/     \/            \/               \/       
";

string infoString = @"<color=#FFB600>-->------[Prices]------<--
 Bitcoin			<color=#FFB600><color=#FFFFFF>[</color>0 BTC<color=#FFFFFF>]</color></color>
 Notepad			<color=#FFB600><color=#FFFFFF>[</color>2 BTC<color=#FFFFFF>]</color></color>
 Teams			<color=#FFB600><color=#FFFFFF>[</color>5 BTC<color=#FFFFFF>]</color></color>
 Security Plus	<color=#FFB600>	<color=#FFFFFF>[</color>20 BTC<color=#FFFFFF>]</color> </color>
 Wireshark	<color=#FFB600>	<color=#FFFFFF>[</color>100 BTC<color=#FFFFFF>]</color></color>
 Deauther	<color=#FFB600>	<color=#FFFFFF>[</color>550 BTC<color=#FFFFFF>]</color></color>
 Password Cracker	<color=#FFB600>	<color=#FFFFFF>[</color>800 BTC<color=#FFFFFF>]</color> </color>
<color=#CECECE>-->------[<color=#257FFB>Commands</color>]------<--
'<color=#C1C1C1>bitcoin</color>'<color=#D3D3D3>		Opens the <color=#257FFB>bitcoin</color> application</color>
'<color=#999999>notepad</color>'<color=#D3D3D3>		Opens the <color=#257FFB>notepad</color> application</color>
'<color=#C1C1C1>teams</color>'<color=#D3D3D3>			Opens the <color=#257FFB>teams</color> application</color>
'<color=#999999>securityplus</color>'<color=#D3D3D3>		Opens the <color=#257FFB>security camera</color> application</color>
'<color=#C1C1C1>wireshark</color>'<color=#D3D3D3>		Opens the <color=#257FFB>wireshark</color> application</color>
'<color=#999999>deauther</color>'<color=#D3D3D3>		Opens the <color=#257FFB>deauther</color> application</color>
'<color=#C1C1C1>passwordcracker</color>'<color=#D3D3D3>		Opens the <color=#257FFB>passwordcracker</color> application</color>

'<color=#999999>install</color> <color=#C1C1C1>bitcoin</color>'<color=#D3D3D3>		Installs the <color=#257FFB>bitcoin</color> application</color>
'<color=#999999>install</color> <color=#C1C1C1>notepad</color>'<color=#D3D3D3>		Installs the <color=#257FFB>notepad</color> application</color>
'<color=#999999>install</color> <color=#C1C1C1>teams</color>'<color=#D3D3D3>		Installs the <color=#257FFB>teams</color> application</color>
'<color=#999999>install</color> <color=#C1C1C1>securityplus</color>'<color=#D3D3D3>	Installs the <color=#257FFB>security camera</color> application</color>
'<color=#999999>install</color> <color=#C1C1C1>wireshark</color>'<color=#D3D3D3>	Installs the <color=#257FFB>wireshark</color> application</color>
'<color=#999999>install</color> <color=#C1C1C1>deauther</color>'<color=#D3D3D3>	Installs the <color=#257FFB>deauther</color> application</color>
'<color=#999999>install</color> <color=#C1C1C1>passwordcracker</color>'<color=#D3D3D3>	Installs the <color=#257FFB>password cracker</color> application</color>
";

    string infoString2 = @"
<color=#A5A5A5>Using default encoding: UTF-8\n
Loaded 3 passwords hashes with no different salts (NT [MD4 128/128 AVX 4x3])\n
Remaining 1 password hash\n<color=#5AB9AB>
Press 'q' or Ctrl-C to abort, almost any other key for status\n
<color=#D3D3D3>School Password:	'teacheracc'</color>
1g 0:00:00:00 DONE (2024-12-2 12:36) 50.00g/s 4420Kp/s 4420Kc/s \n
Warning: passwords printed above might not be all those cracked\n<color=#A5A5A5>
Use the ""--show"" option to display all of the cracked passwords reliably\n
Session completed\n
";

    // Boolean to reset the terminal
    public static bool resetTerminal = false;

    // Store the initial state of the terminal
    private string initialTypedText;
    private int initialCursorPosition;
    private bool initialBitcoin;
    private bool initialNotepadPerms;
    private bool initialDeAutherperms;
    private bool initialWiresharkPerms;
    private bool initialPasswordcrackerperms;
    private bool initialTeamperms;
    private bool initialSecurityPerms;

    void Start()
    {
        // Initialize cursor position to the end of the initial prefixes
        cursorPosition = (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length;

        // Store the initial state
        initialTypedText = typedText;
        initialCursorPosition = cursorPosition;
        initialNotepadPerms = notepadperms;
        initialDeAutherperms = deautherperms;
        initialWiresharkPerms = wiresharkperms;
        initialBitcoin = bitcoinperms;
        initialPasswordcrackerperms = passwordcrackerperms;
        initialTeamperms = teamsperms;
        initialSecurityPerms = securityperms;
    }

    void OnEnable()
    {
        cursorPosition = (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length;
        typedText = "";
        bitcoinscreencommand = false;
        helpcommand = false;
        passcrackcommand = false;
        progress = 0;
        asciiArt = "";
        isTimerActive = false;
        display = true;
        displayText.text = "";
    }

    void Update()
    {
        if (someWindow == windowManager.Focused() && PlayerMovement.Freeze && PlayerMovement.chair)
        {
            HandleInput();
        }

        if (PlayerMovement.Freeze && PlayerMovement.chair)
        {
            UpdateDisplayText();
        }

        // Check if resetTerminal is enabled
        if (resetTerminal)
        {
            ResetTerminal();
            resetTerminal = false; // Disable the boolean after resetting
        }
    }

    // Reset the terminal to its initial state
    void ResetTerminal()
    {
        typedText = initialTypedText;
        cursorPosition = initialCursorPosition;
        notepadperms = initialNotepadPerms;
        deautherperms = initialDeAutherperms;
        wiresharkperms = initialWiresharkPerms;
        passwordcrackerperms = initialPasswordcrackerperms;
        teamsperms = initialTeamperms;
        bitcoinperms = initialBitcoin;
        securityperms = initialSecurityPerms;
        bitcoinscreencommand = false;
        helpcommand = false;
        passcrackcommand = false;
        progress = 0;
        asciiArt = "";
        isTimerActive = false;
        display = true;
        displayText.text = "";
        UpdateDisplayText(); // Redraw the text after resetting
    }

    void NewLine()
    {
        // Handle newline characters and insert all three prefixes on the new line
        string newLinePrefixes = "\n" + prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ";
        typedText = typedText.Insert(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length, newLinePrefixes);
        cursorPosition += newLinePrefixes.Length; // Account for the new line and all prefixes

        ManageLineLimit();
    }

    void HandleInput()
    {
        if (!display)
        {
            return;
        }
        foreach (char c in Input.inputString)
        {
            if (c == '\b' && 1 == 2)
            {
                // Handle backspace, but don't allow removing the initial prefixes
                if (cursorPosition > (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length && typedText.Length > 0)
                {
                    typedText = typedText.Remove(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length - 1, 1);
                    cursorPosition--;
                }
            }

            if (c == '\b')
            {
                return;
            }
            else if (c == '\n' || c == '\r')
            {
                if (helpcommand == true)
                {
                    helpcommand = false;
                    NewLine();
                    return;
                }
                if (passcrackcommand == true)
                {
                    passcrackcommand = false;
                    NewLine();
                    return;
                }
                if (bitcoinscreencommand == true)
                {
                    bitcoinscreencommand = false;
                    NewLine();
                    return;
                }
                ManageLineLimit();
                message = "";


                int lastIndex = typedText.LastIndexOf('\n');
                string lastLine = lastIndex >= 0
                    ? typedText.Substring(lastIndex + 1)
                    : typedText;


                string cleanedString = RemoveRichTextTags(lastLine);

                bool installbitcoin = (cleanedString == "install bitcoin");
                bool installwireshark = (cleanedString == "install wireshark");
                bool installnotepad = (cleanedString == "install notepad");
                bool installdeauther = (cleanedString == "install deauther");
                bool installsecurityplus = (cleanedString == "install securityplus");
                bool installpasswordcracker = (cleanedString == "install passwordcracker");
                bool installteams = (cleanedString == "install teams");

                bool help = (cleanedString == "help");
                bool empty = (cleanedString == "");
                bool sammy = (cleanedString == "sammy");
                bool bitcoin = (cleanedString == "bitcoin");
                bool passwordcracker = (cleanedString == "passwordcracker");
                bool teamstext = (cleanedString == "teams");

                bool wiresharktext = (cleanedString == "wireshark");
                bool notepadtext = (cleanedString == "notepad");
                bool deauthertext = (cleanedString == "deauther");
                bool securityplustext = (cleanedString == "securityplus");

                print(lastLine);

                print(cleanedString);

                if (installwireshark)
                {
                    if (ResetAll.bitcoin < 100)
                    {
                        int joe = 100 - ResetAll.bitcoin;
                        message = "\t<mark=#AD000050><color=#FFFFFF> You need " + joe + " more bitcoin to purchase</color></mark>";
                        typedText = typedText.Insert(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length, message);
                        cursorPosition += message.Length;
                        empty = true;
                    }
                    else
                    {
                        isTimerActive = true;
                        asciiArt = as2ciiArt2;
                        wiresharkperms = true;
                        StartCoroutine(UpdateProgress());
                        //ADD DESKTOP ICON 
                        ResetAll.bitcoin = ResetAll.bitcoin - 100;
                        return;
                    }

                }



                if (installnotepad)
                {
                    if (ResetAll.bitcoin < 2)
                    {
                        int joe = 2 - ResetAll.bitcoin;
                        message = "\t<mark=#AD000050><color=#FFFFFF> You need " + joe + " more bitcoin to purchase</color></mark>";
                        typedText = typedText.Insert(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length, message);
                        cursorPosition += message.Length;
                        empty = true;
                    }
                    else {
                        isTimerActive = true;
                        asciiArt = as2ciiArt2;
                        notepadperms = true;
                        StartCoroutine(UpdateProgress());
                        //ADD DESKTOP ICON 
                        ResetAll.bitcoin = ResetAll.bitcoin - 2;
                        return;
                    }

                }

                if (installdeauther)
                {
                    if (ResetAll.bitcoin < 550)
                    {
                        int joe = 550 - ResetAll.bitcoin;
                        message = "\t<mark=#AD000050><color=#FFFFFF> You need " + joe + " more bitcoin to purchase</color></mark>";
                        typedText = typedText.Insert(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length, message);
                        cursorPosition += message.Length;
                        empty = true;
                    }
                    else
                    {
                        isTimerActive = true;
                        asciiArt = as2ciiArt2;
                        deautherperms = true;
                        StartCoroutine(UpdateProgress());
                        //ADD DESKTOP ICON 
                        ResetAll.bitcoin = ResetAll.bitcoin - 550;
                        return;
                    }

                }

                if (installsecurityplus) 
                {
                    if (ResetAll.bitcoin < 20)
                    {
                        int joe = 20 - ResetAll.bitcoin;
                        message = "\t<mark=#AD000050><color=#FFFFFF> You need " + joe + " more bitcoin to purchase</color></mark>";
                        typedText = typedText.Insert(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length, message);
                        cursorPosition += message.Length;
                        empty = true;
                    }
                    else
                    {
                        isTimerActive = true;
                        asciiArt = as2ciiArt2;
                        securityperms = true;
                        StartCoroutine(UpdateProgress());
                        //ADD DESKTOP ICON 
                        ResetAll.bitcoin = ResetAll.bitcoin - 20;
                        return;
                    }
                }


                if (installpasswordcracker)
                {
                    if (ResetAll.bitcoin < 800)
                    {
                        int joe = 800 - ResetAll.bitcoin;
                        message = "\t<mark=#AD000050><color=#FFFFFF> You need " + joe + " more bitcoin to purchase</color></mark>";
                        typedText = typedText.Insert(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length, message);
                        cursorPosition += message.Length;
                        empty = true;
                    }
                    else
                    {
                        isTimerActive = true;
                        asciiArt = as2ciiArt2;
                        passwordcrackerperms = true;
                        StartCoroutine(UpdateProgress());
                        //ADD DESKTOP ICON 
                        ResetAll.bitcoin = ResetAll.bitcoin - 800;
                        return;
                    }
                }

                if (installteams)
                {

                    if (ResetAll.bitcoin < 5)
                    {
                        int joe = 5 - ResetAll.bitcoin;
                        message = "\t<mark=#AD000050><color=#FFFFFF> You need " + joe + " more bitcoin to purchase</color></mark>";
                        typedText = typedText.Insert(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length, message);
                        cursorPosition += message.Length;
                        empty = true;
                    }
                    else
                    {
                        isTimerActive = true;
                        asciiArt = as2ciiArt2;
                        teamsperms = true;
                        StartCoroutine(UpdateProgress());
                        //ADD DESKTOP ICON 
                        ResetAll.bitcoin = ResetAll.bitcoin - 5;
                        return;
                    }
                }

                if (installbitcoin)
                {

                    isTimerActive = true;
                    asciiArt = as2ciiArt2;
                    bitcoinperms = true;
                    StartCoroutine(UpdateProgress());
                    //ADD DESKTOP ICON 
                    return;

                }
                else if(help)
                {
                    helpcommand = true;
                    return;

                }


                else if (passwordcracker)
                {
                    if (passwordcrackerperms)
                    {
                        passcrackcommand = true;
                        return;
                    }
                    else
                    {
                        message = "\t<mark=#AD000050><color=#FFFFFF> No Permission</color></mark>";

                        // Insert the message before moving to a new line with the prefixes
                        typedText = typedText.Insert(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length, message);
                        cursorPosition += message.Length;
                    }
                }



                else if (bitcoin)
                {
                    if (bitcoinperms)
                    {
                        bitcoinscreencommand = true;
                        bitcoindscreendisplay = "";
                        StartCoroutine(BitCoinMiner());
                        return;
                    }
                    else
                    {
                        message = "\t<mark=#AD000050><color=#FFFFFF> No Permission</color></mark>";

                        // Insert the message before moving to a new line with the prefixes
                        typedText = typedText.Insert(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length, message);
                        cursorPosition += message.Length;
                    }
                }


                else if (sammy)
                {
                    SchoolAppClipChanger.hidden = true;

                }

                else if (wiresharktext)
                {
                    if (wiresharkperms)
                    {
                        WindowOpenDesktop.namer = WiresharkDesktop;
                    }
                    else
                    {
                        message = "\t<mark=#AD000050><color=#FFFFFF> No Permission</color></mark>";

                        // Insert the message before moving to a new line with the prefixes
                        typedText = typedText.Insert(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length, message);
                        cursorPosition += message.Length;
                    }
                }

                else if (teamstext)
                {
                    if (teamsperms)
                    {
                        WindowOpenDesktop.namer = TeamsDesktop;
                    }
                    else
                    {
                        message = "\t<mark=#AD000050><color=#FFFFFF> No Permission</color></mark>";

                        // Insert the message before moving to a new line with the prefixes
                        typedText = typedText.Insert(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length, message);
                        cursorPosition += message.Length;
                    }
                }



                else if (notepadtext)
                {
                    if (notepadperms)
                    {
                        WindowOpenDesktop.namer = NotepadDesktop;
                    }
                    else
                    {
                        message = "\t<mark=#AD000050><color=#FFFFFF> No Permission</color></mark>";

                        // Insert the message before moving to a new line with the prefixes
                        typedText = typedText.Insert(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length, message);
                        cursorPosition += message.Length;
                    }
                }

                else if (deauthertext)
                {
                    if (deautherperms)
                    {
                        WindowOpenDesktop.namer = DeAutherDesktop;
                    }
                    else
                    {
                        message = "\t<mark=#AD000050><color=#FFFFFF> No Permission</color></mark>";

                        // Insert the message before moving to a new line with the prefixes
                        typedText = typedText.Insert(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length, message);
                        cursorPosition += message.Length;
                    }
                }

                else if (securityplustext)
                {

                    if (securityperms)
                    {
                        WindowOpenDesktop.namer = SecurityDesktop;
                    }
                    else
                    {
                        message = "\t<mark=#AD000050><color=#FFFFFF> No Permission</color></mark>";

                        // Insert the message before moving to a new line with the prefixes
                        typedText = typedText.Insert(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length, message);
                        cursorPosition += message.Length;
                    }
                }


                else if (!empty)
                {
                    // Append the unknown command message immediately after the user's input
                    message = "\t<mark=#AD000050><color=#FFFFFF>Unknown Command</color></mark>";

                    // Insert the message before moving to a new line with the prefixes
                    typedText = typedText.Insert(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length, message);
                    cursorPosition += message.Length;
                }

                // Handle newline characters and insert all three prefixes on the new line after the message
                string newLinePrefixes = "\n" + prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ";
                typedText = typedText.Insert(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length, newLinePrefixes);
                cursorPosition += newLinePrefixes.Length; // Account for the new line and all prefixes


                // Check if the number of lines exceeds the limit
            }
            else if (IsCtrlPressed())
            {
                // Handle Ctrl + Backspace to remove the entire word (except the prefixes)
                if (cursorPosition > (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length && typedText.Length > 0)
                {
                    int endIndex = cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length - 1;
                    while (endIndex > 0 && typedText[endIndex] != ' ' && typedText[endIndex] != '\n')
                    {
                        endIndex--;
                    }
                    typedText = typedText.Remove(endIndex, cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length - endIndex);
                    cursorPosition = endIndex + (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length;
                }
            }
            else
            {
                // Insert the typed character
                typedText = typedText.Insert(cursorPosition - (prefix1 + "\n" + prefix2 + "\n" + prefix3 + " ").Length, c.ToString());
                cursorPosition++;
            }
        }
    }
    
    private string RemoveRichTextTags(string input)
    {
        // Regular expression to remove HTML-like tags
        string pattern = "<[^>]+>";
        Regex regex = new Regex(pattern);
        string result = regex.Replace(input, "").Trim();

        // Optionally, you can remove any leading special characters if needed
        result = result.TrimStart('└', '─', ' ', '<',  '>', '$').Trim();

        return result;
    }




    void UpdateDisplayText()
    {
        string newtext = typedText;


        string startertext = " install \n";
        string progressBar = new string('#', Mathf.RoundToInt(progress / 2f));
        string progressText = $"<color=#fff>Downloading the required packages</color>\n\n<mark=#67FE0441><color=#fff>Progress: [{progress}%]</mark> [{progressBar.PadRight(50)}]";
        if (asciiArt == "")
        {
            progressText = "";
            progressBar = "";
            startertext = "";
            maxLines = 7;
        }

        else
        {
            newtext = "";
            display = false;
            maxLines = 3;

            if (progress >= 99)
            {
                isTimerActive = false;
                display = true;
                progress = 0;
                progressText = "";
                progressBar = "";
                startertext = "";
                asciiArt = "";
                maxLines = 7;
                NewLine();
                //APPS
                if (wiresharkperms) {WiresharkDesktop.SetActive(true); }
                if (notepadperms) { NotepadDesktop.SetActive(true); }
                if (securityperms) { SecurityDesktop.SetActive(true); }
                if (teamsperms) { TeamsDesktop.SetActive(true); }
                if (deautherperms) { DeAutherDesktop.SetActive(true); }
            }
}
        if (helpcommand)
        {
            displayText.text = prefix2 + "\n" + prefix3 + " help \n" + infoString;
            return;
        }

        if (passcrackcommand)
        {
            displayText.text = prefix2 + "\n" + prefix3 + " passwordcracker \n" + infoString2;
            return;
        }



        if (bitcoinscreencommand)
        {
            displayText.text = bitcoindscreendisplay;
            return;
        }

        if (helpcommand && 1 == 2)
        {

        }
        else
        {
            displayText.text = prefix2 + "\n" + prefix3 + startertext + newtext + asciiArt + progressText;
            return;
        }

    }


    IEnumerator UpdateProgress()
    {
        while (isTimerActive)
        {
            // Increment the progress
            progress = (progress + 1) % 101; // Ensure progress stays between 0 and 100


            // Update the display text
            UpdateDisplayText();
            float randomInterval = UnityEngine.Random.Range(minUpdateInterval, maxUpdateInterval);


            // Wait for the specified interval
            yield return new WaitForSeconds(randomInterval);
        }
    }


    IEnumerator BitCoinMiner()
    {
        while (bitcoinscreencommand == true && PlayerMovement.Freeze && PlayerMovement.chair) // Infinite loop
        {
            // Split the input text by new lines
            string[] lines = bitcoindscreen.Split('\n');
            bitcoindscreendisplay = "<b>-----------[Bitcoin Amount]---------------------        " + ResetAll.bitcoin + " BTC</b>";
            foreach (string line in lines)
            {

                // Append the current line to the output text
                bitcoindscreendisplay = bitcoindscreendisplay + line + "\n";

                // Optionally, do something with the outputText, like logging it

                // Wait for the specified delay before adding the next line
                yield return new WaitForSeconds(0.1f);

                // Clear the output text after the delay to simulate line-by-line
                
            }
            if (bitcoinscreencommand)
            {
                ResetAll.bitcoin++;
            }
            
        }
    }



    // Method to remove the oldest line if the max number of lines is exceeded
    void ManageLineLimit()

    {

        string[] lines = typedText.Split('\n');

        int totalLines = lines.Length / 3; // Since each block of text has Prefix1 + Prefix2 + Prefix3 + message



        // If the number of lines exceeds maxLines, remove the oldest block (Prefix1 + Prefix2 + Prefix3 + message)

        if (totalLines > maxLines)

        {

            // Remove the ASCII art, progress bar, and progress text

            asciiArt = "";

            progress = 0; // Reset progress to remove the progress bar

            displayText.text = ""; // Clear progress text



            // Find the index of the first line's end

            int firstLineEndIndex = typedText.IndexOf("\n" + prefix3) + ("\n" + prefix3).Length;

            typedText = typedText.Remove(0, firstLineEndIndex);

            cursorPosition -= firstLineEndIndex; // Adjust cursor position accordingly

        }

    }




    bool IsCtrlPressed()
    {
        return (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Backspace);
    }




}
