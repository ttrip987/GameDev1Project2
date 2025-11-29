using UnityEngine;

public class IdleScanGunnerState : GunnerState
{
    public override void Enter(StateMachine state_machine)
    {

        //Sets the initial position of the gunner and the left and right extremes of where they move back and forth
        gunner.pivot_position = gunner.transform.position.x;
        gunner.left_max = gunner.pivot_position - 5f;
        gunner.right_max = gunner.pivot_position + 5f;

        //Starts the delay timer before the gunner starts moving again after losing sight of the player
        gunner.start_moving_timer = gunner.start_moving_timer_max;

    }
    public override void Exit(StateMachine state_machine)
    {
        gunner.start_moving = false;
    }
    public override void StateUpdate(StateMachine state_machine) 
    {
        
    }
    public override void StateFixedUpdate(StateMachine state_machine)
    {
        if(!gunner.start_moving) //Countdown the delay timer before the gunner starts moving again
        {
            gunner.start_moving_timer -= Time.fixedDeltaTime;
            if(gunner.start_moving_timer <= 0)
            {
                gunner.start_moving = true;
            }
        }
        else //Gunner moves around
        {
            float velocity = gunner.move_speed * gunner.move_dir * Time.fixedDeltaTime;
            Vector3 newPos = gunner.transform.position + new Vector3(velocity, 0f, 0f);

            //Clamps the movement to the left and right max, preparing the reversal as the gunner hits the left/right max
            if (newPos.x < gunner.left_max)
            {
                newPos.x = gunner.left_max;
                gunner.move_dir *= -1; //Reverse the move direction and give a 1 second delay to turn around and move the other way
                gunner.start_moving = false;
                gunner.start_moving_timer = 1f;
            }
            else if(gunner.left_check.hit_collider && gunner.move_dir < 0) //If the gunner hits a wall to the left, turn around
            {
                gunner.move_dir *= -1; //Reverse the move direction and give a 1 second delay to turn around and move the other way
                gunner.start_moving = false;
                gunner.start_moving_timer = 1f;
            }
            if (newPos.x > gunner.right_max)
            {
                newPos.x = gunner.right_max;
                gunner.move_dir *= -1; //Reverse the move direction and give a 1 second delay to turn around and move the other way
                gunner.start_moving = false;
                gunner.start_moving_timer = 1f;
            }
            else if(gunner.right_check.hit_collider && gunner.move_dir > 0) //If the gunner hits a wall to the right, turn around
            {
                gunner.move_dir *= -1; //Reverse the move direction and give a 1 second delay to turn around and move the other way
                gunner.start_moving = false;
                gunner.start_moving_timer = 1f;
            }

            gunner.rb.MovePosition(newPos);
        }

        #region Change States
        if(gunner.vision.sees_player) //Change to shooting the player if the player is in view
        {
            state_machine.TransitionToState(state_machine.states[1]);
        }
        #endregion
    }
}
