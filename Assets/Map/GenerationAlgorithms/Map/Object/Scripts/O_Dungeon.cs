using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "GenerationAlgorithm/Object/Dungeon")]
public class O_Dungeon : ObjectGenerationAlgorithm
{
    [SerializeField] int vertexCount;
    [SerializeField] float lineWidth;

    public override Tilemap Algorithm(MapEnvironment env, int mapIndex, ref Tilemap refmap)
    {
        var map = refmap;
        var curStairsPos = MapManager.GetCurStairsPos(mapIndex);

        //既存の頂点とランダムにつなげるための頂点の数
        var add = Random.Range(0, 5);
        var count = vertexCount + curStairsPos.Count;
        Vector2Int?[] roadVertex = new Vector2Int?[count + add];

        //階段を頂点に指定。同じインデックスに入らないようにする
        roadVertex[Random.Range(0, count)] = curStairsPos[0];
        for (int i = 0; i < 10; i++)
        {
            var random = Random.Range(0, count);
            Debug.Log("before : " + roadVertex[random]);
            if (roadVertex[random] == null)
            {
                roadVertex[random] = curStairsPos[1];
                Debug.Log("after : " + roadVertex[random]);
                Debug.LogWarning("階段に道路頂点が生成されました");
                break;
            }
        }


        var mapbounds = env._mapAlgo._mapBounds;

        //頂点の作成
        for (int i = 0; i < count + add; i++)
        {
            if (i < count)
            {
                if (roadVertex[i] == null)
                {
                    var shrinkedBounds = Vector2Int.FloorToInt((Vector2)mapbounds * 0.8f);
                    roadVertex[i] = new Vector2Int(Random.Range(-shrinkedBounds.x, shrinkedBounds.x), Random.Range(-shrinkedBounds.y, shrinkedBounds.y));
                }
            }
            else
            {
                roadVertex[i] = roadVertex[Random.Range(0, count)];
            }
            Debug.Log("vertex : " + roadVertex[i]);
        }

        var tiles = new List<Vector2Int>();

        for (int i = 0; i < roadVertex.Length - 1; i++)
        {
            foreach (var pos in GetLineTiles((Vector2Int)roadVertex[i], (Vector2Int)roadVertex[i + 1]))
            {
                map.SelectTile(mapbounds, (p) =>
                {
                    if (Vector2Int.Distance(pos, p) <= lineWidth)
                    {
                        tiles.Add(p);
                    }
                });
            }
        }

        map.SelectTile(mapbounds, (p) =>
        {
            var isGround = false;
            foreach (var tilePos in tiles)
            {
                if (p == tilePos)
                {
                    isGround = true;
                }
            }

            if (!isGround)
            {
                map.SetTile((Vector3Int)p, _tileSettings[0]._tile);
            }
        });

        refmap = map;
        return refmap;
    }

    //ブレゼンハムのアルゴリズム。AI生成
    List<Vector2Int> GetLineTiles(Vector2Int start, Vector2Int end)
    {
        
        List<Vector2Int> lineTiles = new List<Vector2Int>();

        int x0 = start.x;
        int y0 = start.y;
        int x1 = end.x;
        int y1 = end.y;

        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);

        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;

        int err = dx - dy;

        while (true)
        {
            lineTiles.Add(new Vector2Int(x0, y0));

            if (x0 == x1 && y0 == y1) break;

            int e2 = err * 2;

            if (e2 > -dy)
            {
                err -= dy;
                x0 += sx;
            }

            if (e2 < dx)
            {
                err += dx;
                y0 += sy;
            }
        }

        return lineTiles;
    }
}
