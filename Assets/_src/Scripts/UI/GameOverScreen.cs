using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [Header("GameData Reference")]
    [SerializeField] private GameData gameData;

    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI highScoreTxt;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    private void TriggerGameOver()
    {            
        scoreTxt.text = gameData.CurrentScore.ToString();
        highScoreTxt.text = gameData.HighScore.ToString();

        gameOverPanel.SetActive(true);
    }

    private void OnEnable()
    {
        PlayerMain.OnPlayerDeath += TriggerGameOver;
    }

    private void OnDisable()
    {
        PlayerMain.OnPlayerDeath -= TriggerGameOver;
    }
}
