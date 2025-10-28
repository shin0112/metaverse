using TMPro;

public class GetFruitUI : BaseUI
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI infoText;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

    protected override UIState GetUIState()
    {
        return UIState.GetFruit;
    }

    public void SetUI(int score)
    {
        this.score.text = score.ToString();
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
