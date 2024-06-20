using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class ObjectGenerationAlgorithm : TileGenerationBase
{
    public abstract Tilemap CreateMap(MapEnvironment env, int mapIndex);
    public Tilemap InitMap()
    {
        return base.InitMap("Object", "ObjectMap");
    }
}
