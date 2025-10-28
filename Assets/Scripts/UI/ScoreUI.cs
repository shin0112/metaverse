using TMPro;

public class ScoreUI : BaseUI
{
    public TextMeshProUGUI TotalScore;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

    protected override UIState GetUIState()
    {
        return UIState.Score;
    }

    public void SetUI(int totalScore)
    {
        TotalScore.text = totalScore.ToString();
    }
}
