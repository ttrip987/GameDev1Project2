using UnityEngine;

public class Gunner : Enemy
{
    //Movement
    public float pivot_position = 0f;
    public float move_dir = -1f; // -1 for left, 1 for right
    public float left_max = 0f;
    public float right_max = 0f;
    public bool start_moving = true;
    public float start_moving_timer_max = 2f;
    public float start_moving_timer = 0f;

    //Shooting
    public Vector3 shoot_dir = Vector3.zero;
    public GunnerBullet bullet;
    public float between_burst_timer = 1f;
    public float in_burst_timer = 0.2f;
    public float bullet_timer = 0f;
    public int burst_shots = 3;

    void Start()
    {
        maxHits = 2; //Gunner can take 2 hits
        move_speed = 2f;
    }

}
