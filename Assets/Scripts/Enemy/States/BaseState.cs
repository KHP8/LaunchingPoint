public abstract class BaseState
{
    public BaseEnemy enemy;
    public StateMachine stateMachine;

    /// <summary>
    /// Enter the state
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// Perform the state's actions on Update()
    /// </summary>
    public abstract void Perform();

    /// <summary>
    /// Exit the state
    /// </summary>
    public abstract void Exit();
}