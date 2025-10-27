using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private static MiniGameManager _instance;
    public static MiniGameManager Instance => _instance;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _pressE;

    private bool _isInRange = false;
    private bool _isPlaying = false;
    private int _currentType = 0;
    private GameObject _currentMiniGame;

    private FollowCameraController _followCameraController;
    private EnvironmentManager _environmentManager;

    private void Awake()
    {
        _instance = this;
        if (_pressE != null) _pressE.SetActive(false);
    }

    private void Start()
    {
        _environmentManager = EnvironmentManager.Instance;
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

        // �̴� ���� �߰��ϱ�
        Debug.Log($"MiniGame {type} ����");

        switch (type)
        {
            case 1:
                _environmentManager.EnterGetFruit();
                break;
            default:
                _environmentManager.EnterHome();
                break;
        }

        GameObject prefab = type switch
        {
            1 => Resources.Load<GameObject>("MiniGames/GetFruitMiniGame"),
            _ => null
        };

        _currentMiniGame = Instantiate(prefab);
        _currentMiniGame.GetComponent<BaseMiniGame>()?.Init();
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

        Debug.Log("MiniGame ����");
    }
}
