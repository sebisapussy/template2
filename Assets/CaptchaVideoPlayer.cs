using UnityEngine;
using UnityEngine.Video;

public class CaptchaVideoPlayer : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Get the VideoPlayer component attached to this GameObject
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer component not found on this GameObject.");
            return;
        }

        // Subscribe to the loopPointReached event
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // This method will be called when the video has finished playing
    private void OnVideoFinished(VideoPlayer vp)
    {
        // Disable the GameObject
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        // Clean up event subscription when the object is destroyed
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}
