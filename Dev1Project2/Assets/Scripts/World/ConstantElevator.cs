using System.Security.Cryptography;
using UnityEngine;

public class ConstantElevator : MonoBehaviour
{

    //Component References
    private Rigidbody rb;
    private Player player;

    //Elevator Movement
    public float elevator_speed = 5f;

    //Elevator Positions
    public Vector3 elevator_start;
    public Vector3 elevator_end;
    public bool forward_direction = true;
    public Vector3 change_in_pos;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        elevator_start = transform.position;
        elevator_end = transform.Find("Endpoint").transform.position; //hard-coded elevator stopping point
    }

    
    void FixedUpdate()
    {
        Vector3 target = forward_direction ? elevator_end : elevator_start;

        Vector3 newPos = Vector3.MoveTowards(transform.position, target, elevator_speed * Time.fixedDeltaTime);
        if (player != null) 
        {
            change_in_pos = newPos - transform.position;
            player.MoveWithElevator(change_in_pos); 
        }
        rb.MovePosition(newPos);
      
        if(Vector3.Distance(transform.position, target) < 0.01f)
        {
            forward_direction = !forward_direction;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = other.GetComponent<Player>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        { 
            player = null;
        }
    }
}
