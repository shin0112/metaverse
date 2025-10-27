using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private static MiniGameManager _instance;
    public static MiniGameManager Instance => _instance;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _pressE;

    [Header("MiniGame Prefabs")]
    [SerializeField] private GameObject _getFruitGamePrefab;

    [Header("Spawn Container")]

    private bool _isInRange = false;
    private bool _isPlaying = false;
    private int _currentType = 0;
    private GameObject _currentMiniGame;

    private FollowCameraController _followCameraController;

    private void Awake()
    {
        _instance = this;
        if (_pressE != null) _pressE.SetActive(false);
    }

    private void Start()
    {
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

    public void SetEnterZone(bool active, int gameType)
    {
        _isInRange = active;
        _currentType = gameType;

        if (_pressE != null)
        {
            _pressE.SetActive(active && !_isPlaying);
        }
    }

    private void StartMiniGame(int type)
    {
        if (_isPlaying) return;

        _isPlaying = true;
        _pressE?.SetActive(false);

        // 미니 게임 추가하기
        Debug.Log($"MiniGame {type} 시작");

        GameObject prefab = type switch
        {
            1 => _getFruitGamePrefab,
            _ => null
        };

        if (prefab == null)
        {
            Debug.LogWarning($"미니게임 {type} 없음");
            return;
        }

        _currentMiniGame = Instantiate(prefab);
        _currentMiniGame.GetComponent<BaseMiniGame>()?.Init();
    }

    private void EndMiniGame()
    {
        if (!_isPlaying) return;

        if (_currentMiniGame != null)
        {
            Destroy(_currentMiniGame);
        }

        _isPlaying = false;
        Debug.Log("MiniGame 종료");
    }
}
