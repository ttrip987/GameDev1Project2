using UnityEngine;

public class SeePlayerGunnerState : GunnerState
{
    private GameObject player;

    public override void Enter(StateMachine state_machine)
    {
        //Sets the player location when they are seen and starts the bullet timer
        player = GameObject.Find("Player");
        gunner.bullet_timer = gunner.between_burst_timer;
    }
    public override void Exit(StateMachine state_machine)
    {
        
    }
    public override void StateUpdate(StateMachine state_machine)
    {

    }
    public override void StateFixedUpdate(StateMachine state_machine)
    {
        if (gunner.bullet_timer > 0f) //Count down the timer
        {
            gunner.bullet_timer -= Time.fixedDeltaTime;
        }
        else
        {
            //Set the shoot direction of the gunner
            gunner.shoot_dir = new Vector3(player.transform.position.x - gunner.transform.position.x, player.transform.position.y - gunner.transform.position.y, 0);
            gunner.shoot_dir = gunner.shoot_dir.normalized;

            //Shoot a bullet
            GunnerBullet new_bullet = Instantiate(gunner.bullet);
            new_bullet.SetBullet(gunner.transform.position, gunner.shoot_dir);
            gunner.burst_shots -= 1;

            //Check where we are in the burst to determine time to next shot
            if(gunner.burst_shots > 0) //Mid-burst
            {
                gunner.bullet_timer = gunner.in_burst_timer;
            }
            else //End of the burst, burst_shots == 0
            {
                gunner.bullet_timer = gunner.between_burst_timer;
                gunner.burst_shots = 3; //Reset burst shots
            }

        }


        #region Change States
        if(!gunner.vision.sees_player) //Change back to idle scan if the player is no longer in view
        {
            state_machine.TransitionToState(state_machine.states[0]);
        }
        #endregion
    }
}
