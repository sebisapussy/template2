using UnityEngine;

public class StartAnimationWithOffset : MonoBehaviour
{
    public Animator animator;         // Reference to the Animator component
    float random; // Delay before starting the animation, in seconds
    public float moveSpeed = 3.0f;
    public float moveSpeed2 = 0f;

    private void Start()
    {


        // Invoke the method to start the animation after a delay
        animator = GetComponent<Animator>();
        random = Random.Range(0f,30f);

        animator.Play("npc animation", 0, random);
        //animator.Play("Armature|Walk", 0, random);
        //moveSpeed2 = moveSpeed;
    }

    private void Update()
    {
        //transform.Translate(Vector3.forward * moveSpeed2 * Time.deltaTime);

    }

}
