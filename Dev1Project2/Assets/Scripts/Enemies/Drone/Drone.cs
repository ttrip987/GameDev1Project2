using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Drone : Enemy //Drones only fly around and thats it, so no states needed, they have one constant state at all times
{
    //Flight Movement Direction
    public float direction_angle_degrees;
    public float direction_distance;
    public float direction_angle_radians;
    public Vector3 direction;

    //Movement Timing
    public bool calculate_move = false;
    public bool moving = false;
    public float moving_timer_max = 1.5f;
    public float moving_timer = 0f;

    //Movement
    public Vector3 velocity;
    public float moved_distance = 0f;


    private void Start()
    {
        maxHits = 1;
        move_speed = 5f;
    }

    private void FixedUpdate()
    {
        if(!calculate_move)
        {
            moving_timer -= Time.fixedDeltaTime;
            if(moving_timer < 0)
            {
                calculate_move = true;
            }
        }
        else if (!moving)
        {
            direction_angle_degrees = Random.Range(0, 361); //Randomize the direction (in degrees)
            direction_angle_radians = direction_angle_degrees * Mathf.Deg2Rad;

            direction = new Vector3(Mathf.Cos(direction_angle_radians), Mathf.Sin(direction_angle_radians), 0f); //Derive a direction in Vector3 from the randomness

            direction_distance = Random.Range(1, 5);

            if(!Physics.Raycast(transform.position, direction, out RaycastHit hitInfo, direction_distance))
            {
                moving = true;
            }
        }
        else if(moving)
        {
            velocity = move_speed * direction * Time.fixedDeltaTime;
            Vector3 newPos = transform.position + velocity;

            moved_distance += Mathf.Sqrt(Mathf.Pow(velocity.x, 2) + Mathf.Pow(velocity.y, 2)); //Use pythagorean formula for current distance moved

            if (moved_distance >= direction_distance)
            {
                moving = false;
                calculate_move = false;
                moving_timer = moving_timer_max;
                moved_distance = 0f;
            }
            else
            {
                rb.MovePosition(newPos);
            }
        }
    }
}
