using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("GameData Reference")]
    [SerializeField] private GameData gameData;

    [Header("UI References")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private TextMeshProUGUI highscoreTxt;
    [SerializeField] private GameObject hudObject;

    private void Start()
    {
        mainMenuPanel.SetActive(true);

        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        highscoreTxt.text = gameData.GetHighScore().ToString();
    }
}
