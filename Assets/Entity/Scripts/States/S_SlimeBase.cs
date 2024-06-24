using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class S_SlimeBase
{
    public abstract void Enter(SM_Slime obj);
    public abstract void Update(SM_Slime obj);
    public abstract void Exit(SM_Slime obj);
}
