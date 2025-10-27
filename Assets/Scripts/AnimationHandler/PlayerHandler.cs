using UnityEngine;

public class PlayerHandler : AnimationHandler
{
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");

    protected Animator animator;

    public virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public override void Idle()
    {
        animator.SetBool(IsMoving, false);
        animator.SetBool(IsDamage, false);
    }

    public override void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);
    }

    public void Damage()
    {
        animator.SetBool(IsDamage, true);
    }

    public void InvicibilityEnd()
    {
        animator.SetBool(IsDamage, false);
    }
}
