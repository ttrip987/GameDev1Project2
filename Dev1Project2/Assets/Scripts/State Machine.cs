using UnityEngine;

public class StateMachine : MonoBehaviour
{
    //Current state of the state machine
    private State current_state;

    //List of possible states
    public State[] states;

    void Start()
    {
        //Starts at the first state (idle)
        current_state = states[0];

        current_state.Enter(this);
    }

    void Update()
    {
        current_state.StateUpdate(this);
    }

    void FixedUpdate()
    {
        current_state.StateFixedUpdate(this);
    }

    public void TransitionToState(State new_state)
    {
        current_state.Exit(this); //Exit the current state
        current_state = new_state; //Change the current state
        current_state.Enter(this); //Enter the current state
    }


}
