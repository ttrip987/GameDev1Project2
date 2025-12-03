using System.Security.Cryptography;
using UnityEngine;

public class FinalElevator : MonoBehaviour
{

    //Component References
    private Rigidbody rb;
    public Player player;

    //Elevator Movement
    public float elevator_speed = 5f;
    public Vector3 elevator_velocity = Vector3.zero;
    public float elevator_timer = 2f;
    public bool in_elevator = false;
    public bool move_elevator = false;

    //Elevator Positions
    public Vector3 elevator_start;
    public float elevator_end;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        elevator_start = transform.position;
        elevator_end = transform.Find("Endpoint").transform.position.y; //hard-coded elevator stopping point
    }

    
    void FixedUpdate()
    {
        if(move_elevator)
        {
            if(transform.position.y < elevator_end) //If elevator hasn't reached max height
            {
                elevator_velocity = elevator_speed * Vector3.up * Time.fixedDeltaTime; //Set elevator velocity
                Vector3 newPos = transform.position + elevator_velocity;
                player.MoveWithElevator(elevator_velocity); //Move the player + elevator
                rb.MovePosition(newPos);
            }
        }
        else if(in_elevator)
        {
            if(elevator_timer <= 0)
            {
                move_elevator = true;
            }
            else
            {
                elevator_timer -= Time.fixedDeltaTime;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            in_elevator = true;
            player = other.GetComponent<Player>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            in_elevator = false;
            move_elevator = false;
            elevator_timer = 2f;
            if (transform.position.y < elevator_end)
            {
                transform.position = elevator_start;
            }
        }
    }
}
