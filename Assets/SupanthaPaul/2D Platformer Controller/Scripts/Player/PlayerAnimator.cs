using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace SupanthaPaul
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Rigidbody2D m_rb;
        private PlayerController m_controller;
        private Animator m_anim;
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int JumpState = Animator.StringToHash("JumpState");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        private static readonly int WallGrabbing = Animator.StringToHash("WallGrabbing");
        private static readonly int IsDashing = Animator.StringToHash("IsDashing");
        public static bool attackingval;

        private bool isAttackOnCooldown = false; // Tracks cooldown state

        private void Start()
        {
            m_anim = GetComponentInChildren<Animator>();
            m_controller = GetComponent<PlayerController>();
            m_rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V) && !isAttackOnCooldown)
            {
                StartCoroutine(HandleAttack());
            }

            // Idle & Running animation
            m_anim.SetFloat(Move, Mathf.Abs(m_rb.velocity.x));

            // Jump state (handles transitions to falling/jumping)
            float verticalVelocity = m_rb.velocity.y;
            m_anim.SetFloat(JumpState, verticalVelocity);

            // Jump animation
            if (!m_controller.isGrounded && !m_controller.actuallyWallGrabbing)
            {
                m_anim.SetBool(IsJumping, true);
            }
            else
            {
                m_anim.SetBool(IsJumping, false);
            }

            if (!m_controller.isGrounded && m_controller.actuallyWallGrabbing)
            {
                m_anim.SetBool(WallGrabbing, true);
            }
            else
            {
                m_anim.SetBool(WallGrabbing, false);
            }

            // dash animation
            m_anim.SetBool(IsDashing, m_controller.isDashing);
        }

        private IEnumerator HandleAttack()
        {
            m_anim.SetBool(IsAttacking, true); // Activate attacking
            attackingval = true;
            isAttackOnCooldown = true; // Set cooldown state
            yield return new WaitForSeconds(0.5f); // Attack duration
            m_anim.SetBool(IsAttacking, false);
            yield return new WaitForSeconds(0.5f); // Attack duration
            attackingval = false;
             // Deactivate attacking

            yield return new WaitForSeconds(1f); // Cooldown duration (adjust as needed)
            isAttackOnCooldown = false; // Reset cooldown state
        }
    }
}
