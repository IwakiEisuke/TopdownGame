using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_PigIdle : StateBase
{
    float enterTime, addTime;
    SM_Pig slime;
    public override void Enter<T>(T obj)
    {
        enterTime = Time.time;
        addTime = 1.5f;
        slime = obj as SM_Pig;
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
