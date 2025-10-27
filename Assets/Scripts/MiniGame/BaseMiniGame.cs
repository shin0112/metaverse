using UnityEngine;

public abstract class BaseMiniGame : MonoBehaviour
{
    [SerializeField] protected BaseController _controller;

    protected bool _isReady = false;
    protected bool _isStart = false;

    public virtual void Init()
    {
        _isReady = true;
        _isStart = false;
        OnReady();
    }

    protected virtual void Update()
    {
        if (_isReady && !_isStart && CheckStartInput())
        {
            StartGame();
        }

        if (_isStart)
            OnPlaying();
    }

    protected virtual bool CheckStartInput()
    {
        // space or 마우스 좌클릭
        return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
    }

    protected virtual void StartGame()
    {
        _isReady = false;
        _isStart = true;
        OnStart();
    }

    protected abstract void OnReady();
    protected abstract void OnStart();
    protected abstract void OnPlaying();
    protected abstract void OnExit();
}
