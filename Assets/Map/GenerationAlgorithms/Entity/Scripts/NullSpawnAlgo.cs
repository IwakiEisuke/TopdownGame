using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "GenerationAlgorithm/Entity/Null")]
public class NullSpawnAlgo : EntityGenerationAlgorithm
{
    public override GameObject SpawnEntity(MapEnvironment env)
    {
        var parentObject = new GameObject();
        SetParentOnGrid(parentObject);

        return parentObject;
    }
}
