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
    public float viewRadius = 5; // ����̔��a
    public float viewAngle = 100; // ����̊p�x

    public Transform target; // �^�[�Q�b�g


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

    // �^�[�Q�b�g��������ɂ��邩�ǂ����𔻒肷��֐�
    public bool IsInFieldOfView(Vector3 targetPos)
    {
        Vector3 directionToTarget = (targetPos - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, targetPos);

        // ����������̔��a�ȓ����ǂ���
        if (distanceToTarget < viewRadius)
        {
            // �^�[�Q�b�g�ւ̕���������̊p�x�����ǂ���
            float angleBetween = Vector3.Angle(DirFromAngle(angle), directionToTarget);
            if (angleBetween < viewAngle / 2)
            {
                return true;
            }
        }
        return false;
    }

    // �V�[���r���[�Ŏ���͈͂��������邽�߂̃f�o�b�O�p�֐�
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

    // �p�x��������x�N�g�����v�Z����֐�
    public Vector3 DirFromAngle(float angleInDegrees)
    {
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}
