using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PlayerMoveDirection
{
    UP,
    RIGHT,
    DOWN,
    LEFT
}

public class PlayerMovement : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] private PlayerMain playerMain;

    [Header("Movement Configs")]
    [SerializeField] private float movementDuration;
    [SerializeField] private float rotationDuration;
    [SerializeField] private Vector3 moveOffset;

    [Header("Obstacle Configs")]
    [SerializeField] private Transform obstacleChecker;
    [SerializeField] private float sphereCheckRadius;
    [SerializeField] private LayerMask obstacleLayer;

    private bool _canMove;
    private bool _isMoving;
    private bool _isOnSupport;

    private void Start()
    {
        _canMove = true;
        obstacleChecker.parent = null;
    }

    private void Update()
    {
        if (_isOnSupport)
            obstacleChecker.position = transform.position;
    }

    public void SetMovementBool(bool value)
    {
        _canMove = value;
    }

    public void CheckMove(PlayerMoveDirection direction)
    {
        if (_isMoving || !_canMove)
            return;
        
        Vector3 moveDirection;
        Vector3 rotationDirection;

        switch (direction)
        {
            case PlayerMoveDirection.UP:
                moveDirection = Vector3.forward;
                rotationDirection = new Vector3(0, 0, 0);
                break;

            case PlayerMoveDirection.DOWN:
                moveDirection = Vector3.back;
                rotationDirection = new Vector3(0, 180, 0);
                break;

            case PlayerMoveDirection.RIGHT:
                moveDirection = Vector3.right;
                rotationDirection = new Vector3(0, 90, 0);
                break;

            case PlayerMoveDirection.LEFT:
                moveDirection = Vector3.left;
                rotationDirection = new Vector3(0, 270, 0);
                break;

            default:
                moveDirection = Vector3.zero;
                rotationDirection = Vector3.zero;
                Debug.LogError("Unknown direction received."); 
                break;
        }

        transform.DORotate(rotationDirection, rotationDuration);

        obstacleChecker.position += moveDirection;

        var isOverlapingObstacle = Physics.OverlapSphere(obstacleChecker.position, sphereCheckRadius, obstacleLayer).Length != 0;

        if (isOverlapingObstacle)
        {
            obstacleChecker.position = transform.position;
            playerMain.SetAnimationTrigger("Fail");
            return;
        }

        _canMove = false;

        MoveTween(moveDirection, rotationDirection);
    }

    private void MoveTween(Vector3 moveDirection, Vector3 rotationDirection)
    {
        _isMoving = true;

        playerMain.SetAnimationTrigger("Hop");

        var adjustToWhole = 0f;

        if (transform.position.x % 1 != 0)
            adjustToWhole = Mathf.Round(transform.position.x) - transform.position.x;
        

        var finalPosition = transform.position + moveOffset + moveDirection + new Vector3(adjustToWhole, 0, 0);

        transform.DOMove(finalPosition, movementDuration).OnComplete(delegate {
            obstacleChecker.position = transform.position;
            _isMoving = false;
        });
    }

    public void SetSupportBool(bool value)
    {
        _isOnSupport = value;
    }
}