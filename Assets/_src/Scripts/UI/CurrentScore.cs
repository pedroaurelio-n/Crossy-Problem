using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentScore : MonoBehaviour
{
    [Header("GameData Reference")]
    [SerializeField] private GameData gameData;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI highScoreTxt;
    
    private int _currentScore;

    private void Start()
    {
        _currentScore = 0;
        gameData.UpdateScoreValues(_currentScore);

        UpdateHud();
    }

    private void AddScore()
    {
        _currentScore++;

        gameData.UpdateScoreValues(_currentScore);
        UpdateHud();
    }
    
    private void UpdateHud()
    {
        scoreTxt.text = gameData.CurrentScore.ToString();
        highScoreTxt.text = gameData.GetHighScore().ToString();
    }

    private void OnEnable()
    {
        TriggerScore.OnScoreAdded += AddScore;
    }

    private void OnDisable()
    {
        TriggerScore.OnScoreAdded -= AddScore;
    }
}
