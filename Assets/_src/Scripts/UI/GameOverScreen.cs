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
    [SerializeField] private GameObject gameOverPanelVertical;
    [SerializeField] private GameObject gameOverPanelHorizontal;
    private TextMeshProUGUI _scoreTxt;
    private TextMeshProUGUI _highScoreTxt;

    private void Start()
    {
        if (Screen.height > Screen.width)
        {
            _scoreTxt = gameOverPanelVertical.transform.Find("ScoreNumber").GetComponent<TextMeshProUGUI>();
            _highScoreTxt = gameOverPanelVertical.transform.Find("HighScoreNumber").GetComponent<TextMeshProUGUI>();
        }

        else
        {
            _scoreTxt = gameOverPanelHorizontal.transform.Find("ScoreNumber").GetComponent<TextMeshProUGUI>();
            _highScoreTxt = gameOverPanelHorizontal.transform.Find("HighScoreNumber").GetComponent<TextMeshProUGUI>();
        }

        gameOverPanelVertical.SetActive(false);
        gameOverPanelHorizontal.SetActive(false);
    }

    private void TriggerGameOver()
    {            
        _scoreTxt.text = gameData.CurrentScore.ToString();
        _highScoreTxt.text = gameData.HighScore.ToString();

        if (Screen.height > Screen.width)
            gameOverPanelVertical.SetActive(true);
        else
            gameOverPanelHorizontal.SetActive(true);
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
