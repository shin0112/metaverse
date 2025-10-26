using UnityEngine;

public class FollowCameraController : MonoBehaviour
{
    public Transform target;
    float offsetX;

    private void Start()
    {
        if (target == null) return;

        offsetX = transform.position.x - target.position.x;
    }

    private void Update()
    {
        if (target == null) return;

        Vector3 pos = transform.position;
        pos.x = target.position.x + offsetX;
        transform.position = pos;
    }
}
