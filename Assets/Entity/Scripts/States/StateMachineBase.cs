using UnityEngine;
using UnityEngine.Windows;

public abstract class StateMachineBase : MonoBehaviour
{
    protected StateBase currentState;
    public virtual void SwitchState<T>(T state)
    {
        StateBase stateBase = state as StateBase;
        currentState.Exit(this);
        currentState = stateBase;
        currentState.Enter(this);
    }
}
