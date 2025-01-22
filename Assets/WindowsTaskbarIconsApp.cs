using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class WindowsTaskbarIconsApp : MonoBehaviour
{
    public GameObject AppRun;
    public string text;
    public static Material storedMaterial;
    public static string joe;
    private static bool test =false;
    public static bool reset = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        joe = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            reset = false;
           // gameObject.SetActive(false);
            joe = "";
        }
    }

    public static Material GetMaterialFoccused()
    {
        GameObject joel = WindowManager.isFocused();
        if (joel != null)
        {
            WindowsTaskbarIconsApp[] allIconsApps = FindObjectsOfType<WindowsTaskbarIconsApp>();
            foreach (WindowsTaskbarIconsApp iconsApp in allIconsApps)
            {
                if (iconsApp.AppRun == joel)
                {

                    storedMaterial = null;
                    MaterialCopyScript.rem = iconsApp.text;
                    return iconsApp.GetComponent<Renderer>().material; 
                }

                
            }

        }
        return storedMaterial;
    }


}
