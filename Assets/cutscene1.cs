using UnityEngine;

public class cutscene1 : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public string targetStateName; // Name of the animation state to check
    public GameObject objectToEnable; // GameObject to enable

    void Update()
    {
        // Get the current state information from the Animator
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

        // Check if the current state's name matches the target state
        if (currentState.IsName(targetStateName))
        {
            objectToEnable.SetActive(true);
        }

    }
}
