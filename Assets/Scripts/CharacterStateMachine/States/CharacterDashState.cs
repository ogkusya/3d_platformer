using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDashState : State
{
    private readonly CharacterAnimatorController _characterAnimationController;
    private readonly InputManager _inputManager;
    private readonly Rigidbody _rigidBody;
    public bool isDashing { get; private set; }
    private float _dashPower;
    private float _dashTime;
    private float _dashTimeLeft;

    public CharacterDashState(CharacterAnimatorController characterAnimationController, InputManager inputManager, Rigidbody rigidBody, float dashPower, float dashTime, float dashCooldown)
    {
        _characterAnimationController = characterAnimationController;
        _inputManager = inputManager;
        _rigidBody = rigidBody;
        _dashPower = dashPower;
        _dashTime = dashTime;
    }

    public override void OnStateEntered()
    {
        Dash();
        // _characterAnimationController.SetBool(CharacterAnimationParameter.DashBool, true);
    }

    public override void OnStateExited()
    {
        //_characterAnimationController.SetBool(CharacterAnimationParameter.DashBool, false);
    }

    public override void OnFixedUpdate()
    {
        if (isDashing)
        {
            _dashTimeLeft -= Time.fixedDeltaTime;
            if (_dashTimeLeft < 0)
            {
                _rigidBody.velocity = Vector3.zero;
                isDashing = false;
            }
        }

    }
    private void Dash()
    {
        _rigidBody.velocity = new Vector3(0, 0, _rigidBody.transform.forward.z * _dashPower);
        isDashing = true;
        _dashTimeLeft = _dashTime;
    }
}
