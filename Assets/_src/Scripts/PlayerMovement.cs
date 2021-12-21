using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementDuration;
    [SerializeField] private float rotationDuration;
    [SerializeField] private bool enableKeyboardMovement;
    [SerializeField] private Transform obstacleChecker;
    [SerializeField] private float sphereCheckRadius;
    [SerializeField] private LayerMask obstacleLayer;

    private bool _isMoving;

    private void Start()
    {
        obstacleChecker.parent = null;
    }

    private void Update()
    {
        if (enableKeyboardMovement)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                CheckMove("forward");
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                CheckMove("back");
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                CheckMove("right");
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                CheckMove("left");
        }
    }

    private void CheckMove(string direction)
    {
        if (_isMoving)
            return;
        
        Vector3 moveDirection;
        Vector3 rotationDirection;

        switch (direction)
        {
            case "forward":
                moveDirection = Vector3.forward;
                rotationDirection = new Vector3(0, 0, 0);
                break;

            case "back":
                moveDirection = Vector3.back;
                rotationDirection = new Vector3(0, 180, 0);
                break;

            case "right":
                moveDirection = Vector3.right;
                rotationDirection = new Vector3(0, 90, 0);
                break;

            case "left":
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
            return;
        }

        MoveTween(moveDirection, rotationDirection);
    }

    private void MoveTween(Vector3 moveDirection, Vector3 rotationDirection)
    {
        _isMoving = true;

        var adjustToWhole = 0f;
        if (transform.position.x % 1 != 0)
            adjustToWhole = Mathf.Round(transform.position.x) - transform.position.x;

        var finalPosition = transform.position + moveDirection + new Vector3(adjustToWhole, 0, 0);
        
        transform.DOMove(finalPosition, movementDuration).OnComplete(delegate {
            obstacleChecker.position = transform.position;
            _isMoving = false;
        });
    }

    private void OnEnable()
    {
        ControllerLogic.OnMovementMessage += CheckMove;
    }

    private void OnDisable()
    {
        ControllerLogic.OnMovementMessage -= CheckMove;
    }
}