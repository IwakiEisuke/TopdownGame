using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Extensions
{
    public static List<GameObject> Times(this int count, Func<GameObject> action)
    {
        List<GameObject> obj = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            obj.Add(action());
        }
        return obj;
    }

    public static void SelectTile(this Tilemap map, Vector2Int mapBounds, Action<Vector3Int> tileProcess)
    {
        for (int x = -mapBounds.x; x < mapBounds.x; x++)
        {
            for (int y = -mapBounds.y; y < mapBounds.y; y++)
            {
                tileProcess(new Vector3Int(x, y));
            }
        }
    }

    public static void Fill(this Tilemap map, Vector2Int mapBounds, TileBase tile)
    {
        for (int x = -mapBounds.x; x < mapBounds.x; x++)
        {
            for (int y = -mapBounds.y; y < mapBounds.y; y++)
            {
                map.SetTile(new Vector3Int(x, y), tile);
            }
        }
    }

    public static void SetTileNullInLine(this Tilemap map, float lineWidth, Vector3Int pos)
    {
        var lw = Mathf.CeilToInt(lineWidth);
        for (int x = pos.x - lw; x <= pos.x + lw; x++)
        {
            for (int y = pos.y - lw; y <= pos.y + lw; y++)
            {
                var targetPos = new Vector3Int(x, y);
                if (Vector3Int.Distance(pos, targetPos) <= lineWidth)
                {
                    map.SetTile(Vector3Int.FloorToInt(targetPos), null);
                }
            }
        }
    }

    public static void SetTileInLine(this Tilemap map, float lineWidth, Vector3Int pos, Action<Vector3Int> tileProcess)
    {
        var lw = Mathf.CeilToInt(lineWidth);
        for (int x = pos.x - lw; x <= pos.x + lw; x++)
        {
            for (int y = pos.y - lw; y <= pos.y + lw; y++)
            {
                var targetPos = new Vector3Int(x, y);
                if (Vector3Int.Distance(pos, targetPos) <= lineWidth)
                {
                    tileProcess(targetPos);
                }
            }
        }
    }
}
