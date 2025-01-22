using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiresharkChecker : MonoBehaviour
{

    public GameObject Wireshark;
    public GameObject Deauther;
    public AudioSource audioSource;
    private bool test = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Wireshark.activeSelf == true && PlayerMovement.Freeze && PlayerMovement.chair)
        {
            if (captchaprogressbar.progress == 0 || Deauther.activeSelf == false)
            {
                MaterialFlicker.test = 1;
            }
            else if (test)
            {
                test = false;
                audioSource.Play();
            }
        }
        
    }
}
