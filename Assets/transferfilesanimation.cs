using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Video;

public class transferfilesanimation : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign this in the Inspector
    public VideoClip math;  // Assign the first video clip in the Inspector

    public static int value = 0;

    private int currentClipIndex = 0;

    private bool oneattime;

    public GameObject bitcoin;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished; 
        videoPlayer.Stop();
    }

    void OnEnable()
    {
        value = 0;
        videoPlayer.Stop();
        oneattime = false;
        value = 1;
    }


    void Update()
    {
        if (Inventory.usb == 1 || SchoolUsbOtherPC.hacked)
        {
            gameObject.SetActive(false);
            return;
        }

        if (PlayerMovement.chair && !oneattime)
        {
            if (value == 1)
            {
                value = 0;
                oneattime = true;
                videoPlayer.clip = math;
                videoPlayer.Play();
            }
        }
    }
    void OnVideoFinished(VideoPlayer vp)
    {
        value = 0;
        videoPlayer.Stop(); // Switch to the next clip when the current one finishes
        oneattime = false;
        SchoolUSB.ready_to_take = true;
        bitcoin.SetActive(false);
        gameObject.SetActive(false);
    }
}
