using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private static MiniGameManager _instance;
    public static MiniGameManager Instance => _instance;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _drone;
    [SerializeField] private GameObject _pressE;

    private bool _isInRange = false;
    private bool _isPlaying = false;
    private int _currentType = 0;
    private GameObject _currentMiniGame;

    private FollowCameraController _followCameraController;
    private EnvironmentManager _environmentManager;
    private GetFruitMiniGame _getFruitMiniGame;

    private void Awake()
    {
        _instance = this;
        _getFruitMiniGame = GetComponent<GetFruitMiniGame>();

        if (_pressE != null) _pressE.SetActive(false);
    }

    private void Start()
    {
        _environmentManager = EnvironmentManager.Instance;
        _followCameraController = FindObjectOfType<FollowCameraController>();
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

        switch (type)
        {
            case 1:
                _currentMiniGame = _environmentManager.EnterGetFruit();
                _getFruitMiniGame?.Init();
                _followCameraController.OnEnterGetFruitMode(_drone.transform);
                break;
            default:
                _environmentManager.EnterHome();
                break;
        }
    }

    private void EndMiniGame()
    {
        if (!_isPlaying) return;

        if (_currentMiniGame != null)
        {
            _currentMiniGame.GetComponent<BaseMiniGame>()?.SendMessage("OnExit");

            Destroy(_currentMiniGame);
        }

        _isPlaying = false;

        _environmentManager.EnterHome();

        var playerController = _player.GetComponent<TopdownController>();
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        Debug.Log("MiniGame 종료");
    }

    public void GameOver()
    {

    }

    public void RestartGame(BaseMiniGame miniGame)
    {
        LoopGetFruitBg loopScene = FindObjectOfType<LoopGetFruitBg>();
        if (loopScene != null)
        {
            loopScene.ResetLoopScene();
        }

        miniGame.Init();
    }
}
