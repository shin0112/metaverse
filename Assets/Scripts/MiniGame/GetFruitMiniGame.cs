using System.Collections;
using UnityEngine;

public class GetFruitMiniGame : BaseMiniGame
{
    [Header("References")]
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _drone;
    [SerializeField] private Camera _camera;

    [Header("Settings")]
    [SerializeField] private float _cameraTargetSize = 5f;
    [SerializeField] private float _cameraShiftX = 2f;
    [SerializeField] private float _flapPower = 5f;

    private Vector3 _originalCamPos;
    private float _originalCamSize;
    private TopdownController _playerController;
    private ScrollController _droneController;

    private bool _isFlap = false;
    private float _deathCooldown = 0f;
    private bool _isFirstEnter = true;
    private int _score = 0;

    private MiniGameManager _miniGameManager;
    private ScoreManager _scoreManager;

    public override void Init()
    {
        Debug.Log($"{this.name} Init ȣ��");

        CurrentState = MiniGameState.Preparing;

        _miniGameManager = MiniGameManager.Instance;
        _scoreManager = ScoreManager.Instance;

        if (_camera != null)
        {
            _camera.orthographicSize = _originalCamSize;
            _camera.transform.position = _originalCamPos;
        }

        _droneController.ClearDeathEvent();
        _droneController.OnDroneDeath += () =>
        {
            OnStop();
            Debug.Log("��� �ı�");
        };

        _score = 0;
        OnReady();
    }

    protected override void Awake()
    {
        base.Awake();

        _playerController = _player.GetComponent<TopdownController>();
        _droneController = _drone.GetComponent<ScrollController>();
        _controller = _droneController;

        if (_camera == null)
        {
            _camera = Camera.main;
        }

        _originalCamPos = _camera.transform.position;
        _originalCamSize = _camera.orthographicSize;
    }

    protected override void OnExit()
    {
        _droneController.ResetDronePhysics();
        _droneController.ResetState();
    }

    protected override void OnReady()
    {
        Debug.Log("GetFruit MiniGame �غ� ��...");

        if (_isFirstEnter)
        {
            StartCoroutine(PlayEnterSequence());
            _isFirstEnter = false;
        }
        else
        {
            _droneController.ResetDronePhysics();
            _droneController.ResetState();

            Debug.Log("�ٷ� Ready �Ϸ�");
        }

        CurrentState = MiniGameState.Ready;
        Debug.Log("GetFruit MiniGame �غ� �Ϸ�");
    }

    protected override void OnStop()
    {
        CurrentState = MiniGameState.Stop;

        // ui�� ���� update
        ScoreManager.Instance.CommitRoundScore();
        UIManager.Instance.UpdateScore();

        _deathCooldown = 1f;
    }

    private IEnumerator PlayEnterSequence()
    {
        CurrentState = MiniGameState.Preparing;

        Debug.Log("�÷��̾� �̵� ����");
        _playerController.enabled = false;

        // ī�޶� y ��ǥ �����ϱ� -> mode �����ϸ鼭 ������

        // �÷��̾� ��ǥ 0, 0���� �ٲٱ�
        _player.transform.position = Vector3.zero;

        // �÷��̾� ����
        var playerRb = _player.GetComponent<Rigidbody2D>();
        playerRb.gravityScale = 1f;
        playerRb.velocity = Vector2.zero;

        // �÷��̾� �⺻ �ִϸ��̼�
        // todo: collider�� �浹�ϱ� ������ �������� �ִϸ��̼��̾��ٰ� �ٴڿ� ��� ���� idle���� ����
        var playerPh = _player.GetComponent<PlayerHandler>();
        playerPh.Idle();

        // ��� ���� �ʱ�ȭ
        _droneController.ResetDronePhysics();
        _droneController.ResetState();

        // ī�޶� ���� & ������ �̵�
        yield return StartCoroutine(CameraZoomAndShift());

        Debug.Log("�غ� �Ϸ�");
    }

    // todo: ī�޶� �̵� ���� ����
    private IEnumerator CameraZoomAndShift()
    {
        float duration = 1f;
        float t = 0f;
        Vector3 startPos = _originalCamPos;
        Vector3 targetPos = _originalCamPos + Vector3.right * _cameraShiftX;
        float startSize = _originalCamSize;

        while (t < duration)
        {
            t += Time.deltaTime;
            _camera.orthographicSize = Mathf.Lerp(startSize, _cameraTargetSize, t / duration);
            _camera.transform.position = Vector3.Lerp(startPos, targetPos, t / duration);
            yield return null;
        }
    }

    protected override void OnStart()
    {
        Debug.Log("GetFruitMiniGame start");
        CurrentState = MiniGameState.Start;
        _isFlap = true;
        _droneController.EnableGravity(); // �߷�
        _droneController.Flap(ref _isFlap);
    }

    protected override void OnPlaying()
    {
        if (CurrentState is MiniGameState.Stop)
        {
            if (_deathCooldown <= 0)
            {
                if (CheckStartInput())
                {
                    _miniGameManager.RestartGame(this);
                }
            }
            else
            {
                _deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if (CheckStartInput())
            {
                _isFlap = true;
            }
        }
    }

    protected override void FixedUpdate()
    {
        if (_droneController == null) return;
        if (CurrentState != MiniGameState.Start) return;

        if (_isFlap)
        {
            _droneController.Flap(ref _isFlap);
        }
    }
}
