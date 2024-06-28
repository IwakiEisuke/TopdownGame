using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlienBeam : AlienStateBase
{
    float duration;
    float firingDuration;
    readonly float firingDurationInit = 0.5f;
    public override void Enter(Alien alien)
    {
        var lazerPointer = alien.lazerPointer;
        //��������v���C���[�Ƀ��C���΂�
        //hits�̌��ʂ͋����̏����Ń\�[�g����Ă���Ahit�̓�2�͎����̃R���C�_�[�Ȃ̂�3�ȓ��Ƀv���C���[�������ꍇ�������Ă��锻��ɂȂ�
        var hits = Physics2D.RaycastAll(lazerPointer.transform.position, PlayerController.Transform.position - alien.transform.position, 30, LayerMask.GetMask(Layer.Entity, Layer.Object));
        var rayHitPlayer = hits.Take(3).Any(x => x.collider.CompareTag(Tag.Player));
        lazerPointer.startColor = Color.blue + Color.green;
        lazerPointer.endColor = Color.blue + Color.green;

        foreach (var hit in hits)
        {
            Debug.Log("hit : " + hit.collider.name);
        }

        if (rayHitPlayer)
        {
            PlayerController.TakeDamage(8);
        }

        duration = 0.8f;
        firingDuration = firingDurationInit;
    }

    public override void Exit(Alien alien)
    {

    }

    public override void UpdateState(Alien alien)
    {
        duration -= Time.deltaTime;
        firingDuration -= Time.deltaTime;

        var t = EaseOutPowCircle((firingDurationInit - firingDuration) / firingDurationInit, 10);
        var lazerPointer = alien.lazerPointer;
        lazerPointer.startWidth = Mathf.Lerp(1f, 0, t);
        lazerPointer.endWidth = Mathf.Lerp(1f, 0, t);

        if (firingDuration < 0)
        {
            alien.lazerPointer.enabled = false;
        }
        if (duration < 0)
        {
            alien.SwitchState(alien.alienMove);
        }
    }

    private float EaseOutPowCircle(float x, float pow)
    {
        //(1, 0)�𒆐S�Ƃ��锼�a1�̉~�̕�������y=f(x)�ł���킵���Ƃ���f(x)��Ԃ�
        return Mathf.Sqrt(1 - Mathf.Pow(1 - x, pow));
    }
}
