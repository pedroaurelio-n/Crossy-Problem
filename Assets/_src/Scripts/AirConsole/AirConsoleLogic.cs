using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class AirConsoleLogic : MonoBehaviour
{
    public delegate void MovementMessage(string direction);
    public static event MovementMessage OnMovementMessage;

    public delegate void RestartMessage();
    public static event RestartMessage OnRestartMessage;

    [SerializeField] private GameObject airConsolePanel;

    private void Awake()
    {
        AirConsole.instance.onReady += OnReady;
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onCustomDeviceStateChange += OnCustomDeviceStateChange;
    }

    private void Start()
    {
        if (!AirConsole.instance.IsAirConsoleUnityPluginReady())
        {
            airConsolePanel.SetActive(true);
        }
    }

    private void OnReady(string code)
    {
        JObject newGameState = new JObject();
        newGameState.Add("view", new JObject ());
 
        AirConsole.instance.SetCustomDeviceState(newGameState);

        airConsolePanel.SetActive(false);
    }

    private void OnMessage(int fromDeviceID, JToken data)
    {
        //Debug.Log("message from ID " + fromDeviceID + " : " + data);
        var message = data["action"].ToString();

        if (message == "restart")
        {
            if (OnRestartMessage != null)
                OnRestartMessage();
        }

        else
        {
            if (OnMovementMessage != null)
                OnMovementMessage(message);
        }
    }

    public void SetView(string viewName)
    {
        AirConsole.instance.SetCustomDeviceState(NewView(AirConsole.instance.GetCustomDeviceState(0), viewName));
    }
 
    void OnCustomDeviceStateChange(int from, JToken state)
    {
        Debug.Log("custom device update: " + state);
    }
 
    public static JToken NewView(JToken oldGameState, string sceneName)
    {
        JObject newGameState = oldGameState as JObject;
 
        if (newGameState["view"] != null) 
        { 
            newGameState["view"] = sceneName;
        } else 
        {
            newGameState.Add("view", sceneName);
        }
 
        Debug.Log("returning new game state: " + newGameState);
        return newGameState;
    }

    private void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onReady -= OnReady;
            AirConsole.instance.onMessage -= OnMessage;
            AirConsole.instance.onCustomDeviceStateChange -= OnCustomDeviceStateChange;
        }
    }
}
