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

    GetFruitMiniGame _getFruitMiniGame = null;

    private void Awake()
    {
        _instance = this;

        _getFruitUI = GetComponentInChildren<GetFruitUI>(true);
        _scoreUI = GetComponentInChildren<ScoreUI>(true);

        _getFruitMiniGame = FindObjectOfType<GetFruitMiniGame>(true);

        ChangeState(UIState.Home);
    }

    public void ChangeState(UIState state)
    {
        _currentState = state;
        _getFruitUI?.SetActive(_currentState);
    }

    public void OnClickStart()
    {
        ChangeState(UIState.GetFruit);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR // �̷� ������ �ۼ��ϸ� �� os�� ���缭 �ٸ� ���۵� ������
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }

    public void UpdateScore()
    {
        _getFruitUI.SetUI(ScoreManager.Instance.FruitScore);
    }
}
