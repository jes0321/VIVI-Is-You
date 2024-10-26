public class VIVI : Agent
{
    protected override void Awake()
    {
        base.Awake();
        
        _stateMachine.Initalize(StateType.You, this);
    }
}