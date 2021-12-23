using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public delegate void PlayerDeath();
    public static event PlayerDeath OnPlayerDeath;

    [Header("Script References")]
    public PlayerMovement PlayerMovement;

    [Header("Component References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private CapsuleCollider capsuleCollider;

    [Header("Death Config")]
    [SerializeField] private Vector3 deathOffset;

    private bool _isDead = false;

    public void SetAnimationTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    public void TriggerPlayerDeath()
    {
        if (!_isDead)
        {
            _isDead = true;

            transform.parent = null;

            animator.SetTrigger("Death");
            PlayerMovement.SetMovementBool(false);

            capsuleCollider.enabled = false;
            rigidBody.useGravity = false;
            rigidBody.velocity = Vector3.zero;
            transform.position += deathOffset;


            if (OnPlayerDeath != null)
                OnPlayerDeath();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        PlayerMovement.SetMovementBool(true);
    }
}
