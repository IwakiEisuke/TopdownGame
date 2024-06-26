using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : StateMachineBase
{
    public OrcIdle idle = new OrcIdle();
    public OrcPatrol patrol = new OrcPatrol();
    public OrcChase chase = new OrcChase();
    Rigidbody2D rb;
    public float angle;
    public float viewRadius = 5; // 視野の半径
    public float viewAngle = 100; // 視野の角度

    public Transform target; // ターゲット


    // Start is called before the first frame update
    void Start()
    {
        angle = Random.Range(0, 360);
        rb = GetComponent<Rigidbody2D>();
        currentState = patrol;
        currentState.Enter(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Update(this);
    }

    // ターゲットが視野内にいるかどうかを判定する関数
    public bool IsInFieldOfView(Vector3 targetPos)
    {
        Vector3 directionToTarget = (targetPos - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, targetPos);

        // 距離が視野の半径以内かどうか
        if (distanceToTarget < viewRadius)
        {
            // ターゲットへの方向が視野の角度内かどうか
            float angleBetween = Vector3.Angle(DirFromAngle(angle), directionToTarget);
            if (angleBetween < viewAngle / 2)
            {
                return true;
            }
        }
        return false;
    }

    // シーンビューで視野範囲を可視化するためのデバッグ用関数
    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 viewAngleA = DirFromAngle(-viewAngle / 2 + angle);
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2 + angle);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);
    }

    // 角度から方向ベクトルを計算する関数
    public Vector3 DirFromAngle(float angleInDegrees)
    {
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}
