using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdleState : State
{
    private readonly CharacterAnimatorController characterAnimationController;

    public CharacterIdleState(CharacterAnimatorController characterAnimationController)
    {
        this.characterAnimationController = characterAnimationController;
    }

    public override void OnStateEntered()
    {
        characterAnimationController.SetBool(CharacterAnimationParameter.Idle, true);
    }

    public override void OnStateExited()
    {
        characterAnimationController.SetBool(CharacterAnimationParameter.Idle, false);
    }
}
