using UnityEngine;

public class PlayerRunState : PlayerBaseState
{

    public PlayerRunState(PlayerStateMachine context) : base(context) { }
    public override void Enter()
    {
        ctx.animator.SetBool("Run", true);
    }

    public override void Exit()
    {
        ctx.animator.SetBool("Run", false);
    }

    public override void Update()
    {
        Vector2 moveInput = InputManager.Instance.GetMoveInput();
        
        ctx.Move(ctx.moveSpeed, moveInput);
        
        if (moveInput.x == 0)
        {
            ctx.SwitchState(new PlayerIdleState(ctx));
        }
        
        if (InputManager.Instance.JumpTriggeredThisFrame() && ctx.IsGrounded())
        {
            ctx.SwitchState(new PlayerJumpState(ctx));
        }
    }
}