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
    public float jump_impulse = 10f;
    public float gravity = 15f;
    [HideInInspector] public bool jump;
    [HideInInspector] public float vertical_velocity = 0;

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
    }

    void Update()
    {
        //Updates the speed variable on sprinting
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
        Debug.Log("Attack");
    }

    private void Sprinting(InputAction.CallbackContext context)
    {
        sprint = context.ReadValue<float>() > 0;
    }

    private void Sliding(InputAction.CallbackContext context)
    {
        Debug.Log("Slide");
    }

}
