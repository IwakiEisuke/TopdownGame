using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public abstract class TileGenerationBase : ScriptableObject
{
    public GenerateTileSetting[] _tileSettings;

    public virtual Tilemap InitMap(string sortingLayerName, string mapName)
    {
        var grid = FindObjectOfType<Grid>();
        var map = new GameObject().AddComponent<Tilemap>();
        map.name = mapName;
        map.transform.parent = grid.transform;
        var renderer = map.AddComponent<TilemapRenderer>();
        renderer.sortingLayerName = sortingLayerName;
        if(sortingLayerName == "Object")
        {
            var coll = map.AddComponent<TilemapCollider2D>();
            coll.usedByComposite = true;
            var rb = map.AddComponent<Rigidbody2D>();
            rb.isKinematic = true;
            map.AddComponent<CompositeCollider2D>();
        }

        return map;
    }

    public int[] GetWeights()
    {
        var weights = new int[_tileSettings.Length];

        for (int i = 0; i < _tileSettings.Length; i++)
        {
            weights[i] = _tileSettings[i]._weight;
        }

        if (weights.Any(weight => weight <= 0))
        {
            throw new System.ArgumentException($"{name}には生成されないタイルが含まれています");
        }
        return weights;
    }

    public int ChooseWeight(int[] weights)
    {
        var num = Random.Range(0, weights.Sum());
        int index = -1;

        for (int k = 0; k < _tileSettings.Length; k++)
        {
            if (weights.Take(k).Sum() <= num && num < weights.Take(k + 1).Sum())
            {
                index = k;
                break;
            }
        }

        if (index == -1)
        {
            throw new System.Exception("生成するタイルチップを指定できませんでした。コードを修正してください");
        }

        return index;
    }
}

[Serializable]
public class GenerateTileSetting
{
    public TileBase _tile;
    public int _weight;
}
