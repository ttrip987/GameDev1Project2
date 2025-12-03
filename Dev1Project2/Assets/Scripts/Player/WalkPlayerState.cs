using System.Runtime.CompilerServices;
using UnityEngine;

public class WalkPlayerState : PlayerState
{
    public override void Enter(StateMachine state_machine)
    {
        player.rend.material.color = Color.green;
        player.hit_max_fall = false;
    }

    public override void Exit(StateMachine state_machine)
    {

    }

    public override void StateUpdate(StateMachine state_machine)
    {
        
    }

    public override void StateFixedUpdate(StateMachine state_machine)
    {
        if (player.controller.isGrounded && player.vertical_velocity <= 0)
        {
            player.vertical_velocity = -2f;
        }
        //Take the player speed based on if they're sprinting or not
        float speed = (player.sprint) ? player.sprint_speed : player.standard_speed;
        //Construct the horizontal velocity
        float velocity = player.move_dir * speed;
        //Move the player
        player.controller.Move(new Vector3(velocity, player.vertical_velocity, 0) * Time.fixedDeltaTime);

        #region Change States
        if(player.move_dir == 0) //Transition to idle state
        {
            state_machine.TransitionToState(state_machine.states[0]);
        }
        if (player.jump && player.release_jump && player.controller.isGrounded) //Transition to air state by jumping
        {
            player.vertical_velocity = player.jump_impulse;
            player.initial_y = player.transform.position.y;
            player.max_y = player.initial_y + 0.5f; //Sets a minimum jump height at 0.5 units
            player.hit_max_y = false;
            player.hit_max_fall = false;
            player.release_jump = false;
            state_machine.TransitionToState(state_machine.states[2]);
        }
        if(!player.controller.isGrounded) //Transition to air state by falling
        {
            player.vertical_velocity = 0;
            player.hit_max_y = true;
            player.hit_max_fall = false;

            state_machine.TransitionToState(state_machine.states[2]);
        }
        #endregion
    }
}
