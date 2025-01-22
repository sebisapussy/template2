using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    // Map key combinations to scene names
    [System.Serializable]
    public class KeyCombination
    {
        public KeyCode modifier1;
        public KeyCode modifier2;
        public KeyCode modifier3;
        public KeyCode triggerKey;
        public string sceneName;
    }

    public KeyCombination[] keyCombinations;

    void Update()
    {
        foreach (var combination in keyCombinations)
        {
            if (Input.GetKey(combination.modifier1) && Input.GetKey(combination.modifier2) && Input.GetKey(combination.modifier3) && Input.GetKeyDown(combination.triggerKey))
            {
                TeleportToScene(combination.sceneName);
                break;
            }
        }
    }

    private void TeleportToScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log($"Teleporting to scene: {sceneName}");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is empty or null!");
        }
    }
}
