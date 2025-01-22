using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CaptchaBUttonTester : MonoBehaviour
{
    public Material tickMaterial;

    public Material noneMaterial;
    public VideoPlayer videoPlayer;

    // Boolean flag to control video playback
    public static bool starter;
    public static bool tick;
    public static bool reset;
    public bool reset2;

    // Reference to the GameObject whose material will change

    // Reference to the custom material to be apsplied

    private void Start()
    {
        // Subscribe to the loopPointReached event to handle video completion
        videoPlayer.loopPointReached += OnVideoFinished;

        // Get the current material of the targetObject (this is the default material)

    }


    public void OnEnable()
    {
        if (tick)
        {

            gameObject.GetComponent<Renderer>().material = tickMaterial;

        }
        else
        {
            gameObject.GetComponent<Renderer>().material = noneMaterial;
            
        }

    }
    private void Update()
    {
        if (starter)
        {
            tick = true;
            starter = false;
            videoPlayer.Play();
        }



        if (reset)
        {
            reset = false;
            ResetAll();
        }

        if (reset2)
        {
            reset2 = false;
            reset = true;
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        vp.Pause();
    }

    private void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
    }

    public void ResetAll()
    {
        videoPlayer.Stop();
        tick = false;
        starter = false;

        // Reset the material back to the default material
        gameObject.GetComponent<Renderer>().material = noneMaterial;
        
    }
}
