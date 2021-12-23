using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class GameOverScreen : MonoBehaviour
{
    [Header("AirConsoleLogic Reference")]
    [SerializeField] private AirConsoleLogic logic;

    [Header("HighScoreData Reference")]
    [SerializeField] private HighScoreData highScoreData;

    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private TMP_Text highScoreTxt;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    private void TriggerGameOver()
    {
        if (AirConsole.instance.IsAirConsoleUnityPluginReady())
            logic.SetView("GameOver");
        else
            StartCoroutine(ResetLevelCoroutine());
            
        scoreTxt.text = highScoreData.CurrentScore.ToString();
        highScoreTxt.text = highScoreData.HighScore.ToString();

        gameOverPanel.SetActive(true);
    }

    private IEnumerator ResetLevelCoroutine()
    {
        yield return new WaitForSeconds(4f);
        ResetLevel();
    }

    public void ResetLevel()
    {
        if (AirConsole.instance.IsAirConsoleUnityPluginReady())
            logic.SetView("InGame");
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnEnable()
    {
        PlayerMain.OnPlayerDeath += TriggerGameOver;
        AirConsoleLogic.OnRestartMessage += ResetLevel;
    }

    private void OnDisable()
    {
        PlayerMain.OnPlayerDeath -= TriggerGameOver;
        AirConsoleLogic.OnRestartMessage -= ResetLevel;
    }
}
