using UnityEngine;

public enum MiniGameState
{
    Preparing,
    Ready,
    Wait,
    Start,
    Stop,
}

public abstract class BaseMiniGame : MonoBehaviour
{
    protected BaseController _controller;

    private MiniGameState _currentState = MiniGameState.Preparing;
    protected MiniGameState CurrentState
    {
        get { return _currentState; }
        set
        {
            _currentState = value;
            Debug.Log($"현재 상태: {_currentState.ToString()}");
        }
    }

    protected virtual void Awake()
    {

    }

    public virtual void Init()
    {
        CurrentState = MiniGameState.Preparing;
    }

    protected virtual void Update()
    {
        if (_controller == null) return;

        if (CurrentState is MiniGameState.Stop && CheckStartInput())
        {
            OnReady();
        }
        else if (CurrentState is MiniGameState.Ready && CheckStartInput())
        {
            StartGame();
        }
        else if (CurrentState is MiniGameState.Start)
        {
            OnPlaying();
        }
    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual bool CheckStartInput()
    {
        // space or 마우스 좌클릭
        return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
    }

    protected virtual void StartGame()
    {
        CurrentState = MiniGameState.Start;
        OnStart();
    }

    protected abstract void OnReady();
    protected abstract void OnStart();
    protected abstract void OnPlaying();
    protected abstract void OnExit();
}
