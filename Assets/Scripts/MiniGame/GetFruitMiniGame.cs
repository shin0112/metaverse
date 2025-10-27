using System.Collections;
using UnityEngine;

public class GetFruitMiniGame : BaseMiniGame
{
    [Header("References")]
    [SerializeField] private GameObject _environment;
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

    public override void Init()
    {
        base.Init();

        if (_camera != null)
        {
            _camera.orthographicSize = _originalCamSize;
            _camera.transform.position = _originalCamPos;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        _playerController = _player.GetComponent<TopdownController>();
        _droneController = GetComponentInChildren<ScrollController>();
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
        throw new System.NotImplementedException();
    }

    protected override void OnPlaying()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _droneController.Flap(_flapPower);
        }
    }

    protected override void OnReady()
    {
        Debug.Log("GetFruit MiniGame 준비 중...");

        _environment.SetActive(true);
        StartCoroutine(PlayEnterSequence());

        Debug.Log("GetFruit MiniGame 준비 완료");
    }

    private IEnumerator PlayEnterSequence()
    {
        // 플레이어 낙하
        Debug.Log("플레이어 이동 금지");
        _playerController.enabled = false;
        var playerRb = _player.GetComponent<Rigidbody2D>();
        playerRb.gravityScale = 1f;
        playerRb.velocity = Vector2.zero;

        // 드론 


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
        _droneController.Flap(_flapPower);
    }

    protected override void StartGame()
    {
        base.StartGame();
    }

    protected override void Update()
    {
        base.Update();
    }
}
