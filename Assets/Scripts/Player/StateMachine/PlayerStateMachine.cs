using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius;

    [Header("Movement")]
    [SerializeField] public float moveSpeed = default;
    [SerializeField] public float jumpForce = default;

    private PlayerBaseState currentState;

    //
    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerDeathState DeathState { get; private set; }


    private void Awake()
    {
        IdleState = new PlayerIdleState(this);
        RunState = new PlayerRunState(this);
        JumpState = new PlayerJumpState(this);
        DeathState = new PlayerDeathState(this);
    }
    private void Start()
    {
        currentState = new PlayerIdleState(this);
        currentState.Enter();
    }

    private void Update()
    {
        currentState.Update();
    }

    public void SwitchState(PlayerBaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void Jump(float jumpForce)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
    }

    public void Move(float moveSpeed, Vector2 moveInput)
    {
        if (moveInput.x < 0) spriteRenderer.flipX = true;
        else if (moveInput.x > 0) spriteRenderer.flipX = false;
        rb.linearVelocityX = moveInput.x * moveSpeed;
    }

    private void OnDrawGizmosSelected()
    {

        if (IsGrounded())
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
