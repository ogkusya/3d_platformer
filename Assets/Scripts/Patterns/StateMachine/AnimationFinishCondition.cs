using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFinishCondition : StateCondition
{
    private readonly Animator _animator;
    private readonly string _name;
    private readonly float _finishTime;

    public AnimationFinishCondition(Animator animator, string name, float finishTime = 0.8f)
    {
        _animator = animator;
        _name = name;
        _finishTime = finishTime;
    }

    public override bool IsConditionSuccessed()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > _finishTime && _animator
            .GetCurrentAnimatorStateInfo(0)
            .IsName(_name);
    }
}
