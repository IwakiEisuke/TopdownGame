using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlienCharge : AlienStateBase
{
    float stateDuration, laserCastTime;
    LineRenderer laserPointer;

    public override void Enter(Alien alien)
    {
        Debug.Log("EnterCharge");
        stateDuration = 1f;
        laserCastTime = 0.02f;
        laserPointer = alien.laserPointer;
        laserPointer.startColor = Color.red * new Color(1, 1, 1, 0.6f);
        laserPointer.endColor = Color.red * new Color(1, 1, 1, 0.2f);
        laserPointer.startWidth = 0.065f;
        laserPointer.endWidth = 0.02f;
        laserPointer.enabled = true;

        alien.chargeSE.Play();
    }

    public override void Exit(Alien alien)
    {

    }

    public override void UpdateState(Alien alien)
    {
        stateDuration -= Time.deltaTime;
        laserCastTime -= Time.deltaTime;
        var laserOrigin = laserPointer.transform.position;

        var hits = Physics2D.RaycastAll(laserOrigin, PlayerController.Transform.position - alien.transform.position, 30, LayerMask.GetMask(Layer.Entity, Layer.Object));

        if (hits.Length >= 3)
        {
            var laser = new Vector3[]
            {
            laserOrigin,
            Vector3.Lerp(laserOrigin, hits[2].point, 0.5f),
            Vector3.Lerp(laserOrigin, hits[2].point, 0.5f),
            };

            if (laserCastTime < 0)
            {
                laser[2] = hits[2].point;
            }

            laserPointer.SetPositions(laser);
        }

        //laserPointer.SetPosition(0, laserOrigin);
        //laserPointer.SetPosition(1, hits[2].point);

        if (stateDuration < 0)
        {
            alien.SwitchState(alien.alienBeam);
        }
    }
}
