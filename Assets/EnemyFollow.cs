using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyChase : MonoBehaviour
{
    public float moveSpeed = 3f;  // Movement speed of the enemy
    public float detectionRange = 10f;  // Range at which the enemy will start chasing the player
    public float stopDistance = 0.5f;  // Distance at which the enemy will stop (touching the player)
    public Transform player;  // Reference to the player

    public bool followX = true;  // Whether the enemy should follow the player on the X-axis
    public bool followY = true;  // Whether the enemy should follow the player on the Y-axis

    public string scenename = "";
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;  // Reference to the sprite renderer

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Get the SpriteRenderer component to flip the sprite
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ensure player is assigned in the inspector or dynamically assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        // Check the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // If the player is within detection range, start chasing
        if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }

        // If the enemy is very close to the player (touching), stop moving
        if (distanceToPlayer <= stopDistance)
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene(scenename);
        }
    }

    void FixedUpdate()
    {
        // Move the enemy using Rigidbody2D physics
        rb.velocity = moveDirection * moveSpeed;
    }

    void ChasePlayer()
    {
        // Get direction towards the player
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Disable movement along certain axes
        if (!followX)
        {
            directionToPlayer.x = 0;
        }
        if (!followY)
        {
            directionToPlayer.y = 0;
        }

        // Set the movement direction
        moveDirection = directionToPlayer;

        // Flip the sprite based on the player's position (only on X-axis)
        if (followX && directionToPlayer.x < 0)  // Player is to the left
        {
            spriteRenderer.flipX = true;
        }
        else if (followX)  // Player is to the right
        {
            spriteRenderer.flipX = false;
        }
    }
}