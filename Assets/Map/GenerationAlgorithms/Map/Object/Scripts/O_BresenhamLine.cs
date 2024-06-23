using System.Collections;
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
        for (int i = 0; i < 10; i++)
        {
            var random = Random.Range(0, count);
            if (!vertices[random].alreadySet)
            {
                Debug.Log("before : " + vertices[random].Pos);
                vertices[random].Set(currentMapStairsPos[1]);
                Debug.Log("after : " + vertices[random].Pos);
                Debug.Log("階段に道路頂点が生成されました");
                break;
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
                if (!vertices[i].alreadySet)
                {
                    var pos = new Vector2(Random.Range(-shrinkedBounds.x, shrinkedBounds.x), Random.Range(-shrinkedBounds.y, shrinkedBounds.y));
                    vertices[i].Set(pos);
                }
            }
            Debug.Log("vertex : " + vertices[i].Pos);
        }

        //ドロネー三角形分割でグラフを生成した後、最小全域木から通路を生成
        var paths = Path.CreateTree(vertices);

        var tiles = new List<Vector2Int>();
        //ブレゼンハムのアルゴリズムを使用してタイルマップの通路部分を決める
        //for (int i = 0; i < vertices.Length - 1; i++)
        //{
        //    foreach (var pos in GetLineTiles(Vector2Int.FloorToInt(vertices[i].Pos), Vector2Int.FloorToInt(vertices[i + 1].Pos)))
        //    {
        //        map.SelectTile(mapbounds, (p) =>
        //        {
        //            if (Vector2.Distance(pos, p) <= lineWidth)
        //            {
        //                tiles.Add(p);
        //            }
        //        });
        //    }
        //}

        foreach (var path in paths)
        {
            var v1 = Vector2Int.FloorToInt(path.start.Pos);
            var v2 = Vector2Int.FloorToInt(path.end.Pos);
            foreach(var pos in GetLineTiles(v1, v2))
            {
                map.SelectTile(mapbounds, (p) =>
                {
                    if (Vector2.Distance(pos, p) <= lineWidth)
                    {
                        tiles.Add(p);
                    }
                });
            }
        }

        //上で決めた通路部分以外に壁を生成
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

class Path : IEnumerable<Vertice>
{
    public static List<Path> pathInstances = new();
    public Vertice start { get; private set; }
    public Vertice end { get; private set; }
    public float dist { get; private set; }

    public Path(Vertice start, Vertice end)
    {
        this.start = start;
        this.end = end;
        dist = Vector2.Distance(start.Pos, end.Pos);
        pathInstances.Add(this);
    }

    public static List<Path> CreateTree(Vertice[] vertices)
    {
        var tri = new Triangulator();
        tri.CreateTriangles(vertices);

        List<Path> allPaths = new();
        var union = new UnionTree();

        for (int i = 0; i < vertices.Length - 1; i++)
        {
            for (int j = i + 1; j < vertices.Length; j++)
            {
                if (vertices[i].connectedV.Contains(vertices[j]))
                {
                    Debug.DrawLine(vertices[i].Pos, vertices[j].Pos, Color.blue, 2);
                    allPaths.Add(new Path(vertices[i], vertices[j]));
                }
            }
        }

        allPaths.Sort((x, y) => x.dist.CompareTo(y.dist));
        var selectedPaths = new List<Path>();
        foreach (var path in allPaths)
        {
            var v1 = path.start;
            var v2 = path.end;
            if (!union.same(v1, v2))
            {
                union.unite(v1, v2);
                selectedPaths.Add(path);
                Debug.DrawLine(v1.Pos, v2.Pos, Color.yellow, 2);
            }
        }

        foreach (var path in selectedPaths)
        {
            Debug.Log(path.start.Pos + " | " + path.end.Pos + " ||" + path.dist);
        }

        foreach (var v in vertices)
        {
            Debug.DrawLine(v.Pos + Vector2.left, v.Pos + Vector2.up, Color.red, 2);
            Debug.DrawLine(v.Pos + Vector2.up, v.Pos + Vector2.right, Color.red, 2);
            Debug.DrawLine(v.Pos + Vector2.right, v.Pos + Vector2.down, Color.red, 2);
            Debug.DrawLine(v.Pos + Vector2.down, v.Pos + Vector2.left, Color.red, 2);
        }

        return selectedPaths;
    }

    public IEnumerator<Vertice> GetEnumerator()
    {
        var vertices = new Vertice[] { start, end };
        foreach (var v in vertices)
        {
            yield return v;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

/// <summary>
/// 道を生成するためのノード。曲がり角
/// </summary>
public class Vertice
{
    public Vector2 Pos { get; private set; }
    public bool alreadySet { get; private set; } = false;
    /// <summary>
    /// エッジ生成用
    /// </summary>
    public bool IsChecked { get; private set; } = false;
    public int connect { get; private set; } = 0;
    public List<Vertice> connectedV = new();

    private Vertice root;
    public readonly int id;

    public Vertice(int id)
    {
        this.id = id;
        root = this;
    }

    public Vertice() { root = this; }

    public void Set(Vector2 pos)
    {
        Pos = pos;
        alreadySet = true;
    }

    public void Connect(params Vertice[] vertices)
    {
        foreach (var v in vertices)
        {
            if (!connectedV.Contains(v))
            {
                connectedV.Add(v);
            }
        }
    }

    public void SetRoot(Vertice root)
    {
        this.root = root;
    }

    public Vertice GetRoot()
    {
        if (root == this)
        {
            return this;
        }
        else
        {
            return root.GetRoot();
        }
    }
}

class UnionTree
{
    public UnionTree()
    {

    }

    public Vertice Root(Vertice v)
    {
        if (v.GetRoot() == v)
        {
            return v;
        }
        else
        {
            return v.GetRoot();
        }
    }

    public bool same(Vertice v1, Vertice v2)
    {
        return v1.GetRoot() == v2.GetRoot();
    }

    public void unite(Vertice v1, Vertice v2)
    {
        v1 = v1.GetRoot();
        v2 = v2.GetRoot();
        if (v1 == v2) return;

        v1.SetRoot(v2);
    }
}
