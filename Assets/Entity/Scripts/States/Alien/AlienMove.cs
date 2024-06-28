using UnityEngine;

public class AlienMove : AlienStateBase
{
    bool chasePlayer;
    float stateDuration, stepDuration, angleRange;
    readonly float chaseRange = 10;
    readonly float stepTime = 0.4f;
    readonly float accScale = 20;
    Vector2 velocity, acceleration, beforePos;
    Rigidbody2D rb;

    public override void Enter(Alien alien)
    {
        stateDuration = Random.Range(4f, 6f);
        stepDuration = stepTime;
        //距離が一度でもchaseRangeメートル内になると追いかけ続ける
        if (!chasePlayer && Vector2.Distance(alien.transform.position, PlayerController.Transform.position) < chaseRange)
        {
            chasePlayer = true;
        }
        SetAcceleration(alien);
        rb = alien.GetComponent<Rigidbody2D>();
        Debug.Log(Vector2.Distance(alien.transform.position, PlayerController.Transform.position));
    }

    public override void Exit(Alien alien)
    {

    }

    public override void UpdateState(Alien alien)
    {
        rb.velocity = velocity;
        velocity += acceleration * Time.deltaTime;

        if (stepDuration < 0)
        {
            rb.velocity = Vector2.zero;
            velocity = Vector2.zero;

            if (stateDuration < 0 && chasePlayer)
            {
                alien.SwitchState(alien.alienCharge);
                return;
            }
            else
            {
                AudioSource.PlayClipAtPoint(alien.StepsSE[Random.Range(0, alien.StepsSE.Length)], alien.transform.position);
            }

            //距離が一度でもchaseRangeメートル内になると追いかけ続ける
            if (!chasePlayer && Vector2.Distance(alien.transform.position, PlayerController.Transform.position) < chaseRange)
            {
                chasePlayer = true;
            }

            var dir = PlayerController.Transform.position - alien.transform.position;
            var dirAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var hit = Physics2D.Raycast(alien.transform.position, dir, 1, LayerMask.GetMask(Layer.Object));

            if (Vector2.Distance(beforePos, alien.transform.position) < 0.5f || hit)
            {
                var angle = 0f;
                if (Mathf.Round(Random.value) > 0.5)
                {
                    angle = dirAngle + 90;
                }
                else
                {
                    angle = dirAngle - 90;
                }
                acceleration = Quaternion.Euler(0, 0, angle) * Vector2.right * accScale;
            }
            else
            {
                angleRange = 20;
                SetAcceleration(alien);
            }

            stepDuration = stepTime;
            beforePos = alien.transform.position;
        }

        stepDuration -= Time.deltaTime;
        stateDuration -= Time.deltaTime;
    }

    private void SetAcceleration(Alien alien)
    {
        //移動ベクトルの角度を決定
        float angle = 0;
        if (chasePlayer)
        {
            var dir = PlayerController.Transform.position - alien.transform.position;
            var dirAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle = Random.Range(dirAngle - angleRange / 2, dirAngle + angleRange / 2);
        }
        else
        {
            angle = Random.value * 360;
        }

        acceleration = Quaternion.Euler(0, 0, angle) * Vector2.right * accScale;
    }
}
