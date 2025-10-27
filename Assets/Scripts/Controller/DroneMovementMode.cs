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
        Debug.Log("��� �̴ϰ��� ��� Ȱ��ȭ");
    }

    public void SetDefaultMode()
    {
        _scrollController.enabled = false;
        _droneFollowController.enabled = true;
        Debug.Log("��� �⺻ ��� Ȱ��ȭ");
    }
}
