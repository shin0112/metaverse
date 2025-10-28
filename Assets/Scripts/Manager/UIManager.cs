using UnityEngine;

public enum UIState
{
    Home,
    GetFruit,
    Score
}

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    UIState _currentState = UIState.Home;
    GetFruitUI _getFruitUI = null;
    ScoreUI _scoreUI = null;
    HomeUI _homeUI = null;

    GetFruitMiniGame _getFruitMiniGame = null;
    ScoreManager _scoreManager = null;

    private void Awake()
    {
        _instance = this;

        _getFruitUI = GetComponentInChildren<GetFruitUI>(true);
        _getFruitUI?.Init(this);
        _scoreUI = GetComponentInChildren<ScoreUI>(true);
        _scoreUI?.Init(this);
        _homeUI = GetComponentInChildren<HomeUI>(true);
        _homeUI?.Init(this);

        _getFruitMiniGame = FindObjectOfType<GetFruitMiniGame>(true);

        ChangeState(UIState.Home);
    }

    private void Start()
    {
        _scoreManager = ScoreManager.Instance;
        _scoreManager.LoadTotalScore(); // 점수 불러오기
        UpdateScore(); // ui로 보여주기
    }

    public void ChangeState(UIState state)
    {
        _currentState = state;
        _getFruitUI?.SetActive(_currentState);
        //_scoreUI?.SetActive(_currentState);
        _homeUI?.SetActive(_currentState);
    }

    public void ShowInfoText()
    {
        switch (_currentState)
        {
            case UIState.GetFruit:
                _getFruitUI.ShowInfoText();
                break;
            default:
                break;
        }
    }

    public void HideInfoText()
    {
        switch (_currentState)
        {
            case UIState.GetFruit:
                _getFruitUI.HideInfoText();
                break;
            default:
                break;
        }
    }

    public void OnClickStart()
    {
        ChangeState(UIState.GetFruit);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR // 이런 식으로 작성하면 각 os에 맞춰서 다른 동작도 가능함
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void UpdateScore()
    {
        _getFruitUI.SetUI(_scoreManager.FruitScore);
        _scoreUI.SetUI(_scoreManager.TotalScore);
    }
}
