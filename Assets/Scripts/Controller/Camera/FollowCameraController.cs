using UnityEngine;

public class FollowCameraController : MonoBehaviour
{
    public Transform target;

    [Header("Axis Options")]
    [SerializeField] private bool _lockY = false;
    [SerializeField] private float _fixedY = 0f;

    float _offsetX;
    float _offsetY;

    private void Start()
    {
        if (target == null) return;

        _offsetX = transform.position.x - target.position.x;
        _offsetY = transform.position.y - target.position.y;
    }

    private void Update()
    {
        if (target == null) return;

        // y값 설정
        float targetY = _lockY ? _fixedY : target.position.y + _offsetX;

        Vector3 pos = new(
            target.position.x + _offsetX,
            targetY,
            transform.position.z
        );

        transform.position = pos;
    }

    public void OnEnterGetFruitMode(Transform transform)
    {
        target = transform;
        _lockY = true;
    }

    public void ChangeTarget(Transform transform)
    {
        target = transform;
        _lockY = false;

        // offset 재계산
        _offsetX = 0f;
        _offsetY = 0f;

        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );
    }
}
