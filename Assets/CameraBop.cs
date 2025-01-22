using UnityEngine;

public class CameraBob: MonoBehaviour
{
    private Camera playerCamera;
    private Vector3 originalCameraPosition;
    private float bobAmount;
    private float bobSpeed;
    private float bobTimer;
    private bool isBobbing;
    private AudioSource audioSource;
    private CharacterController characterController;
    public CameraBob(Camera camera, float bobAmount, float bobSpeed, AudioSource audioSource)
    {
        this.playerCamera = camera;
        this.bobAmount = bobAmount;
        this.bobSpeed = bobSpeed;
        this.originalCameraPosition = camera.transform.localPosition;
        this.bobTimer = 0;
        this.isBobbing = false;

        // Ensure AudioSource is properly attached to the playerCamera
        this.audioSource = audioSource;
    }


    public void UpdateBobbing()
    {

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && !Input.GetButton("Jump") && !PlayerMovement.Freeze)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // Play the audio only if it's not already playing
            }
            audioSource.pitch = isRunning ? 1.1f : 0.8f;
            StartBobbing();
        }
        else
        {

            PauseBobbing();

        }

    }

    public void PauseBobbing()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause(); // Pause the audio when no movement keys are pressed
        }
        isBobbing = false;
        // No reset; just stop updating bobbing
    }

    private void StartBobbing()
    {
        if (!isBobbing)
        {
            isBobbing = true;
        }

        bobTimer += Time.deltaTime * bobSpeed;
        float bobOffset = Mathf.Sin(bobTimer) * bobAmount;  // Up and down motion using a sine wave
        playerCamera.transform.localPosition = originalCameraPosition + new Vector3(0, bobOffset, 0);
    }

    private void StopBobbing()
    {
        isBobbing = false;
        bobTimer = 0;  // Instantly reset the bobbing timer
        playerCamera.transform.localPosition = originalCameraPosition; // Instantly reset the camera position
    }
}
