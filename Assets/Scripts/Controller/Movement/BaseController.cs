using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    protected PlayerHandler animationHandler;

    [SerializeField] protected SpriteRenderer characterRenderer;
    [SerializeField] protected Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection => movementDirection;

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LoookDirection => lookDirection;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<PlayerHandler>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate();
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

    protected virtual void Rotate()
    {
    }

    protected virtual void Ready()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.gravityScale = 0f;
    }
}
