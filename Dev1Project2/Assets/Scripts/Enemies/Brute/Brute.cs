using UnityEngine;

public class Brute : Enemy
{
    //Movement
    public float pivot_position = 0f;
    public float move_dir = -1f; // -1 for left, 1 for right
    public float left_max = 0f;
    public float right_max = 0f;
    public bool start_moving = true;
    public float start_moving_timer_max = 2f;
    public float start_moving_timer = 0f;

    //Attacking
    public float sees_player_delay = 0.2f; //short delay once the brute sees the player before they charge at them
    public GameObject punch_hitbox;
    public Vision attack_range;
    public bool attacking = false;
    public float attack_timer;
    public float wind_up = 0.5f;
    public float attack_duration = 0.8f;
    public float hitbox_position;
    public float hitbox_distance = 0.85f;

    void Start()
    {
        maxHits = 3; //Brutes can take 3 hits
        move_speed = 2f;
        punch_hitbox.SetActive(false);
    }

    private void Update()
    {
        if (move_dir < 0) //Sets the hitbox_position based on the brute facing left or right
        {
            hitbox_position = -hitbox_distance;
        }
        else if (move_dir > 0)
        {
            hitbox_position = hitbox_distance;
        }
    }
}
