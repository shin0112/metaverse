using UnityEngine;

public class TopdownController : BaseController
{
    protected Vector2 knockback = Vector2.zero;
    private float _knockbackDuration = .0f;

    public void ResetPlayer(Vector3 pos)
    {
        _rigidbody.transform.position = pos;
        _rigidbody.gravityScale = 0f;
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
        GetMovementAction();
        GetRotateAction();
    }

    protected virtual void GetMovementAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;
    }

    protected virtual void GetRotateAction()
    {
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
        animationHandler.Move(direction);
    }

    protected override void Rotate()
    {
        bool isLeft = movementDirection.x < 0;
        characterRenderer.flipX = isLeft;
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        _knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }
}
