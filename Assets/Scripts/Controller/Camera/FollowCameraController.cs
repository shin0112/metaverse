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

        // y°ª ¼³Á¤
        float targetY = _lockY ? _fixedY : target.position.y + _offsetX;

        Vector3 pos = new(
            target.position.x + _offsetX,
            targetY,
            transform.position.z
        );

        transform.position = pos;
    }

    public void OnEnterDroneMode()
    {

    }
}
