using UnityEngine;

public class SprintPlayerState : PlayerState
{
    public override void Enter(StateMachine state_machine)
    {
        player.rend.material.color = Color.red;

    }

    public override void Exit(StateMachine state_machine)
    {

    }

    public override void StateUpdate(StateMachine state_machine)
    {
        
    }

    public override void StateFixedUpdate(StateMachine state_machine)
    {
        #region Change States

        #endregion
    }
}
