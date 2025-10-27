using UnityEngine;

public class DroneMovementMode : MonoBehaviour
{
    private ScrollController _scrollController;
    private DroneFollowController _droneFollowController;

    public void Start()
    {
        _scrollController = GetComponent<ScrollController>();
        _droneFollowController = GetComponent<DroneFollowController>();

        SetDefaultMode();
    }

    public void SetMiniGameMode()
    {
        _scrollController.enabled = true;
        _droneFollowController.enabled = false;
        Debug.Log("드론 미니게임 모드 활성화");
    }

    public void SetDefaultMode()
    {
        _scrollController.enabled = false;
        _droneFollowController.enabled = true;
        Debug.Log("드론 기본 모드 활성화");
    }
}
