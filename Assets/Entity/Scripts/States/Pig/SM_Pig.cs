using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Pig : StateMachineBase
{
    public S_PigWander wander = new();
    public S_PigIdle idle = new();

    private void Start()
    {
        currentState = wander;
        currentState.Enter(this);
    }

    private void Update()
    {
        currentState.Update(this);
    }
}
