using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "GenerationAlgorithm/Object/Dungeon")]
public class O_Dungeon : ObjectGenerationAlgorithm
{
    [SerializeField] int vertexCount;
    [SerializeField] float lineWidth;
    [SerializeField] Vector2 step;

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
        var s = shrinkedBounds * 2 / step;

        int rx = 0;
        int ry = 0;
        int loopCount = 0;
        //ランダムな位置に頂点を作成
        for (int i = 0; i < count; i++)
        {
            if (i < count)
            {
                if (!vertices[i].AlreadySet)
                {
                    //進行方向
                    int xdir = Mathf.Round(Random.value) == 1 ? 1 : -1;
                    int ydir = Mathf.Round(Random.value) == 1 ? 1 : -1;

                    //範囲内で10マスずつランダムに移動
                    rx = (int)Mathf.Clamp(rx + s.x * xdir, -shrinkedBounds.x, shrinkedBounds.x);
                    ry = (int)Mathf.Clamp(ry + s.y * ydir, -shrinkedBounds.y, shrinkedBounds.y);
                    var pos = new Vector2(rx, ry);

                    var isAlreadySetPos = false;
                    foreach (var v in vertices)
                    {
                        if (v.AlreadySet && v.Pos == pos)
                        {
                            isAlreadySetPos = true;
                        }
                    }

                    if (!isAlreadySetPos)
                    {
                        vertices[i].Set(pos);
                    }
                    else
                    {
                        var loops = step.x * step.x * step.y * step.y;
                        if (loopCount > loops)
                        {
                            Debug.LogAssertion("O_Dungeonの頂点生成時に既に頂点が存在する位置が" + loops + "回以上選ばれたため生成を停止しました");
                            break;
                        }
                        loopCount++;
                        i--;
                        continue;
                    }
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
