using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine context) : base(context) {}

    //make sure the character really left the ground so it cant change state right after jump
    private bool hasLeftGround;

    public override void Enter()
    {
        hasLeftGround = false;
        ctx.animator.Play("Player_Jump");
        ctx.Jump(ctx.jumpForce);
    }

    public override void Exit()
    {
        ctx.Move(0, InputManager.Instance.GetMoveInput());
    }

    public override void Update()
    {
        Vector2 moveInput = InputManager.Instance.GetMoveInput();

        if (!ctx.IsGrounded()) hasLeftGround = true;
        if (ctx.IsGrounded() && hasLeftGround)
        {
            if (Mathf.Abs(moveInput.x) > 0.1f) ctx.SwitchState(new PlayerRunState(ctx));
            else ctx.SwitchState(new PlayerIdleState(ctx));
        }
    }
}
