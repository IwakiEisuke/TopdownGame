using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "GenerationAlgorithm/Object/Mix")]
public class O_Mix : ObjectGenerationAlgorithm
{
    [SerializeField] ObjectGenerationAlgorithm[] algos;

    public override Tilemap CreateMap(MapEnvironment env, int mapIndex)
    {
        var refmap = InitMap();
        foreach (var algo in algos)
        {
            algo.Algorithm(env, mapIndex, ref refmap);
        }
        return refmap;
    }

    public override Tilemap Algorithm(MapEnvironment env, int mapIndex, ref Tilemap refmap)
    {
        throw new System.NotImplementedException();
    }
}
