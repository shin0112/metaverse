using System.Collections;
using UnityEngine;

public class DroneHandler : AnimationHandler
{
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    private void Update()
    {
        //DebugAnimatorState(); // F1 누르면 콘솔에 Animator 상태 전부 출력
    }


    public void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Die()
    {
        _animator.SetBool(IsDamage, true);
        StartCoroutine(TransitionToDead(1f)); // 1초 뒤 사망
    }

    public override void Idle()
    {
        StopAllCoroutines();
        Debug.Log("모든 코루틴 종료");

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

        Debug.Log("드론 파괴");
    }

    private void DebugAnimatorState()
    {
        if (_animator == null)
        {
            Debug.LogWarning("Animator 없음");
            return;
        }

        // 현재 상태 정보
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        Debug.Log($"🎬현재 State: {stateInfo.fullPathHash} / normalizedTime: {stateInfo.normalizedTime}");

        // 이름으로도 확인 가능 (단, Layer 0 기준)
        Debug.Log($"현재 애니메이션 상태 이름: {GetCurrentStateName(stateInfo)}");

        // 파라미터 전부 출력
        foreach (AnimatorControllerParameter p in _animator.parameters)
        {
            switch (p.type)
            {
                case AnimatorControllerParameterType.Bool:
                    Debug.Log($"Bool - {p.name}: {_animator.GetBool(p.name)}");
                    break;
                case AnimatorControllerParameterType.Float:
                    Debug.Log($"Float - {p.name}: {_animator.GetFloat(p.name)}");
                    break;
                case AnimatorControllerParameterType.Int:
                    Debug.Log($"Int - {p.name}: {_animator.GetInteger(p.name)}");
                    break;
                case AnimatorControllerParameterType.Trigger:
                    Debug.Log($"Trigger - {p.name} (트리거 값은 읽을 수 없음)");
                    break;
            }
        }
    }

    private string GetCurrentStateName(AnimatorStateInfo info)
    {
        // 이름 확인용 보조 함수
        if (_animator == null) return "Animator 없음";
        var clips = _animator.GetCurrentAnimatorClipInfo(0);
        if (clips.Length > 0) return clips[0].clip.name;
        return "이름 확인 불가";
    }

}
