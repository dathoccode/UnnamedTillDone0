using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = default;
    [SerializeField] private float jumpForce = default;
    [SerializeField] private float jumpCutMultiplier = 0.5f; // Cut height if button released (not used yet)


    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;


    Rigidbody2D rb;
    bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if player is on ground or jumping
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //get input values
        Vector2 moveInput = InputManager.Instance.GetMoveInput();
        bool isJumpPressed = InputManager.Instance.JumpTriggeredThisFrame();

        // Handle jump press
        if (InputManager.Instance.JumpTriggeredThisFrame() && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Handle jump release (variable height)
        if (!isGrounded && !InputManager.Instance.JumpHeld() && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier);
        }
    }

    //for debugging: in scene view(doesn't work in game window), if the object is selected this function will automatically called
    //check if the player is grounded or not on scene (when selected)
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
