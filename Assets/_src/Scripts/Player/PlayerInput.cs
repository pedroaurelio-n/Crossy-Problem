using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float minSweepSpeed;
    [SerializeField] private float sweepDelay;
    
    private PlayerMovement _movement;
    private float _sweepTimer;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _sweepTimer = 0f;
    }

    private void Update()
    {
        #if UNITY_WEBGL || UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.UpArrow))
            _movement.CheckMove(PlayerMoveDirection.UP);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            _movement.CheckMove(PlayerMoveDirection.DOWN);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            _movement.CheckMove(PlayerMoveDirection.RIGHT);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            _movement.CheckMove(PlayerMoveDirection.LEFT);
        #endif

        if (_sweepTimer > 0f)
        {
            _sweepTimer -= Time.deltaTime;
            return;
        }

        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount == 0)
            return;

        var touch = Input.touches[0];
        var sweepSpeed = touch.deltaPosition.magnitude / touch.deltaTime;

        if (sweepSpeed <= minSweepSpeed)
            return;

        CheckSweepDirection(touch.deltaPosition);
    }

    private void CheckSweepDirection(Vector2 deltaPosition)
    {
        PlayerMoveDirection moveDirection;

        if (Mathf.Abs(deltaPosition.x) > Mathf.Abs(deltaPosition.y))
            moveDirection = deltaPosition.x > 0f ? PlayerMoveDirection.RIGHT : PlayerMoveDirection.LEFT;
        else
            moveDirection = deltaPosition.y > 0f ? PlayerMoveDirection.UP : PlayerMoveDirection.DOWN;

        _movement.CheckMove(moveDirection);
        Debug.Log(deltaPosition);

        _sweepTimer = sweepDelay;
    }
}
