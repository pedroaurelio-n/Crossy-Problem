using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentScore : MonoBehaviour
{
    [Header("HighScoreData Reference")]
    [SerializeField] private HighScoreData highScoreData;

    [Header("UI References")]
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private TMP_Text highScoreTxt;
    
    private int _currentScore;

    private void Start()
    {
        _currentScore = 0;
        highScoreData.UpdateScoreValues(_currentScore);

        UpdateHud();
    }

    private void AddScore()
    {
        _currentScore++;

        highScoreData.UpdateScoreValues(_currentScore);
        UpdateHud();
    }
    
    private void UpdateHud()
    {
        scoreTxt.text = highScoreData.CurrentScore.ToString();
        highScoreTxt.text = highScoreData.HighScore.ToString();
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
