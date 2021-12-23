using UnityEngine;

[CreateAssetMenu(fileName = "HighScoreData", menuName = "High Score Data")]
public class HighScoreData : ScriptableObject
{
    public int HighScore;
    public int CurrentScore;

    public void UpdateScoreValues(int score)
    {
        CurrentScore = score;

        if (CurrentScore > HighScore)
            HighScore = score;
    }
}
