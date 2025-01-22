using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Video;

public class SchoolAppClipChanger2 : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign this in the Inspector
    public VideoClip math;  // Assign the first video clip in the Inspector
    public VideoClip eng;  // Assign the second video clip in the Inspector
    public VideoClip eco;  // Assign the second video clip in the Inspector

    public static int value = 0;

    private int currentClipIndex = 0; // To track which clip is currently playing

    private bool oneattime;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished; // Subscribe to the event
        videoPlayer.Stop();
        
    }

    void OnEnable()
    {
        value = 0;
        videoPlayer.Stop(); // Switch to the next clip when the current one finishes
        oneattime = false;
    }


    void Update()
    {
        if (PlayerMovement.chair && !oneattime)
        {

            if (value == 1)
            {
                value = 0;
                oneattime = true;
                videoPlayer.clip = math;
                videoPlayer.Play();

            }

            if (value == 2)
            {
                value = 0;
                oneattime = true;
                videoPlayer.clip = eng;
                videoPlayer.Play();

            }

            if (value == 3)
            {
                value = 0;
                oneattime = true;
                videoPlayer.clip = eco;
                videoPlayer.Play();

            }
        }
    }
    void OnVideoFinished(VideoPlayer vp)
    {
        value = 0;
        videoPlayer.Stop(); // Switch to the next clip when the current one finishes
        oneattime = false;
    }


}
