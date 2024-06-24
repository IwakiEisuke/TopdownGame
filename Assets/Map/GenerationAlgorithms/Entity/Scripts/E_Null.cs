using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "GenerationAlgorithm/Entity/Null")]
public class E_Null : EntityGenerationAlgorithm
{
    public override GameObject SpawnEntity(MapEnvironment env, Tilemap groundmap, Tilemap objectmap)
    {
        return CreateParentObj();
    }
}
