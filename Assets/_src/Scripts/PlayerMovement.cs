using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void Move(string direction)
    {
        switch (direction)
        {
            case "forward":
                transform.position += Vector3.forward;
                break;

            case "back":
                transform.position += -Vector3.forward;
                break;

            case "right":
                transform.position += Vector3.right;
                break;

            case "left":
                transform.position += -Vector3.right;
                break;

            default: 
                Debug.LogError("Unknown direction received."); 
                break;
        }
    }

    private void OnEnable()
    {
        ControllerLogic.OnMovementMessage += Move;
    }

    private void OnDisable()
    {
        ControllerLogic.OnMovementMessage -= Move;
    }
}
