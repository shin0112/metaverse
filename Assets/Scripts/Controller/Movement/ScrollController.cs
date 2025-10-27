using System;
using UnityEngine;

public class ScrollController : BaseController
{
    [SerializeField] private float _flapPower = 5f;
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

    public void GetGravity()
    {
        _rigidbody.gravityScale = 1f;
    }

    public void Flap(ref bool _isFlap)
    {
        Debug.Log("Flag Ω√¿€");

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = _forwardSpeed;

        if (_isFlap)
        {
            velocity.y += _flapPower;
            _isFlap = false;
        }

        _rigidbody.velocity = velocity;
        Rotate();
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
}
