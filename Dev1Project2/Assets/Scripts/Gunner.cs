using UnityEngine;

public class Gunner : MonoBehaviour
{
    //Component References
    [HideInInspector] public Rigidbody rb;
    public MeshRenderer rend;
    public Vision vision;

    //Movement
    public float move_speed = 2f;
    public float pivot_position = 0f;
    public float move_dir = -1f; // -1 for left, 1 for right
    public float left_max = 0f;
    public float right_max = 0f;
    public ColliderCheck left_check;
    public ColliderCheck right_check;
    public bool start_moving = true;
    public float start_moving_timer_max = 3f;
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
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<MeshRenderer>();
    }

   
    void Update()
    {
        
    }
}
