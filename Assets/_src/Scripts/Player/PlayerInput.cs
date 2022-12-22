using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float minSwipeSpeed;
    [SerializeField] private float swipeDelay;
    
    private PlayerMovement _movement;
    private float _swipeTimer;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _swipeTimer = 0f;
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

        if (_swipeTimer > 0f)
        {
            _swipeTimer -= Time.deltaTime;
            return;
        }

        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount == 0)
            return;

        var touch = Input.touches[0];
        var swipeSpeed = touch.deltaPosition.magnitude / Time.deltaTime;

        if (swipeSpeed <= minSwipeSpeed)
            return;

        CheckswipeDirection(touch.deltaPosition);
    }

    private void CheckswipeDirection(Vector2 deltaPosition)
    {
        PlayerMoveDirection moveDirection;

        if (Mathf.Abs(deltaPosition.x) > Mathf.Abs(deltaPosition.y))
            moveDirection = deltaPosition.x > 0f ? PlayerMoveDirection.RIGHT : PlayerMoveDirection.LEFT;
        else
            moveDirection = deltaPosition.y > 0f ? PlayerMoveDirection.UP : PlayerMoveDirection.DOWN;

        _movement.CheckMove(moveDirection);

        _swipeTimer = swipeDelay;
    }
}
