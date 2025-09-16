using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine context) : base(context) {}

    public override void Enter()
    {
        ctx.animator.SetTrigger("Jump");
        ctx.Jump(ctx.jumpForce);
    }

    public override void Exit()
    {
        ctx.animator.ResetTrigger("Jump");
    }

    public override void Update()
    {
        Vector2 moveInput = InputManager.Instance.GetMoveInput();
        if (ctx.IsGrounded())
        {
            ctx.SwitchState(new PlayerIdleState(ctx));
        }
    }
}
