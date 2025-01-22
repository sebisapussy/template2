using SupanthaPaul;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boxxhead : MonoBehaviour
{
    public float stopDistance = 10f;  // Range at which the enemy will start chasing the player
    public Transform player;  // Reference to the player
    public SpriteRenderer spriteRenderer;
    private bool isSpriteVisible = false;
    public float showDuration = 2f;

    void Start()
    {
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
            if (Input.GetKeyDown(KeyCode.V) && PlayerAnimator.attackingval)
            {
                bossbar.change = true;
                if (!isSpriteVisible)
                {
                    StartCoroutine(ShowSpriteForDuration());
                }
            }
        }
    }
    private IEnumerator ShowSpriteForDuration()
    {
        isSpriteVisible = true;
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(showDuration);
        spriteRenderer.enabled = false;
        isSpriteVisible = false;
    }
}