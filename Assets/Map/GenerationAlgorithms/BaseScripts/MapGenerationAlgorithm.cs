using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public abstract class MapGenerationAlgorithm : TileGenerationBase
{
    public Vector2Int _mapBounds;
    public abstract Tilemap CreateMap(MapEnvironment env);

    public Tilemap InitMap()
    {
        return base.InitMap("Ground", "GroundMap");
    }
}
