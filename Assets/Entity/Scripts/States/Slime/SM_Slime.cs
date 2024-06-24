using UnityEngine;

public class SM_Slime : StateMachineBase
{
    public S_SlimeWander wander = new();
    public S_SlimeIdle idle = new();
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
