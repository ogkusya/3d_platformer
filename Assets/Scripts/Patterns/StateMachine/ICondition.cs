using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICondition
{
    bool IsConditionSuccessed();
    void OnStateEntered();
    void OnStateExited();
    void OnTick();
}
