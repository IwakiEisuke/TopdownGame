using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlienBeam : AlienStateBase
{
    float duration;
    public override void Enter(Alien alien)
    {
        //��������v���C���[�Ƀ��C���΂�
        //hits�̌��ʂ͋����̏����Ń\�[�g����Ă���Ahit�̓�2�͎����̃R���C�_�[�Ȃ̂�3�ȓ��Ƀv���C���[�������ꍇ�������Ă��锻��ɂȂ�
        var hits = Physics2D.RaycastAll(alien.transform.position, PlayerController.Transform.position - alien.transform.position, 30, LayerMask.GetMask(Layer.Entity, Layer.Object));
        var rayHitPlayer = hits.Take(3).Any(x => x.collider.CompareTag(Tag.Player));

        foreach (var hit in hits)
        {
            Debug.Log("hit : " + hit.collider.name);
        }

        if (rayHitPlayer)
        {
            PlayerController.TakeDamage(8);
        }

        duration = 1;
    }

    public override void Exit(Alien alien)
    {

    }

    public override void UpdateState(Alien alien)
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            alien.SwitchState(alien.alienMove);
        }
    }
}
