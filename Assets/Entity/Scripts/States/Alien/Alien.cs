using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    AlienStateBase currentState;
    public AlienMove alienMove = new();
    public AlienCharge alienCharge = new();
    public AlienBeam alienBeam = new();
    public AlienRapidFire alienRapidFire = new();
    public AlienKick alienKick = new();
    // Start is called before the first frame update
    void Start()
    {
        currentState = alienMove;
        currentState.Enter(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public virtual void SwitchState(AlienStateBase state)
    {
        currentState.Exit(this);
        currentState = state;
        currentState.Enter(this);
    }
}
