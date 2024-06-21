using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMapCreator : MonoBehaviour
{
    [SerializeField] MapManager manager;
    // Start is called before the first frame update
    public void MyFunction()
    {
        manager.StartProcess();
    }
}
