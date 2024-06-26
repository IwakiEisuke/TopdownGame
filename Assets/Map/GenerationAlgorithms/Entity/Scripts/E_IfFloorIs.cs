using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class E_IfFloorIs : EntityGenerationAlgorithm
{
    [SerializeField] MapInFloor[] generationSettings;

    public override GameObject SpawnEntity(MapEnvironment env, Tilemap groundmap, Tilemap objectmap)
    {
        throw new System.NotImplementedException();
    }

    //public override GameObject SpawnEntity(MapEnvironment env, Tilemap groundmap, Tilemap objectmap)
    //{
    //    foreach (var set in generationSettings)
    //    {
    //        if (set.floor == MapManager.Maps[mapIndex]._layer)
    //        {
    //            var algo = set.algo as ObjectGenerationAlgorithm;
    //            algo.GenerateWithAlgorithm(env, mapIndex, ref refmap);
    //        }
    //    }
    //}
}
