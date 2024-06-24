using UnityEngine;
using UnityEngine.Windows;

public abstract class StateMachineBase : MonoBehaviour
{
    protected StateBase currentState;
    public virtual void SwitchState<T>(T state) where T : StateBase
    {
        currentState.Exit(this);
        currentState = state;
        currentState.Enter(this);
    }
}
