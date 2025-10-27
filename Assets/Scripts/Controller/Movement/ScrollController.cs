using System;
using UnityEngine;

public class ScrollController : BaseController
{
    [SerializeField] private float _flapPower = 6f;
    [SerializeField] private float _forwardSpeed = 3f;

    private DroneHandler _animator;
    private bool _isDead;
    public bool IsDead => _isDead;
    public event Action? OnDroneDeath;

    private DroneMovementMode _droneMovementMode;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<DroneHandler>();
        _droneMovementMode = GetComponent<DroneMovementMode>();
    }

    protected override void Start()
    {
        base.Start();
        _rigidbody.freezeRotation = false;
        _isDead = false;
    }

    public void ResetDronePhysics()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.angularVelocity = 0f;
        _rigidbody.gravityScale = 0f;
        _rigidbody.rotation = 0f;

        _rigidbody.transform.position = Vector3.right * 4f;
        _rigidbody.transform.rotation = Quaternion.identity;
    }

    public void ResetState()
    {
        _isDead = false;
        if (_animator != null)
        {
            _animator.Idle();
        }
    }

    public void EnableGravity()
    {
        _rigidbody.gravityScale = 1f;
    }

    protected override void FixedUpdate()
    {
        if (_isDead) return;

        if (_rigidbody.gravityScale > 0f)
        {
            _rigidbody.velocity = new Vector2(
                _forwardSpeed,
                _rigidbody.velocity.y);

            Rotate();
        }
    }

    public void Flap(ref bool _isFlap)
    {
        if (_isFlap)
        {
            Debug.Log("Flag ผ๖วเ");

            Vector2 velocity = _rigidbody.velocity;
            velocity.y += _flapPower;
            _isFlap = false;
            _rigidbody.velocity = new Vector2(
                _forwardSpeed,
                velocity.y);
        }
    }

    protected override void Rotate()
    {
        float angle = Mathf.Clamp(_rigidbody.velocity.y * 5f, -45f, 45f);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDead) return;

        if (_droneMovementMode == null || !_droneMovementMode.IsMiniGameMode) return;

        _animator.Die();
        _isDead = true;

        OnDroneDeath?.Invoke();
    }

    public void ClearDeathEvent() { OnDroneDeath = null; }
}
