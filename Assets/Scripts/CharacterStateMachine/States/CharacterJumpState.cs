using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJumpState : State
{
    private readonly CharacterAnimatorController _characterAnimationController;
    private readonly InputManager _inputManager;
    private readonly Rigidbody _rigidBody;
    private readonly float _speed;
    private readonly float _speedRotate;
    private readonly float _forceJump;
    private bool _isJumping = false;

    public CharacterJumpState(CharacterAnimatorController characterAnimationController, InputManager inputManager, Rigidbody rigidBody, float speed, float speedRotate, float forceJump)
    {
        _characterAnimationController = characterAnimationController;
        _inputManager = inputManager;
        _rigidBody = rigidBody;
        _speed = speed;
        _speedRotate = speedRotate;
        _forceJump = forceJump;
    }

    public override void OnStateEntered()
    {
        if (!_isJumping)
        {
            _isJumping = true;
            _characterAnimationController.SetBool(CharacterAnimationParameter.Grounded, false);
            Jump();
        }
    }

    public override void OnStateExited()
    {
        _isJumping = false;
    }

    public override void OnFixedUpdate()
    {
        if (_rigidBody.velocity.y != 0)
        {
            Move();
            Rotate2();
        }
    }

    private void Jump()
    {
        _rigidBody.AddForce(Vector3.up * _forceJump, ForceMode.Impulse);
    }

    private void Move()
    {
        _rigidBody.velocity = new Vector3(0, _rigidBody.velocity.y, _inputManager.MoveDirectionHorizontal * _speed);
        //_rigidBody.velocity = _inputManager.MoveDirection * _speed;
    }

    private void Rotate2()
    {
        if (_rigidBody.velocity != Vector3.zero)
        {
            _rigidBody.MoveRotation(Quaternion.LookRotation(new Vector3(0, 0, _rigidBody.velocity.z)));
        }
    }


    
}
