using System.Collections;
using UnityEngine;

public class DroneHandler : AnimationHandler
{
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");
    private static readonly int IsDead = Animator.StringToHash("IsDead");


    public void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Die()
    {
        _animator.SetBool(IsDamage, true);
        StartCoroutine(TransitionToDead(1f)); // 1ÃÊ µÚ »ç¸Á
    }

    public override void Idle()
    {
        _animator.SetBool(IsDamage, false);
        _animator.SetBool(IsDead, false);
    }

    public override void Move(Vector2 obj)
    {

    }

    private IEnumerator TransitionToDead(float delay)
    {
        yield return new WaitForSeconds(delay);

        _animator.SetBool(IsDamage, false);
        _animator.SetBool(IsDead, true);

        Debug.Log("µå·Ð ÆÄ±«");
    }
}
