using UnityEngine;

public class DroneMovementMode : MonoBehaviour
{
    private ScrollController _scrollController;
    private TopdownController _topdownController;

    public void Start()
    {
        _scrollController = GetComponent<ScrollController>();
        _topdownController = GetComponent<TopdownController>();

        SetDefaultMode();
    }

    public void SetMiniGameMode()
    {
        _scrollController.enabled = true;
        _topdownController.enabled = false;
        Debug.Log("드론 미니게임 모드 활성화");
    }

    public void SetDefaultMode()
    {
        _scrollController.enabled = false;
        _topdownController.enabled = true;
        Debug.Log("드론 기본 모드 활성화");
    }
}
