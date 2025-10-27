using UnityEngine;

public class DroneHandler : MonoBehaviour
{
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    protected Animator animator;

    public void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Die()
    {
        animator.SetBool(IsDead, true);
    }
}
