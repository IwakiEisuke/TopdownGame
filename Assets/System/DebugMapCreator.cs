using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMapCreator : MonoBehaviour
{
    [SerializeField] MapManager manager;
    [SerializeField] GameStartManager gameStartManager;
    // Start is called before the first frame update
    public void MyFunction()
    {
        if (MapManager.Maps[0] != null)
        {
            Destroy(MapManager.Maps[0]._groundmap.gameObject);
            Destroy(MapManager.Maps[0]._objectmap.gameObject);
            Destroy(MapManager.Maps[0]._objectsParent);
            MapManager.Maps[0] = null;
        }

        if (manager != null)
        {
            manager.StartProcess();
        }

        if (gameStartManager != null)
        {
            gameStartManager.Start();
        }
    }
}
