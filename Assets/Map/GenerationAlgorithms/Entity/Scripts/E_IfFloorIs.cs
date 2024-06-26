using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "GenerationAlgorithm/Entity/IfFloorIs", fileName = "E_IfFloorIs")]
public class E_IfFloorIs : EntityGenerationAlgorithm
{
    [SerializeField] EntityMapInFloor[] generationSettings;

    public override GameObject SpawnEntity(MapEnvironment env, int mapIndex, Tilemap groundmap, Tilemap objectmap)
    {
        foreach (var set in generationSettings)
        {
            if (set.floor == MapManager.Maps[mapIndex]._layer)
            {
                return set.algo.SpawnEntity(env, mapIndex, groundmap, objectmap);
            }
        }
        return null;
    }
}

[Serializable]
public class EntityMapInFloor
{
    public EntityGenerationAlgorithm algo;
    public int floor;
}
