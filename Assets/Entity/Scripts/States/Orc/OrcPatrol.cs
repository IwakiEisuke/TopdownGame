using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public class OrcPatrol : StateBase
{
    float enterTime, addTime;
    Vector2 dir;
    Rigidbody2D rb;
    Orc orc;
    public override void Enter<T>(T obj)
    {
        orc = obj as Orc;
        enterTime = Time.time;
        addTime = Random.Range(5f, 7f);
        orc.viewAngle = 100;

        rb = obj.GetComponent<Rigidbody2D>();
    }

    public override void Exit<T>(T obj)
    {
        
    }

    public override void Update<T>(T obj)
    {
        orc.angle += Random.Range(-9f, 9f);
        dir = Quaternion.Euler(0, 0, orc.angle) * Vector2.right;
        rb.velocity = dir;

        if (Time.time > enterTime + addTime)
        {
            orc.SwitchState(orc.idle);
        }

        if (orc.IsInFieldOfView(PlayerController.Transform.position))
        {
            orc.SwitchState(orc.chase);
        }
    }
}
