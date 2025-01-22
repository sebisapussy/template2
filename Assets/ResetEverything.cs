using UnityEngine;
using System.Collections.Generic; // Import this to use List<T>

public class ResetAll : MonoBehaviour
{
    public static bool RESET = false;
    public bool RESET2 = false;
    public bool FULL_RESET = false;
    public List<GameObject> desktopApps; // List to hold references to GameObjects
    public GameObject KaliTaskbar;
    private int chairchecker;
    public static int bitcoin = 0;
    public int bitcoin_show;
    private bool performance;


    private void Update()
    {
        bitcoin_show = bitcoin;

        if (performance == true) { 
            if (!PlayerMovement.chair && chairchecker == 1)
            {
                chairchecker = 0;
                RESET2 = true;
            }
            if (PlayerMovement.chair)
            {
                chairchecker = 1;
            }
        }

        if (RESET2 || RESET)
        {
            RESET = false;
            RESET2 = false;
            RestoreAll();
            HideAll();
            TerminalCode.resetTerminal = true;
            WindowsLoginButton.reset = true;

            WindowsTaskbarIconsApp.reset = true;
            WindowsTaskbar.reset = true;

            WindowsStartMenuButton.reset = true;
            WindowManager.reset = true;
            CaptchaBUttonTester.reset = true;
            CaptchaScript.reset = true;
            //KaliTaskbar.SetActive(true);
            SchoolAppClipChanger.value = 0;
            SchoolAppClipChanger2.value = 0;
            SchoolAppClipChanger.hidden = false;
            SchoolUSB.ready_to_take = false;
            transferfilesanimation.value = 0;
            SchoolUsbOtherPC.hacked = false;
            HelpMenu.switcher = false;
        }

        if (FULL_RESET)
        {
            bitcoin = 0;
            DisableDesktopApps();
            RESET2 = true;
        }
    }
public void RestoreAll()
    {
        MoveToCenter[] objects = FindObjectsOfType<MoveToCenter>();
        foreach (var obj in objects)
        {
            if (obj.name != "Cursor")
            {
                obj.RestorePosition();
                print(obj.name);
            }
        }
    }

    public void HideAll()
    {
        MoveToCenter[] objects = FindObjectsOfType<MoveToCenter>();
        foreach (var obj in objects)
        {
            if (obj.name != "Cursor")
            {
                obj.HideObject();
            }
        }
    }

    public void DisableDesktopApps()
    {
        foreach (var app in desktopApps)
        {
            if (app != null)
            {
                app.SetActive(false); // Disable the GameObject
            }
        }
    }
}
