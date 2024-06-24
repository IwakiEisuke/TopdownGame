using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SlimeIdle : S_SlimeBase
{
    float enterTime, addTime;
    public override void Enter(SM_Slime slime)
    {
        Debug.Log("Enter Idle");
        enterTime = Time.time;
        addTime = Random.Range(1f, 3f);
    }

    public override void Exit(SM_Slime slime)
    {
        Debug.Log("Exit Idle");
    }

    public override void Update(SM_Slime slime)
    {
        if (Time.time > enterTime + addTime)
        {
            slime.SwitchState(slime.wander);
        }
    }
}
