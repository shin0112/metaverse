using UnityEngine;

public class TopdownController : BaseController
{
    protected Vector2 knockback = Vector2.zero;
    private float _knockbackDuration = .0f;

    protected override void Update()
    {
        base.Update();
        Rotate(lookDirection);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_knockbackDuration > .0f)
        {
            _knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;
    }

    protected override void Movement(Vector2 direction)
    {
        direction = direction * 5;

        if (_knockbackDuration > .0f)
        {
            direction *= .2f;
            direction += knockback;
        }

        _rigidbody.velocity = direction;
    }

    protected override void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;
    }
}
