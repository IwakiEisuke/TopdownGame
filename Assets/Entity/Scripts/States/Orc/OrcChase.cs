using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcChase : StateBase
{
    Vector2 dir;
    Rigidbody2D rb;
    Orc orc;

    float missTime;

    public override void Enter<T>(T obj)
    {
        Debug.Log("í«ê’äJén");
        rb = obj.GetComponent<Rigidbody2D>();
        orc = obj as Orc;

        orc.viewAngle = 360;
    }

    public override void Exit<T>(T obj)
    {
    }

    public override void Update<T>(T obj)
    {
        orc.angle += Random.Range(-5f, 5f);
        var angleBound = 5f;

        var toPlayerVector = PlayerController.Transform.position - obj.transform.position;
        var toPlayerAngle = Mathf.Atan2(toPlayerVector.y, toPlayerVector.x) * Mathf.Rad2Deg;

        orc.angle = Mathf.Clamp(orc.angle, toPlayerAngle - angleBound, toPlayerAngle + angleBound);
        dir = Quaternion.Euler(0, 0, orc.angle) * Vector2.right;
        rb.velocity = dir * 2f;

        if (!orc.IsInFieldOfView(PlayerController.Transform.position))
        {
            missTime += Time.deltaTime;
            if (missTime > 3)
            {
                orc.SwitchState(orc.patrol);
            }
        }
        else
        {
            missTime = 0;
        }
    }
}
