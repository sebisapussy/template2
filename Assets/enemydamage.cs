using SupanthaPaul;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemydamage : MonoBehaviour
{
    public float stopDistance = 10f;  // Range at which the enemy will start chasing the player
    public Transform player;  // Reference to the player


    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;  // Reference to the sprite renderer
    public bool diddymode;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= stopDistance)
        {
          if (PlayerAnimator.attackingval)
             {
                if (diddymode)
                {
                    animator.Play("diddy", -1, 0f);
                }
                else
                { 
                    gameObject.SetActive(false);
                }
            }
        }
    }

}