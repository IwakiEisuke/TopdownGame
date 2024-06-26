using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "GenerationAlgorithm/Object/Random")]
public class O_Random : ObjectGenerationAlgorithm
{
    [SerializeField] int count;
    public override Tilemap GenerateWithAlgorithm(MapEnvironment env, int mapIndex, ref Tilemap refmap)
    {
        var map = refmap;

        Vector3Int[] randomPos = new Vector3Int[count];
        var mapbounds = env._mapAlgo._mapBounds;


        for (int i = 0; i < count; i++)
        {
            randomPos[i] = new Vector3Int(Random.Range(-mapbounds.x, mapbounds.x), Random.Range(-mapbounds.y, mapbounds.y));
        }

        var weights = GetWeights();
        foreach (var p in randomPos)
        {
            map.SelectTile(mapbounds, (pos) =>
            {
                if (p == pos && map.GetTile(pos) == null)
                {
                    map.SetTile(pos, _tileSettings[ChooseWeight(weights)]._tile);
                }
            });
        }

        refmap = map;
        return refmap;
    }
}
