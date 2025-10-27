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
        Debug.Log("��� �̴ϰ��� ��� Ȱ��ȭ");
    }

    public void SetDefaultMode()
    {
        _scrollController.enabled = false;
        _topdownController.enabled = true;
        Debug.Log("��� �⺻ ��� Ȱ��ȭ");
    }
}
