using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    public abstract void Enter<T>(T obj) where T : StateMachineBase;
    public abstract void Update<T>(T obj) where T : StateMachineBase;
    public abstract void Exit<T>(T obj) where T : StateMachineBase;
}
