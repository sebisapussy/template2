using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class PizzaboyAnimation : MonoBehaviour
{
    public static bool first = false;
    public GameObject object1;
    public GameObject object2;
    private Animator animator1;
    private Animator animator2;


    void OnEnable()
    {
        animator1 = object1.GetComponent<Animator>();
        animator2 = object2.GetComponent<Animator>();
        if (!first)
        {
            first = true;
            animator1.enabled = true;
            animator2.enabled = true;

        }
    }

}
