using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRunState : State
{
    private readonly CharacterAnimatorController _characterAnimationController;
    private readonly InputManager _inputManager;
    private readonly Rigidbody _rigidBody;
    private readonly float _speed;
    private readonly float _speedRotate;

    public CharacterRunState(CharacterAnimatorController characterAnimationController, InputManager inputManager, Rigidbody rigidBody, float speed, float speedRotate)
    {
        _characterAnimationController = characterAnimationController;
        _inputManager = inputManager;
        _rigidBody = rigidBody;
        _speed = speed;
        _speedRotate = speedRotate;
    }

    public override void OnStateEntered()
    {
        
    }

    public override void OnStateExited()
    {
        _characterAnimationController.SetFloat(CharacterAnimationParameter.RunFloat, 0f);
        _rigidBody.velocity = Vector3.zero;
    }

    public override void OnFixedUpdate()
    {
        _characterAnimationController.SetFloat(CharacterAnimationParameter.RunFloat, Mathf.Abs(_inputManager.MoveDirectionHorizontal));
        Move();
        Rotate2();
        //Rotate();
    }

    private void Move()
    {
        _rigidBody.velocity = new Vector3(0, _rigidBody.velocity.y, _inputManager.MoveDirectionHorizontal * _speed);
    }

    private void Rotate()
    {
        var charForward = _rigidBody.transform.forward;
        var newDirection = Vector3.RotateTowards(charForward, _inputManager.MoveDirection, _speed, 0.0f);
        _rigidBody.rotation =
            Quaternion.Lerp(_rigidBody.transform.rotation, Quaternion.LookRotation(newDirection),
                _speedRotate * Time.deltaTime);
    }

    private void Rotate2()
    {
        if (_rigidBody.velocity != Vector3.zero)
        {
            _rigidBody.MoveRotation(Quaternion.LookRotation(new Vector3(0, 0, _rigidBody.velocity.z)));
        }
    }
}
