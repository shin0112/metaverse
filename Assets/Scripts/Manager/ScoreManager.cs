using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    private int _fruitScore = 0;
    public int FruitScore => _fruitScore;

    int _totalScore = 0;
    public int TotalScore => _totalScore;

    private const string TotalScoreKey = "TotalScore";

    private void Awake()
    {
        _instance = this;
    }

    public void AddScore(int amount)
    {
        _fruitScore += amount;
        Debug.Log($"현재 점수: {_fruitScore}");
    }

    public void GetScore()
    {
        _totalScore = PlayerPrefs.GetInt(TotalScoreKey, 0);
    }

    public void UpdateTotalScore(int score)
    {
        _totalScore += score;

        PlayerPrefs.SetInt(TotalScoreKey, _totalScore);
    }

    public void UpdateFruitScore(int score)
    {
        _fruitScore += score;
        UpdateTotalScore(score);
    }
}
