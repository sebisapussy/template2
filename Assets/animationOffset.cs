using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationOffset : MonoBehaviour
{
    public float delay = 0.5f;
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play("baby", 0, delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
