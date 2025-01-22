using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTouch : MonoBehaviour
{
    // The name of the scene to load
    public string sceneName;

    // This method is called when a collider enters the trigger area of this object
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that collided with this has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Change to the specified scene
            SceneManager.LoadScene(sceneName);
        }
    }
}
