using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PedroAurelio.AudioSystem;

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
    [SerializeField] private PlayAudioEvent deathAudio;

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

            SetAnimationTrigger("Death");
            deathAudio.PlayAudio();

            capsuleCollider.enabled = false;
            rigidBody.useGravity = false;
            rigidBody.velocity = Vector3.zero;
            transform.position += deathOffset;

            PlayerMovement.SetMovementBool(false);

            if (OnPlayerDeath != null)
                OnPlayerDeath();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_isDead)
            PlayerMovement.SetMovementBool(true);
    }
}
