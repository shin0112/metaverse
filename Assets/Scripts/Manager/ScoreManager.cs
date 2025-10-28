using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    private int _fruitScore = 0;
    public int FruitScore => _fruitScore;
    private int _bestFruitScore = 0;
    public int BestFruitScore => _bestFruitScore;

    int _totalScore = 0;
    public int TotalScore => _totalScore;

    private const string TotalScoreKey = "TotalScore";
    private const string BestFruitScoreKey = "BestFruitScore";

    private void Awake()
    {
        _instance = this;
    }

    public void AddScore(int amount)
    {
        _fruitScore += amount;
        if (_bestFruitScore < _fruitScore)
        {
            _bestFruitScore = _fruitScore;
        }
        Debug.Log($"현재 점수: {_fruitScore}");
    }

    public void LoadTotalScore()
    {
        _bestFruitScore = PlayerPrefs.GetInt(BestFruitScoreKey, 0);
        _totalScore = PlayerPrefs.GetInt(TotalScoreKey, 0);
    }

    public void CommitRoundScore()
    {
        _totalScore += _fruitScore;
        PlayerPrefs.SetInt(TotalScoreKey, _totalScore);
        PlayerPrefs.SetInt(BestFruitScoreKey, _bestFruitScore);

        Debug.Log($"총 점수 업데이트: {_totalScore} (+{_fruitScore})");

        ResetRoundScore(); // 이번 판 점수 리셋
    }

    public void ResetRoundScore()
    {
        _fruitScore = 0;
    }

    public void UpdateTotalScore(int score)
    {
        _totalScore += score;

        PlayerPrefs.SetInt(TotalScoreKey, _totalScore);
    }

    public void UpdateFruitScore(int score)
    {
        _fruitScore = score;
    }
}
