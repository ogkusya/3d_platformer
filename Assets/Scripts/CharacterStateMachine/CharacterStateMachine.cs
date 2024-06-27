using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterStateMachine : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speedRotate = 3f;
    [SerializeField] private float heightJump = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    [SerializeField] private string currentState;
    [SerializeField] private string currentVelocity;

    public CharacterAnimatorController CharacterAnimationController { get; private set; }

    private StateMachine _stateMachine;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private bool _isGrounded = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        InitializeStateMachine();
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
        currentState = _stateMachine.CurrentState.ToString();
        currentVelocity = _rigidbody.velocity.ToString();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        Debug.Log(_isGrounded);
        _stateMachine.OnFixedUpdate();
    }

    private void CheckGrounded()
    {
        Collider[] groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        _isGrounded = groundCollisions.Length > 0 ? true : false;
        CharacterAnimationController.SetBool(CharacterAnimationParameter.Grounded, _isGrounded);
    }

    private void InitializeStateMachine()
    {
        CharacterAnimatorController characterAnimatorController = new CharacterAnimatorController(_animator);
        CharacterAnimationController = characterAnimatorController;

        var idleState = new CharacterIdleState(characterAnimatorController);
        var runState = new CharacterRunState(characterAnimatorController, inputManager, _rigidbody, speed, speedRotate);
        var jumpAndFallState = new CharacterJumpState(characterAnimatorController, inputManager, _rigidbody, speed, speedRotate, heightJump);
        //var landState = new CharacterLandState(characterAnimatorController);

        idleState.AddTransition(new StateTransition(runState, new FuncStateCondition(() => Mathf.Abs(inputManager.MoveDirectionHorizontal) > 0.1f)));
        runState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => Mathf.Abs(inputManager.MoveDirectionHorizontal) < 0.1f)));

        idleState.AddTransition(new StateTransition(jumpAndFallState, new FuncStateCondition(() => _isGrounded && inputManager.IsJumping)));
        runState.AddTransition(new StateTransition(jumpAndFallState, new FuncStateCondition(() => _isGrounded && inputManager.IsJumping)));

        jumpAndFallState.AddTransition(new StateTransition(idleState, new AnimationFinishCondition(_animator, CharacterAnimationParameter.Land.ToString())));

        _stateMachine = new StateMachine(idleState);
    }
}
