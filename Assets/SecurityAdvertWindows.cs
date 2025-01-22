using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class SecurityAdvertWindows : MonoBehaviour
{

    public static int AdvertChecker = 0;
    public GameObject Advert;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once sper frame
    void Update()
    {
        if (AdvertChecker == 1) { 
            if (PlayerMovement.chair && PlayerMovement.Freeze)
            {
                if (WindowsLoginButton.loginactive == 1)
                {
                    Advert.SetActive(true);
                }
            }
        }
    }
}
