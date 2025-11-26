using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Component References
    [HideInInspector] public CharacterController controller;
    [HideInInspector] public MeshRenderer rend;
    public InputActionAsset input;
    public Camera cam;

    //Input
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;
    private InputAction sprintAction;
    private InputAction slideAction;


    //Horizontal Movement
    [HideInInspector] public float standard_speed = 5f;
    [HideInInspector] public float sprint_speed = 8f;
    [HideInInspector] public float move_dir = 0f;
    [HideInInspector] public bool sprint;

    //Vertical Movement
    public float initial_y = 0f;
    public float max_y = 0f;
    public bool hit_max_y = false;
    public bool hit_max_fall = false;
    public float jump_impulse = 10f;
    public float fall_speed = -15f;
    public bool jump;
    public bool release_jump = true;
    public float vertical_velocity = 0;
    public CeilingCheck ceiling_check;

    //Attacking
    public GameObject hitbox;
    private float attack_time = 0f;
    private float attack_duration = 0.5f;
    private float hitbox_distance = 1.01f;
    private float hitbox_position = 0f;

    //Health
    public int maxHits = 3;   // Player can take 3 hits
    private int currentHits = 0;
    public bool isDead = false;
    public UnityEngine.UI.Image[] heartImages;  

    private void OnEnable()
    {
        //Set the input action map and enable it
        var player_map = input.FindActionMap("Player");
        player_map.Enable();


        //Set all actions from the input action mpa and enable them all
        moveAction = player_map.FindAction("Move");
        moveAction.Enable();
        jumpAction = player_map.FindAction("Jump");
        jumpAction.Enable();
        attackAction = player_map.FindAction("Attack");
        attackAction.Enable();
        sprintAction = player_map.FindAction("Sprint");
        sprintAction.Enable();
        slideAction = player_map.FindAction("Slide");
        //slideAction.Enable(); Don't enable until the player gets the powerup :D

        //Bind the actions (and their activation/deactivation) to associated functions for input reading
        moveAction.performed += Moving;
        moveAction.canceled += Moving;
        jumpAction.performed += Jumping;
        jumpAction.canceled += Jumping;
        sprintAction.performed += Sprinting;
        sprintAction.canceled += Sprinting;
        attackAction.performed += Attacking;
        slideAction.performed += Sliding;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        rend = GetComponent<MeshRenderer>();
        hitbox.SetActive(false);
    }

    void Update()
    {
        if (!jump) { release_jump = true; } //Checks for if the player let go of the button to jump again
        if (!release_jump) //Builds up the max height for jump sensitivity (quick press jump button makes you jump lower)
        {
            if (max_y <= initial_y + 2f)
            {
                max_y += 0.1f;
            }
            else //Max jump height is 2 units
            {
                max_y = initial_y + 2f;
            }
        }

        if(move_dir < 0) //Sets the hitbox_position based on the player facing left or right
        {
            hitbox_position = -hitbox_distance;
        }
        else if(move_dir > 0)
        {
            hitbox_position = hitbox_distance;
        }
    }

    void FixedUpdate()
    {
        if (attack_time > 0f) //Track the attack duration in fixed update
        {
            attack_time -= Time.fixedDeltaTime;
            if(attack_time <= 0f)
            {
                attack_time = 0f;
                hitbox.SetActive(false);
            }
        }
    }

    private void Moving(InputAction.CallbackContext context)
    {
        move_dir = context.ReadValue<float>();
    }

    private void Jumping(InputAction.CallbackContext context)
    {
        jump = context.ReadValue<float>() > 0;
    }

    private void Attacking(InputAction.CallbackContext context)
    {
        Attack(); //Attack
    }

    private void Sprinting(InputAction.CallbackContext context)
    {
        sprint = context.ReadValue<float>() > 0;
    }

    private void Sliding(InputAction.CallbackContext context)
    {
        Debug.Log("Slide");
    }

    public void Attack() //Sets the hitbox as active, in the direction the player is facing, and starts the attack timer
    {
        if(attack_time == 0f)
        {
            hitbox.SetActive(true);
            hitbox.transform.localPosition = new Vector3(hitbox_position, 0, 0);
            attack_time = attack_duration;
        }
    }

    //when enemy takes damage
    public void TakeDamage()
    {
        if (isDead) return;

        currentHits++;
        Debug.Log("Player hit! (" + currentHits + "/" + maxHits + ")");

        UpdateHeartUI();

        if (currentHits >= maxHits)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("PLAYER IS DEAD");

        // Disable movement + attacks
        move_dir = 0;
        sprint = false;
        jump = false;

        Destroy(gameObject, 1f);
    }

    private void UpdateHeartUI()
    {
        int heartsLeft = maxHits - currentHits;

        for (int i = 0; i < heartImages.Length; i++)
        {
            // If the heart index is >= hearts left, hide it.
            heartImages[i].enabled = i < heartsLeft;
        }
    }

}
