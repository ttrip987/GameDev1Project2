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
        //Player goes up at the same speed, then smoothly transitions to falling
        if (player.hit_max_y && player.hit_max_fall) //Just falling
        {
            player.vertical_velocity = player.fall_speed;
        }
        else if(player.hit_max_y) //Transition from peak height to falling
        {
            player.vertical_velocity += player.fall_speed * 0.08f;
            if (player.vertical_velocity <= player.fall_speed) { player.hit_max_fall = true; }
        }
        else if (player.transform.position.y > player.max_y && !player.hit_max_y) //Waits until until player reaches max height
        {
            player.hit_max_y = true;
        }

        //Take the player speed based on if they're sprinting or not
        float speed = (player.sprint) ? player.sprint_speed : player.standard_speed;
        //Construct the horizontal velocity
        float h_velocity = player.move_dir * speed;
        //Move the player
        player.controller.Move(new Vector3(h_velocity, player.vertical_velocity, 0) * Time.fixedDeltaTime);

        #region Change States
        if (player.controller.isGrounded) //Change to walk state
        {
            state_machine.TransitionToState(state_machine.states[1]);
        }
        #endregion
    }
}
