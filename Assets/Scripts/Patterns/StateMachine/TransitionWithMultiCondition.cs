using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionWithMultiCondition //: ITransition
{
    private State nextState;
    private List<ICondition> condition;

    public State StateTo => nextState;

    public bool IsConditionSuccess => IsConditionsSuccess();

    private bool IsConditionsSuccess()
    {
        foreach (var condition in condition)
        {
            if (condition.IsConditionSuccessed() == false)
            {
                return false;
            }
        }

        return true;
    }

    public TransitionWithMultiCondition(State nextState, List<ICondition> condition)
    {
        this.nextState = nextState;
        this.condition = condition;
    }

    public void OnStateEntered()
    {
        foreach (var condition in condition)
        {
            condition.OnStateEntered();
        }
    }

    public void OnStateExited()
    {
        foreach (var condition in condition)
        {
            condition.OnStateExited();
        }
    }

    public void OnTick()
    {
        foreach (var condition in condition)
        {
            condition.OnTick();
        }
    }
}
