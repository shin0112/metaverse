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
        Debug.Log($"{this.name} Init 호출");

        if (_camera != null)
        {
            _camera.orthographicSize = _originalCamSize;
            _camera.transform.position = _originalCamPos;
        }

        _droneMovementMode.SetMiniGameMode();

        _droneController.OnDroneDeath += () =>
        {
            _deathCooldown = 1f;
            Debug.Log("드론 파괴");
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
        Debug.Log("GetFruit MiniGame 준비 중...");

        StartCoroutine(PlayEnterSequence());

        Debug.Log("GetFruit MiniGame 준비 완료");
    }

    private IEnumerator PlayEnterSequence()
    {
        Debug.Log("플레이어 이동 금지");
        _playerController.enabled = false;

        // 카메라 y 좌표 고정하기

        // 플레이어 좌표 0, 0으로 바꾸기
        _player.transform.position = Vector3.zero;

        // 플레이어 낙하
        var playerRb = _player.GetComponent<Rigidbody2D>();
        playerRb.gravityScale = 1f;
        playerRb.velocity = Vector2.zero;

        // 플레이어 기본 애니메이션
        // todo: collider에 충돌하기 전까지 떨어지는 애니메이션이었다가 바닥에 닿는 순간 idle으로 변경
        var playerPh = _player.GetComponent<PlayerHandler>();
        playerPh.Idle();

        // 드론 좌표 4, 0
        _drone.transform.position = Vector3.right * 4;


        // 카메라 줌인 & 오른쪽 이동
        yield return StartCoroutine(CameraZoomAndShift());

        // 대기
        _isReady = true;

        Debug.Log("준비 완료");
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
        _droneController.GetGravity(); // 중력
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
