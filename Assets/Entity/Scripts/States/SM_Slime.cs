using UnityEngine;

public class SM_Slime : MonoBehaviour
{
    protected S_SlimeBase currentState;
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

    public void SwitchState(S_SlimeBase state)
    {
        currentState.Exit(this);
        currentState = state;
        currentState.Enter(this);
    }
}
