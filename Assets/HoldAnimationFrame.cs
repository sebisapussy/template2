using UnityEngine;
using UnityEngine.Playables;

public class HoldFrameWithPlayableDirector : MonoBehaviour
{
    public PlayableDirector director;
    public Transform targetObject; // The other GameObject to compare distance
    public float distanceThreshold = 5f; // Set the threshold for horizontal distance
    public float yLevelTolerance = 0.5f; // Tolerance for Y-level difference

    private bool isPlayingForward = true;

    void Update()
    {
        // Calculate the horizontal distance between this GameObject and the targetObject (ignoring Y)
        Vector3 horizontalPosition = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 targetHorizontalPosition = new Vector3(targetObject.position.x, 0, targetObject.position.z);
        float horizontalDistance = Vector3.Distance(horizontalPosition, targetHorizontalPosition);

        // Calculate the difference in Y level
        float yDifference = Mathf.Abs(transform.position.y - targetObject.position.y);

        // Determine direction based on distance threshold and Y-level tolerance
        if (horizontalDistance < distanceThreshold && yDifference <= yLevelTolerance)
        {
            isPlayingForward = true;  // Play forward when within both thresholds
        }
        else
        {
            isPlayingForward = false; // Play in reverse when outside either threshold
        }

        // Play or update the animation based on the current direction
        if (director.state != PlayState.Playing)
        {
            director.Play();
        }

        // Update the playback direction and clamp the time (using double precision)
        if (isPlayingForward)
        {
            director.time = System.Math.Clamp(director.time + Time.deltaTime, 0, director.duration); // Play forward and clamp
        }
        else
        {
            director.time = System.Math.Clamp(director.time - Time.deltaTime, 0, director.duration); // Play in reverse and clamp
        }

        // Stop the animation if it reaches the start or end
        if (director.time <= 0 || director.time >= director.duration)
        {
            director.Pause();
        }
    }
}
