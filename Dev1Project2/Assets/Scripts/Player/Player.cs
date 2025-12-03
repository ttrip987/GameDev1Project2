using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Component References
    [HideInInspector] public CharacterController controller;
    public MeshRenderer rend;
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
    public ColliderCheck ceiling_check;
    private InputActionMap player_map;

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
    public GameObject HealthImageFull;
    public GameObject HealthImageHitOne;
    public GameObject HealthImageHitTwo;
    public GameObject HealthImageHitThree;

    //Invulnerability
    public bool invulnerability = false;
    public float invulnerability_timer = 1f;
    private Color last_color;

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
        if(invulnerability)
        {
            rend.material.color = new Color(135, 206, 235); //Set invulnerability color
            invulnerability_timer -= Time.fixedDeltaTime; //Start invulnerable timer
            if (invulnerability_timer <= 0f)  
            {
                invulnerability = false; //Reset invulnerability and timer, and reset the color of the model
                invulnerability_timer = 1f;
                rend.material.color = last_color;
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

    //when the player takes damage
    public void TakeDamage()
    {
        if (isDead) return;

        if(!invulnerability)
        {
            currentHits++;
            Debug.Log("Player hit! (" + currentHits + "/" + maxHits + ")");
            invulnerability = true;
            last_color = rend.material.color;

            UpdateHeartUI();

            if (currentHits >= maxHits)
            {
                Die();
            }
        }
        
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("PLAYER IS DEAD");

        // Disable movement + attacks
        move_dir = 0;
        moveAction.Disable();
        jumpAction.Disable();
        attackAction.Disable();
        sprintAction.Disable();

    }

    private void UpdateHeartUI()
    {
        if (currentHits == 0)
        {
            HealthImageFull.SetActive(true);
            HealthImageHitOne.SetActive(false);
            HealthImageHitTwo.SetActive(false);
            HealthImageHitThree.SetActive(false);
        }

        if (currentHits == 1)
        {
            HealthImageFull.SetActive(false);
            HealthImageHitOne.SetActive(true);
            HealthImageHitTwo.SetActive(false);
            HealthImageHitThree.SetActive(false);
        }

        if (currentHits == 2)
        {
            HealthImageFull.SetActive(false);
            HealthImageHitOne.SetActive(false);
            HealthImageHitTwo.SetActive(true);
            HealthImageHitThree.SetActive(false);
        }

        if (currentHits == 3)
        {
            HealthImageFull.SetActive(false);
            HealthImageHitOne.SetActive(false);
            HealthImageHitTwo.SetActive(false);
            HealthImageHitThree.SetActive(true);
        }

    }

    public void MoveWithElevator(Vector3 elevator_movement)
    {
        controller.Move(elevator_movement);
    }

    public bool Heal() //Heals the player, boolean stands for whether it actually healed or not
    {
        if(currentHits > 0)
        {
            currentHits--;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(!hit.collider.GetComponent<Enemy>().isDead)
            {
                TakeDamage();
            }
        }
    }
}
