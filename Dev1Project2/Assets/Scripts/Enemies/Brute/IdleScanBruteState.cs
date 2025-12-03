using UnityEngine;
using UnityEngine.Analytics;

public class IdleScanBruteState : BruteState
{
    public override void Enter(StateMachine state_machine)
    {
        //Sets the initial position of the brute and the left and right extremes of where they move back and forth
        brute.pivot_position = brute.transform.position.x;
        brute.left_max = brute.pivot_position - 5f;
        brute.right_max = brute.pivot_position + 5f;

        //Starts the delay timer before the brute starts moving again after losing sight of the player
        brute.start_moving_timer = brute.start_moving_timer_max;
    }
    public override void Exit(StateMachine state_machine)
    {
        brute.start_moving = false;
    }
    public override void StateUpdate(StateMachine state_machine)
    {

    }
    public override void StateFixedUpdate(StateMachine state_machine)
    {

        if (!brute.start_moving) //Countdown the delay timer before the brute starts moving again
        {
            brute.start_moving_timer -= Time.fixedDeltaTime;
            if (brute.start_moving_timer <= 0)
            {
                brute.start_moving = true;
            }
        }
        else //brute moves around
        {
            float velocity = brute.move_speed * brute.move_dir * Time.fixedDeltaTime;
            Vector3 newPos = brute.transform.position + new Vector3(velocity, 0f, 0f);

            //Clamps the movement to the left and right max, preparing the reversal as the brute hits the left/right max
            if (newPos.x < brute.left_max)
            {
                newPos.x = brute.left_max;
                brute.move_dir *= -1; //Reverse the move direction and give a 1 second delay to turn around and move the other way
                brute.start_moving = false;
                brute.start_moving_timer = 1f;
            }
            else if (brute.left_check.hit_collider && brute.move_dir < 0) //If the brute hits a wall to the left, turn around
            {
                brute.move_dir *= -1; //Reverse the move direction and give a 1 second delay to turn around and move the other way
                brute.start_moving = false;
                brute.start_moving_timer = 1f;
            }
            if (newPos.x > brute.right_max)
            {
                newPos.x = brute.right_max;
                brute.move_dir *= -1; //Reverse the move direction and give a 1 second delay to turn around and move the other way
                brute.start_moving = false;
                brute.start_moving_timer = 1f;
            }
            else if (brute.right_check.hit_collider && brute.move_dir > 0) //If the brute hits a wall to the right, turn around
            {
                brute.move_dir *= -1; //Reverse the move direction and give a 1 second delay to turn around and move the other way
                brute.start_moving = false;
                brute.start_moving_timer = 1f;
            }

            brute.rb.MovePosition(newPos);
        }

        #region Change States
        if (brute.vision.sees_player) //Change to shooting the player if the player is in view
        {
            state_machine.TransitionToState(state_machine.states[1]);
        }
        #endregion
    }


}
