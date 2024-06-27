using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AlienStateBase
{
    public abstract void Enter(Alien alien);
    public abstract void UpdateState(Alien alien);
    public abstract void Exit(Alien alien);
}
