using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection => movementDirection;

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LoookDirection => lookDirection;



    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        HandleAction();
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
    }

    protected virtual void HandleAction()
    {

    }

    protected virtual void Movement(Vector2 direction)
    {

    }

    protected virtual void Rotate(Vector2 direction)
    {

    }
}
