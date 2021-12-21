using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class ControllerLogic : MonoBehaviour
{
    public delegate void MovementMessage(string direction);
    public static event MovementMessage OnMovementMessage;

    private void Awake()
    {
        AirConsole.instance.onMessage += PrintMessage;
    }

    private void PrintMessage(int fromDeviceID, JToken data)
    {
        Debug.Log ("message from ID " + fromDeviceID + " : " + data);
        OnMovementMessage(data["action"].ToString());
    }
}
