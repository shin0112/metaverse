using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private static MiniGameManager _instance;
    public static MiniGameManager Instance => _instance;

    [Header("MiniGame Prefabs")]

    [Header("Spawn Container")]

    [Header("UI")]
    [SerializeField] private GameObject _pressE;

    private bool _isInRange = false;
    private bool _isPlaying = false;
    private int _currentType = 0;
    private GameObject _currentMiniGame;

    private void Awake()
    {
        _instance = this;
        if (_pressE != null) _pressE.SetActive(false);
    }

    private void Update()
    {
        if (_isInRange && !_isPlaying && Input.GetKeyDown(KeyCode.E))
        {
            StartMiniGame(_currentType);
        }

        if (_isPlaying && Input.GetKeyDown(KeyCode.Escape))
        {
            EndMiniGame();
        }
    }

    private void StartMiniGame(int type)
    {
        if (_isPlaying) return;

        _isPlaying = true;
        _pressE?.SetActive(false);

        // �̴� ���� �߰��ϱ�
        Debug.Log($"MiniGame {type} ����");
    }

    private void EndMiniGame()
    {
        if (!_isPlaying) return;

        if (_currentMiniGame != null)
        {
            Destroy(_currentMiniGame);
        }

        _isPlaying = false;
        Debug.Log("MiniGame ����");
    }
}
