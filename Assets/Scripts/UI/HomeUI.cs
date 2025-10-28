using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    Button _exitButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        _exitButton = transform.Find("ExitButton").GetComponent<Button>();
        if (_exitButton == null) Debug.Log("Exit Button ����");

        _exitButton.onClick.AddListener(OnClickExitButton);
    }

    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    void OnClickExitButton()
    {
        Debug.Log("Exit Button Ŭ��");
        uiManager.OnClickExit();
    }
}
