using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Extensions
{
    public static void SelectTile(this Tilemap map, Vector2Int mapbounds, Action<Vector2Int> tileProcess)
    {
        for (int i = -mapbounds.x; i < mapbounds.x; i++)
        {
            for (int j = -mapbounds.x; j < mapbounds.y; j++)
            {
                var processTilePos = new Vector2Int(i, j);
                tileProcess(processTilePos);
            }
        }
    }
}
