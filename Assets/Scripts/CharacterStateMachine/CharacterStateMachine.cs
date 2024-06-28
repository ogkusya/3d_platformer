using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterStateMachine : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    [Header("Movement Properties")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speedRotate = 3f;
    [SerializeField] private float heightJump = 5f;

    [Header("IsGrounded Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    [Header("Dash Properties")]
    [SerializeField] private float dashPower = 24f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldownTime = 2f;

    [Header("User Output")]
    [SerializeField] private string currentState;
    [SerializeField] private string currentVelocity;

    public CharacterAnimatorController CharacterAnimationController { get; private set; }

    private StateMachine _stateMachine;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private bool _isGrounded;
    private bool _isDashCooldown;
    private bool _isDashStartCoroutine;

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }


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

        if ((currentState == "CharacterDashState") && _isDashStartCoroutine == false)
        {
            _isDashCooldown = true;
            Debug.Log(currentState);
            _isDashStartCoroutine = true;
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        CheckGrounded();
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
        var jumpAndFallState = new CharacterJumpFallState(characterAnimatorController, inputManager, _rigidbody, speed, speedRotate, heightJump);
        var dashState = new CharacterDashState(CharacterAnimationController, inputManager, _rigidbody, dashPower, dashTime, dashCooldownTime);

        idleState.AddTransition(new StateTransition(runState, new FuncStateCondition(() => Mathf.Abs(inputManager.MoveDirectionHorizontal) > 0.1f)));
        runState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => Mathf.Abs(inputManager.MoveDirectionHorizontal) < 0.1f)));

        idleState.AddTransition(new StateTransition(jumpAndFallState, new FuncStateCondition(() => _isGrounded && inputManager.IsJumping)));
        runState.AddTransition(new StateTransition(jumpAndFallState, new FuncStateCondition(() => _isGrounded && inputManager.IsJumping)));

        jumpAndFallState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => _isGrounded && _rigidbody.velocity.y < 0.1f && Mathf.Abs(inputManager.MoveDirectionHorizontal) < 0.1f)));
        jumpAndFallState.AddTransition(new StateTransition(runState, new FuncStateCondition(() => _isGrounded && _rigidbody.velocity.y < 0.1f && Mathf.Abs(inputManager.MoveDirectionHorizontal) > 0.1f)));

        idleState.AddTransition(new StateTransition(dashState, new FuncStateCondition(() => inputManager.IsDashing && (_isDashCooldown == false))));
        runState.AddTransition(new StateTransition(dashState, new FuncStateCondition(() => inputManager.IsDashing && (_isDashCooldown == false))));
        jumpAndFallState.AddTransition(new StateTransition(dashState, new FuncStateCondition(() => inputManager.IsDashing && (_isDashCooldown == false))));

        dashState.AddTransition(new StateTransition(idleState, new FuncStateCondition(() => dashState.isDashing == false && Mathf.Abs(inputManager.MoveDirectionHorizontal) < 0.1f)));
        dashState.AddTransition(new StateTransition(runState, new FuncStateCondition(() => dashState.isDashing == false && Mathf.Abs(inputManager.MoveDirectionHorizontal) > 0.1f)));



        _stateMachine = new StateMachine(idleState);
    }

    private IEnumerator Dash()
    {
        yield return new WaitForSeconds(dashCooldownTime);
        _isDashCooldown = false;
        _isDashStartCoroutine = false;
    }
}
