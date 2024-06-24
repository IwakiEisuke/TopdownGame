using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SlimeIdle : StateBase
{
    float enterTime, addTime;
    SM_Slime slime;
    public override void Enter<T>(T obj)
    {
        enterTime = Time.time;
        addTime = 1.5f;
        slime = obj as SM_Slime;
    }

    public override void Exit<T>(T obj)
    {

    }

    public override void Update<T>(T obj)
    {
        if (Time.time > enterTime + addTime)
        {
            slime.SwitchState(slime.wander);
        }
    }
}
