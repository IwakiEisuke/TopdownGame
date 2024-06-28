using System;
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
    public LineRenderer lazerPointer;
    // Start is called before the first frame update
    void Start()
    {
        lazerPointer = GetComponentInChildren<LineRenderer>();
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
        currentState.UpdateState(this);
    }

    public IEnumerator Step(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
