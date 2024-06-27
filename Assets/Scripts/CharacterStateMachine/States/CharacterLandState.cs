using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLandState : State
{
    private readonly CharacterAnimatorController characterAnimationController;

    public CharacterLandState(CharacterAnimatorController characterAnimationController)
    {
        this.characterAnimationController = characterAnimationController;
    }

    public override void OnStateEntered()
    {
        characterAnimationController.SetBool(CharacterAnimationParameter.Land, true);
    }

    public override void OnStateExited()
    {
        characterAnimationController.SetBool(CharacterAnimationParameter.Land, false);
    }
}
