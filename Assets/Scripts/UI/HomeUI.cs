using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    Button _exitButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        _exitButton = transform.Find("ExitButton").GetComponent<Button>();
        if (_exitButton == null) Debug.Log("Exit Button 없음");

        _exitButton.onClick.AddListener(OnClickExitButton);
    }

    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    void OnClickExitButton()
    {
        Debug.Log("Exit Button 클릭");
        uiManager.OnClickExit();
    }
}
