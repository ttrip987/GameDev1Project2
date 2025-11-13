using UnityEngine;

public class IdlePlayerState : PlayerState
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
        if(player.move_dir != 0) //Transition to Walk State
        {
            state_machine.TransitionToState(state_machine.states[1]);
            return;
        }
        if(player.jump && player.controller.isGrounded) //Transition to Air State
        {
            state_machine.TransitionToState(state_machine.states[2]);
        }
        #endregion
    }
}
