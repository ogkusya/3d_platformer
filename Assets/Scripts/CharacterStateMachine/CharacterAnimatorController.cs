using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorController
{
    private Animator _animator;
    private Dictionary<CharacterAnimationParameter, int> hashStorage = new Dictionary<CharacterAnimationParameter, int>();

    public Animator Animator => _animator;

    public CharacterAnimatorController(Animator animator)
    {
        _animator = animator;
        foreach (CharacterAnimationParameter caType in Enum.GetValues(typeof(CharacterAnimationParameter)))
        {
            hashStorage.Add(caType, Animator.StringToHash(caType.ToString()));
        }
    }

    public void SetBool(CharacterAnimationParameter animationType, bool value)
    {
        _animator.SetBool(hashStorage[animationType], value);
    }

    public void SetFloat(CharacterAnimationParameter animationType, float value)
    {
        _animator.SetFloat(hashStorage[animationType], value);
    }

    public void SetPlay(CharacterAnimationParameter characterAnimationType)
    {
        _animator.Play((hashStorage[characterAnimationType]));
    }

    public void SetTrigger(CharacterAnimationParameter characterAnimationType)
    {
        _animator.SetTrigger((hashStorage[characterAnimationType]));
    }

    public void ResetTrigger(CharacterAnimationParameter characterAnimationType)
    {
        _animator.ResetTrigger((hashStorage[characterAnimationType]));
    }

    public bool IsAnimationPlay(string animationStateName)
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName(animationStateName);
    }

    public float NormalizedAnimationPlayTime()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
