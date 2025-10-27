using UnityEngine;

public class ScrollController : BaseController
{
    [SerializeField] private float _flapPower = 5f;

    protected override void Start()
    {
        base.Start();
        _rigidbody.gravityScale = 1f;
    }

    public void Flap(float power)
    {
        _rigidbody.velocity = Vector2.up * power;

        // 드론 애니메이션
    }

    protected override void Rotate()
    {
        float angle = Mathf.Clamp(_rigidbody.velocity.y * 5f, -45f, 45f);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
