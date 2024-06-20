using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMapCreator : MonoBehaviour
{
    [SerializeField] MapEnvironment env;
    // Start is called before the first frame update
    void Start()
    {
        env._objectAlgo.CreateMap(env, 0);
        MapManager.PlaceStair(0);
    }
}
