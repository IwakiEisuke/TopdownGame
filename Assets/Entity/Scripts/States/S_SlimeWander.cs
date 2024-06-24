using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_SlimeWander : S_SlimeBase
{
    float enterTime, angle;
    Vector2 dir;
    public override void Enter(SM_Slime slime)
    {
        Debug.Log("Enter");
        angle = Random.Range(0, 360);
        enterTime = Time.time;
    }

    public override void Update(SM_Slime slime)
    {
        angle += Random.Range(-5f, 5f);
        dir = Quaternion.Euler(0, 0, angle) * Vector2.right;
        slime.transform.Translate(dir * Time.deltaTime);

        if (Time.time > enterTime + 3)
        {
            slime.SwitchState(slime.idle);
        }
    }
    public override void Exit(SM_Slime slime)
    {
        Debug.Log("Exit");
    }
}
