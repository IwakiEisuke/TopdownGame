using UnityEngine;

public class S_SlimeWander : StateBase
{
    float enterTime, addTime, angle;
    Vector2 dir;
    Rigidbody2D rb;
    SM_Slime slime;
    public override void Enter<T>(T obj)
    {
        angle = Random.Range(0, 360);
        enterTime = Time.time;
        addTime = Random.Range(3f, 5f);
        slime = obj as SM_Slime;
        rb = slime.gameObject.GetComponent<Rigidbody2D>();
    }

    public override void Update<T>(T obj)
    {
        angle += Random.Range(-5f, 5f);
        dir = Quaternion.Euler(0, 0, angle) * Vector2.right;
        rb.velocity = dir;

        if (Time.time > enterTime + addTime)
        {
            slime.SwitchState(slime.idle);
        }
    }

    public override void Exit<T>(T obj)
    {
        rb.velocity = Vector2.zero;
    }
}
