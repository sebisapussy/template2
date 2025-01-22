using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Load the scene named "Computer"
            SceneManager.LoadScene("School");
        }
    }
}
