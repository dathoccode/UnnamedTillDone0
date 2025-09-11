using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius;


    private PlayerBaseState currentState;

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

    }
}
