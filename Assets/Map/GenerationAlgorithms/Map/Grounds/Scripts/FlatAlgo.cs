using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Debug = UnityEngine.Debug;

[CreateAssetMenu(menuName = "GenerationAlgorithm/Map/Flat")]
public class FlatAlgo : MapGenerationAlgorithm
{
    public override Tilemap CreateMap(MapEnvironment env)
    {
        var debug_timer = Time.realtimeSinceStartup;

        var mapAlgo = env._mapAlgo;

        if (mapAlgo._tileSettings.Length == 0)
        {
            throw new System.Exception($"生成アルゴリズム{env._mapAlgo}は生成タイルを持っていません");
        }

        var bounds = mapAlgo._mapBounds;

        var map = InitMap();
        var weight = GetWeights();

        for (int i = -bounds.x; i < bounds.x; i++)
        {
            for (int j = -bounds.y; j < bounds.y; j++)
            {
                map.SetTile(new Vector3Int(i, j, 0), _tileSettings[ChooseWeight(weight)]._tile);
            }
        }

        Debug.Log("生成時間:" + (Time.realtimeSinceStartup - debug_timer));
        return map;
    }
}
