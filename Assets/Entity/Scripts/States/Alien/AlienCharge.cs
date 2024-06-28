using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlienCharge : AlienStateBase
{
    float stateDuration, lazerCastTime;
    LineRenderer lazerPointer;

    public override void Enter(Alien alien)
    {
        Debug.Log("EnterCharge");
        stateDuration = 1f;
        lazerCastTime = 0.02f;
        lazerPointer = alien.lazerPointer;
        lazerPointer.startColor = Color.red * new Color(1, 1, 1, 0.6f);
        lazerPointer.endColor = Color.red * new Color(1, 1, 1, 0.2f);
        lazerPointer.startWidth = 0.065f;
        lazerPointer.endWidth = 0.02f;
        lazerPointer.enabled = true;
    }

    public override void Exit(Alien alien)
    {

    }

    public override void UpdateState(Alien alien)
    {
        stateDuration -= Time.deltaTime;
        lazerCastTime -= Time.deltaTime;
        var lazerOrigin = lazerPointer.transform.position;

        var hits = Physics2D.RaycastAll(lazerOrigin, PlayerController.Transform.position - alien.transform.position, 30, LayerMask.GetMask(Layer.Entity, Layer.Object));

        if (hits.Length >= 3)
        {
            var lazer = new Vector3[]
            {
            lazerOrigin,
            Vector3.Lerp(lazerOrigin, hits[2].point, 0.5f),
            Vector3.Lerp(lazerOrigin, hits[2].point, 0.5f),
            };

            if (lazerCastTime < 0)
            {
                lazer[2] = hits[2].point;
            }

            lazerPointer.SetPositions(lazer);
        }

        //lazerPointer.SetPosition(0, lazerOrigin);
        //lazerPointer.SetPosition(1, hits[2].point);

        if (stateDuration < 0)
        {
            alien.SwitchState(alien.alienBeam);
        }
    }
}
