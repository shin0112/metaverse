using TMPro;

public class GetFruitUI : BaseUI
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI infoText;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

    protected override UIState GetUIState()
    {
        return UIState.GetFruit;
    }

    public void SetUI(int score, int bestScore)
    {
        this.score.text = score.ToString();
        this.bestScore.text = bestScore.ToString();
    }

    public void ShowInfoText()
    {
        infoText.gameObject.SetActive(true);
    }

    public void HideInfoText()
    {
        infoText.gameObject.SetActive(false);
    }
}
