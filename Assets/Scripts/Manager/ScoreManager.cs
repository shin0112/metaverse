using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    private int _fruitScore = 0;
    public int FruitScore => _fruitScore;

    private void Awake()
    {
        _instance = this;
    }

    public void AddScore(int amount)
    {
        _fruitScore += amount;
        Debug.Log($"현재 점수: {_fruitScore}");
    }
}
