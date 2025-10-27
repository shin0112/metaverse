using UnityEngine;

public abstract class AnimationHandler : MonoBehaviour
{
    protected Animator _animator;

    public abstract void Idle();
    public abstract void Move(Vector2 obj);
}
