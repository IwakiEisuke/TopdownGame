using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "GenerationAlgorithm/Object/BresenhamLine")]
public class O_BresenhamLine : ObjectGenerationAlgorithm
{
    [SerializeField] int vertexCount, addLoopCount;
    [SerializeField] float lineWidth;

    public override Tilemap Algorithm(MapEnvironment env, int mapIndex, ref Tilemap refmap)
    {
        var map = refmap;
        var currentMapStairsPos = MapManager.GetCurStairsPos(mapIndex); //生成するマップにある階段を取得
        //生成する通路の頂点（曲がり角の数）
        var count = vertexCount + currentMapStairsPos.Count;
        Vertice[] vertices = new Vertice[count];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vertice(i); //頂点を生成し、生成順にidを割り振る
        }

        //階段を頂点に入れる。
        var startStairIndex = Random.Range(0, count);
        vertices[startStairIndex].Set(currentMapStairsPos[0]);
        //2個目の階段に頂点を設定。
        if (currentMapStairsPos.Count == 2)
        {
            for (int i = 0; i < 10; i++)
            {
                var random = Random.Range(0, count);
                if (!vertices[random].AlreadySet)
                {
                    vertices[random].Set(currentMapStairsPos[1]);
                    break;
                }
            }
        }

        //頂点を生成する範囲の決定のためにマップ範囲を取得
        var mapbounds = env._mapAlgo._mapBounds;
        var shrinkedBounds = (Vector2)mapbounds * 0.8f;
        //ランダムな位置に頂点を作成
        for (int i = 0; i < count; i++)
        {
            if (i < count)
            {
                if (!vertices[i].AlreadySet)
                {
                    var pos = new Vector2(Random.Range(-shrinkedBounds.x, shrinkedBounds.x), Random.Range(-shrinkedBounds.y, shrinkedBounds.y));
                    vertices[i].Set(pos);
                }
            }
        }

        //ドロネー三角形分割でグラフを生成した後、最小全域木から通路を生成
        var paths = MinimumSpanningTree.CreateTree(vertices);

        //マップ全体を壁にする
        map.Fill(mapbounds, _tileSettings[0]._tile);

        //ブレゼンハムのアルゴリズムを使用してタイルマップの通路部分を消す
        foreach (var path in paths)
        {
            var v1 = Vector2Int.FloorToInt(path.start.Pos);
            var v2 = Vector2Int.FloorToInt(path.end.Pos);
            foreach (var pos in Bresenham.GetLineTiles(v1, v2))
            {
                map.SetTileNullInLine(lineWidth, pos);
            }
        }

        refmap = map;
        return refmap;
    }
}

public static class Bresenham
{
    //ブレゼンハムのアルゴリズム。AI生成
    public static List<Vector3Int> GetLineTiles(Vector2Int start, Vector2Int end)
    {

        List<Vector3Int> lineTiles = new List<Vector3Int>();

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
            lineTiles.Add(new Vector3Int(x0, y0));

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
