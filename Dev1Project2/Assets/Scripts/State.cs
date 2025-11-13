using Unity.VisualScripting;
using UnityEngine;

public class State : MonoBehaviour
{
    public virtual void Enter(StateMachine state_machine) { }
    public virtual void Exit(StateMachine state_machine) { }
    public virtual void StateUpdate(StateMachine state_machine) { }
    public virtual void StateFixedUpdate(StateMachine state_machine) { }

}
