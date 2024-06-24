using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimumSpanningTree
{
    public static List<RoadPath> CreateTree(Vertice[] vertices)
    {
        var tri = new Triangulator();
        tri.CreateTriangles(vertices);

        return Kruskal(vertices);
    }

    private static List<RoadPath> Kruskal(Vertice[] vertices)
    {
        List<RoadPath> allPaths = new();
        var union = new UnionTree();

        for (int i = 0; i < vertices.Length - 1; i++)
        {
            for (int j = i + 1; j < vertices.Length; j++)
            {
                if (vertices[i].connectedV.Contains(vertices[j]))
                {
                    Debug.DrawLine(vertices[i].Pos, vertices[j].Pos, Color.blue, 2);
                    allPaths.Add(new RoadPath(vertices[i], vertices[j]));
                }
            }
        }

        allPaths.Sort((x, y) => x.dist.CompareTo(y.dist));
        var selectedPaths = new List<RoadPath>();
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

        foreach (var v in vertices)
        {
            Debug.DrawLine(v.Pos + Vector2.left, v.Pos + Vector2.up, Color.red, 2);
            Debug.DrawLine(v.Pos + Vector2.up, v.Pos + Vector2.right, Color.red, 2);
            Debug.DrawLine(v.Pos + Vector2.right, v.Pos + Vector2.down, Color.red, 2);
            Debug.DrawLine(v.Pos + Vector2.down, v.Pos + Vector2.left, Color.red, 2);
        }

        return selectedPaths;
    }
}

public class RoadPath : IEnumerable<Vertice>
{
    public static List<RoadPath> pathInstances = new();
    public Vertice start { get; private set; }
    public Vertice end { get; private set; }
    public float dist { get; private set; }

    public RoadPath(Vertice start, Vertice end)
    {
        this.start = start;
        this.end = end;
        dist = Vector2.Distance(start.Pos, end.Pos);
        pathInstances.Add(this);
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
    /// <summary>
    /// 階段の頂点で使われているかのチェック用
    /// </summary>
    public bool AlreadySet { get; private set; } = false;

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
        AlreadySet = true;
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

public class UnionTree
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
