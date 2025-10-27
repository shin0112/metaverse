using UnityEngine;

public class ScrollController : BaseController
{
    [SerializeField] private float _flapPower = 5f;

    private DroneHandler _animator;
    private bool _isDead;

    protected override void Start()
    {
        base.Start();
        _rigidbody.gravityScale = 1f;
        _animator = GetComponent<DroneHandler>();
        _isDead = false;
    }

    public void Flap(float power)
    {
        _rigidbody.velocity = Vector2.up * power;
    }

    protected override void Rotate()
    {
        float angle = Mathf.Clamp(_rigidbody.velocity.y * 5f, -45f, 45f);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDead) return;

        _animator.Die();
        _isDead = true;
    }
}
