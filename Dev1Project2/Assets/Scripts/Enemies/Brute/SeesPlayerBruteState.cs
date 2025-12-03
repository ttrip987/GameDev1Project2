using UnityEngine;
using UnityEngine.Analytics;

public class SeesPlayerBruteState : BruteState
{
    private GameObject player;

    public override void Enter(StateMachine state_machine) 
    {
        player = GameObject.Find("Player");
        brute.start_moving_timer = brute.sees_player_delay;
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
        else
        {
            float horiz_dist_to_player = player.transform.position.x - brute.transform.position.x;
            if (horiz_dist_to_player < 0) { brute.move_dir = -1; } else { brute.move_dir = 1; }

            float velocity = brute.move_speed * brute.move_dir * Time.fixedDeltaTime;
            Vector3 newPos = brute.transform.position + new Vector3(velocity, 0f, 0f);

            brute.rb.MovePosition(newPos);
        }


        #region Change States
        if (!brute.vision.sees_player) //Change back to idle scan if the player is no longer in view
        {
            state_machine.TransitionToState(state_machine.states[0]);
        }
        if(brute.attack_range.sees_player) //If the player is within range, launch an attack
        {
            state_machine.TransitionToState(state_machine.states[2]);
        }
        #endregion

    }
}
