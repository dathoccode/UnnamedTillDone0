using System;
using UnityEngine;


public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine context) : base(context) { }
    public override void Enter()
    {

    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        Vector2 moveInput = InputManager.Instance.GetMoveInput();
        if (moveInput.x != 0)
        {
            ctx.SwitchState(new PlayerRunState(ctx));
        }

        if (InputManager.Instance.JumpTriggeredThisFrame() && ctx.IsGrounded())
        {
            ctx.SwitchState(new PlayerJumpState(ctx));
        }
    }
}

