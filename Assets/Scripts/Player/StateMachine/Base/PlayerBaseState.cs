public abstract class PlayerBaseState
{
    protected PlayerStateMachine ctx;
    protected PlayerBaseState(PlayerStateMachine context)
    {
        ctx = context;
    }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}