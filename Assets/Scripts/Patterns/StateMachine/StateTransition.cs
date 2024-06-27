using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTransition : ITransition
{
    public State StateTo { get; private set; } //Next state
    public ICondition Condition { get; private set; } //Condition

    public StateTransition(State state, StateCondition stateCondition) //Construct
    {
        StateTo = state;
        Condition = stateCondition;
    }

    public void OnStateEntered() //Condition initializer
    {
        Condition.OnStateEntered();
    }

    public void OnStateExited() //Condition deInitializer
    {
        Condition.OnStateExited();
    }
}
