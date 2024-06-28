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
        AudioSource.PlayClipAtPoint(alien.beamSE, alien.transform.position);

        var laserPointer = alien.laserPointer;
        //自分からプレイヤーにレイを飛ばす
        //hitsの結果は距離の昇順でソートされており、hitの内2つは自分のコライダーなので3つ以内にプレイヤーがいた場合当たっている判定になる
        var hits = Physics2D.RaycastAll(laserPointer.transform.position, PlayerController.Transform.position - alien.transform.position, 30, LayerMask.GetMask(Layer.Entity, Layer.Object));
        var rayHitPlayer = hits.Take(3).Any(x => x.collider.CompareTag(Tag.Player));
        laserPointer.startColor = Color.blue + Color.green;
        laserPointer.endColor = Color.blue + Color.green;

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
        var laserPointer = alien.laserPointer;
        laserPointer.startWidth = Mathf.Lerp(1f, 0, t);
        laserPointer.endWidth = Mathf.Lerp(1f, 0, t);

        if (firingDuration < 0)
        {
            alien.laserPointer.enabled = false;
        }
        if (duration < 0)
        {
            alien.SwitchState(alien.alienMove);
        }
    }

    private float EaseOutPowCircle(float x, float pow)
    {
        //(1, 0)を中心とする半径1の円の方程式をy=f(x)であらわしたときのf(x)を返す
        return Mathf.Sqrt(1 - Mathf.Pow(1 - x, pow));
    }
}
