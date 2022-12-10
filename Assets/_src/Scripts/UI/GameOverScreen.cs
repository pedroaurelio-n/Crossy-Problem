using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    [Header("HighScoreData Reference")]
    [SerializeField] private HighScoreData highScoreData;

    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanelVertical;
    [SerializeField] private GameObject gameOverPanelHorizontal;
    private TMP_Text _scoreTxt;
    private TMP_Text _highScoreTxt;

    private void Start()
    {
        gameOverPanelVertical.SetActive(false);
        gameOverPanelHorizontal.SetActive(false);

        if (Screen.height > Screen.width)
        {
            _scoreTxt = gameOverPanelVertical.transform.Find("ScoreNumber").GetComponent<TMP_Text>();
            _highScoreTxt = gameOverPanelVertical.transform.Find("HighscoreNumber").GetComponent<TMP_Text>();
        }

        else
        {
            _scoreTxt = gameOverPanelHorizontal.transform.Find("ScoreNumber").GetComponent<TMP_Text>();
            _highScoreTxt = gameOverPanelHorizontal.transform.Find("HighscoreNumber").GetComponent<TMP_Text>();
        }
    }

    private void TriggerGameOver()
    {
        StartCoroutine(ResetLevelCoroutine());
            
        _scoreTxt.text = highScoreData.CurrentScore.ToString();
        _highScoreTxt.text = highScoreData.HighScore.ToString();

        if (Screen.height > Screen.width)
            gameOverPanelVertical.SetActive(true);
        else
            gameOverPanelHorizontal.SetActive(true);
    }

    private IEnumerator ResetLevelCoroutine()
    {
        yield return new WaitForSeconds(4f);
        ResetLevel();
    }

    public void ResetLevel()
    {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
