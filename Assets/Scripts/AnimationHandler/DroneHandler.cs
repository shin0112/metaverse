using System.Collections;
using UnityEngine;

public class DroneHandler : MonoBehaviour
{
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    private Animator _animator;

    public void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Die()
    {
        _animator.SetBool(IsDamage, true);
        StartCoroutine(TransitionToDead(1f)); // 1ÃÊ µÚ »ç¸Á
    }

    private IEnumerator TransitionToDead(float delay)
    {
        yield return new WaitForSeconds(delay);

        _animator.SetBool(IsDamage, false);
        _animator.SetBool(IsDead, true);

        Debug.Log("µå·Ð ÆÄ±«");
    }
}
