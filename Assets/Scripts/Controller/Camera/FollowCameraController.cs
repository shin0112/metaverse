using UnityEngine;

public class FollowCameraController : MonoBehaviour
{
    public Transform target;

    [Header("Axis Options")]
    [SerializeField] private bool _lockY = false;
    [SerializeField] private float _fixedY = 0f;

    float offsetX;
    float offsetY;

    private void Start()
    {
        if (target == null) return;

        offsetX = transform.position.x - target.position.x;
        offsetY = transform.position.y - target.position.y;
    }

    private void Update()
    {
        if (target == null) return;

        float targetY = _lockY ? _fixedY : target.position.y + offsetX;
        Vector3 pos = new(target.position.x + offsetX, targetY, transform.position.z)
        {
            x = target.position.x + offsetX,
            y = target.position.y + offsetY
        };

        transform.position = pos;
    }
}
