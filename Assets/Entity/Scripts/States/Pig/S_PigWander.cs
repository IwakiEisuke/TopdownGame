using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class S_PigWander : StateBase
{
    float enterTime, addTime, angle;
    Vector2 dir;
    Rigidbody2D rb;
    SM_Pig pig;
    public override void Enter<T>(T obj)
    {
        angle = Random.Range(0, 360);
        enterTime = Time.time;
        addTime = Random.Range(3f, 5f);

        pig = obj as SM_Pig;
        rb = pig.gameObject.GetComponent<Rigidbody2D>();
    }

    public override void Update<T>(T obj)
    {
        angle += Random.Range(-5f, 5f);
        dir = Quaternion.Euler(0, 0, angle) * Vector2.right;
        rb.velocity = dir;

        if (Time.time > enterTime + addTime)
        {
            pig.SwitchState(pig.idle);
        }
    }

    public override void Exit<T>(T obj)
    {
        rb.velocity = Vector2.zero;
    }
}
