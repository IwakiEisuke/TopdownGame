using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "GenerationAlgorithm/Object/Null")]
public class O_Null : ObjectGenerationAlgorithm
{
    public override Tilemap CreateMap(MapEnvironment env, int mapIndex)
    {
        return InitMap();
    }
}
