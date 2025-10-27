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

    private Rigidbody2D _rigidbody = null;
    private float _deathCooldown = 0f;
    private bool _isFlap = false;

    public override void Init()
    {
        base.Init();
        Debug.Log($"{this.name} Init ȣ��");

        if (_camera != null)
        {
            _camera.orthographicSize = _originalCamSize;
            _camera.transform.position = _originalCamPos;
        }

        _droneMovementMode.SetMiniGameMode();

        _droneController.OnDroneDeath += () =>
        {
            _deathCooldown = 1f;
            Debug.Log("��� �ı�");
        };

        OnReady();
    }

    protected override void Awake()
    {
        base.Awake();

        _playerController = _player.GetComponent<TopdownController>();
        _droneController = GetComponentInChildren<ScrollController>();
        _controller = _droneController;
        _droneMovementMode = _drone.GetComponent<DroneMovementMode>();
        _rigidbody = _drone.GetComponent<Rigidbody2D>();

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
    }

    protected override void OnPlaying()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _droneController.Flap(ref _isFlap);
        }
    }

    protected override void OnReady()
    {
        Debug.Log("GetFruit MiniGame �غ� ��...");

        StartCoroutine(PlayEnterSequence());

        Debug.Log("GetFruit MiniGame �غ� �Ϸ�");
    }

    private IEnumerator PlayEnterSequence()
    {
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

        // ��� ��ǥ 4, 0
        _drone.transform.position = Vector3.right * 4;


        // ī�޶� ���� & ������ �̵�
        yield return StartCoroutine(CameraZoomAndShift());

        // ���
        _isReady = true;

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
        _droneController.GetGravity(); // �߷�
        _droneController.Flap(ref _isFlap);
    }

    protected override void Update()
    {
        if (_droneController.IsDead)
        {
            if (_deathCooldown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
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
        if (_droneController.IsDead) return;
        OnPlaying();
    }
}
