using UnityEngine;

public class AttackingPlayerBruteState : BruteState
{
    public override void Enter(StateMachine state_machine)
    {
        brute.attack_timer = brute.wind_up;
    }
    public override void Exit(StateMachine state_machine)
    {

    }
    public override void StateUpdate(StateMachine state_machine)
    {

    }
    public override void StateFixedUpdate(StateMachine state_machine)
    {
        if(brute.attacking) //Attack, activate hitbox, countdown timer, set hitbox position, and once timer ends, stop attacking
        {
            brute.punch_hitbox.SetActive(true);
            brute.attack_timer -= Time.fixedDeltaTime;
            brute.punch_hitbox.transform.localPosition = new Vector3(brute.hitbox_position, 0f, 0f);
            if(brute.attack_timer < 0) //This IS the change states region, as the brute sits still as they punch, and once the punch ends, go back to the original seeing the player state
            {
                brute.attacking = false;
                brute.punch_hitbox.SetActive(false);
                state_machine.TransitionToState(state_machine.states[1]);
            }
        }
        else
        {
            brute.attack_timer -= Time.fixedDeltaTime;
            if(brute.attack_timer < 0)
            {
                brute.attacking = true;
                brute.attack_timer = brute.attack_duration;
            }
        }
    }
}
