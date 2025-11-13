using UnityEngine;

public class AirPlayerState : PlayerState
{
    public override void Enter(StateMachine state_machine)
    {
        player.rend.material.color = Color.yellow;
    }

    public override void Exit(StateMachine state_machine)
    {

    }

    public override void StateUpdate(StateMachine state_machine)
    {
        
    }

    public override void StateFixedUpdate(StateMachine state_machine)
    {
        //Take gravity's effect, slowly making the player fall
        player.vertical_velocity -= player.gravity * Time.fixedDeltaTime;

        //Take the player speed based on if they're sprinting or not
        float speed = (player.sprint) ? player.sprint_speed : player.standard_speed;
        //Construct the horizontal velocity
        float h_velocity = player.move_dir * speed;
        //Move the player
        player.controller.Move(new Vector3(h_velocity, player.vertical_velocity, 0) * Time.fixedDeltaTime);

        #region Change States
        if (player.controller.isGrounded)
        {
            state_machine.TransitionToState(state_machine.states[1]);
        }
        #endregion
    }
}
