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
    private DroneMovementMode _droneMovementMode;

    private bool _isFlap = false;
    private float _deathCooldown = 0f;
    private bool _isFirstEnter = true;

    private MiniGameManager _miniGameManager;

    public override void Init()
    {
        base.Init();
        Debug.Log($"{this.name} Init ȣ��");

        _miniGameManager = MiniGameManager.Instance;

        if (_camera != null)
        {
            _camera.orthographicSize = _originalCamSize;
            _camera.transform.position = _originalCamPos;
        }

        _droneMovementMode.SetMiniGameMode();

        _droneController.ClearDeathEvent();
        _droneController.OnDroneDeath += () =>
        {
            //_isStart = false;
            //_isReady = true;
            _deathCooldown = .5f;
            Debug.Log("��� �ı�");
        };

        OnReady();
    }

    protected override void Awake()
    {
        base.Awake();

        _playerController = _player.GetComponent<TopdownController>();
        _droneController = _drone.GetComponent<ScrollController>();
        _controller = _droneController;
        _droneMovementMode = _drone.GetComponent<DroneMovementMode>();

        if (_camera == null)
        {
            _camera = Camera.main;
        }

        _originalCamPos = _camera.transform.position;
        _originalCamSize = _camera.orthographicSize;
    }

    protected override void OnExit()
    {
        _droneMovementMode.SetDefaultMode();

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
            _droneController.EnableGravity();

            _isReady = true;
            _isPreparing = false;
            Debug.Log("�ٷ� Ready �Ϸ�");
        }

        Debug.Log("GetFruit MiniGame �غ� �Ϸ�");
    }

    private IEnumerator PlayEnterSequence()
    {
        _isPreparing = true;

        Debug.Log("�÷��̾� �̵� ����");
        _playerController.enabled = false;

        // ī�޶� y ��ǥ �����ϱ�

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

        _isReady = true;
        _isPreparing = false;
        Debug.Log("�غ� �Ϸ�");
    }

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
        _isFlap = true;
        _isStart = true;
        _droneController.EnableGravity(); // �߷�
        _droneController.Flap(ref _isFlap);
    }

    protected override void OnPlaying()
    {
        if (_droneController.IsDead)
        {
            if (_deathCooldown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
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
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                _isFlap = true;
            }
        }
    }

    protected override void FixedUpdate()
    {
        if (_droneController == null || !_isStart) return;
        if (_droneController.IsDead) return;

        if (_isFlap)
        {
            _droneController.Flap(ref _isFlap);
        }
    }
}
