using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcIdle : StateBase
{
    float enterTime, addTime;
    Orc orc;
    public override void Enter<T>(T obj)
    {
        enterTime = Time.time;
        addTime = 0.7f;
        orc = obj as Orc;

        var rb = obj.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    public override void Exit<T>(T obj)
    {

    }

    public override void Update<T>(T obj)
    {
        if (Time.time > enterTime + addTime)
        {
            orc.SwitchState(orc.patrol);
        }

        if (orc.IsInFieldOfView(PlayerController.Transform.position))
        {
            orc.SwitchState(orc.chase);
        }
    }
}
