using UnityEngine;

public class DroneFollowController : BaseController
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _followSpeed = 5f; // 가능하면 나중에 수정해도
    [SerializeField] private float _stopDistance = 1.5f;

    protected override void FixedUpdate()
    {
        if (_target == null) return;

        Vector3 targetPos = _target.position + new Vector3(0, 1.5f, 0); // 1.5 높게
        Vector2 dir = targetPos - transform.position;

        if (dir.magnitude > _stopDistance)
        {
            movementDirection = dir.normalized;
            _rigidbody.velocity = movementDirection * _followSpeed;
            animationHandler?.Move(movementDirection);
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

    protected override void HandleAction()
    {
        base.HandleAction();
    }

    protected override void Movement(Vector2 direction)
    {
        base.Movement(direction);
    }

    protected override void Rotate()
    {
        base.Rotate();
    }

    protected override void Start()
    {
        base.Start();
        _rigidbody.gravityScale = 0f;
        _rigidbody.freezeRotation = true;

        if (_target == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                _target = player.transform;
            }
        }

        transform.position = new Vector3(0, 1.5f, 0);
    }

    protected override void Update()
    {
        base.Update();
    }
}
