using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "GenerationAlgorithm/Object/IfFloorIs", fileName = "O_IfFloorIs")]
public class O_IfFloorIs : ObjectGenerationAlgorithm
{
    [SerializeField] MapInFloor[] generationSettings;

    public override Tilemap CreateMap(MapEnvironment env, int mapIndex)
    {
        var refmap = InitMap();
        foreach(var set in generationSettings)
        {
            if(set.floor == MapManager.Maps[mapIndex]._layer)
            {
                var algo = set.algo as ObjectGenerationAlgorithm;
                algo.GenerateWithAlgorithm(env, mapIndex, ref refmap);
            }
        }
        return refmap;
    }

    public override Tilemap GenerateWithAlgorithm(MapEnvironment env, int mapIndex, ref Tilemap refmap)
    {
        throw new System.NotImplementedException();
    }
}

[Serializable]
public class MapInFloor
{
    public TileGenerationBase algo;
    public int floor;
}
