using System.Linq;
using UnityEngine;

public class AlienCharge : AlienStateBase
{
    float stateDuration;
    public override void Enter(Alien alien)
    {
        Debug.Log("EnterCharge");
        stateDuration = 1f;
    }

    public override void Exit(Alien alien)
    {

    }

    public override void UpdateState(Alien alien)
    {
        stateDuration -= Time.deltaTime;
        Debug.DrawLine(alien.transform.position, alien.transform.position + (PlayerController.Transform.position - alien.transform.position).normalized * 10);

        if (stateDuration < 0)
        {
            alien.SwitchState(alien.alienBeam);
        }
    }
}
