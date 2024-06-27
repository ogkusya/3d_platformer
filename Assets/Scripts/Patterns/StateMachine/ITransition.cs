using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransition
{
    State StateTo { get; }
    ICondition Condition { get; }
    void OnStateEntered();
    void OnStateExited();
}
