using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "GenerationAlgorithm/Object/Random")]
public class O_Random : ObjectGenerationAlgorithm
{
    [SerializeField] int count;
    public override Tilemap Algorithm(MapEnvironment env, int mapIndex, ref Tilemap refmap)
    {
        var map = refmap;

        Vector2Int[] pos = new Vector2Int[count];
        var mapbounds = env._mapAlgo._mapBounds;


        for (int i = 0; i < count; i++)
        {
            pos[i] = new Vector2Int(Random.Range(-mapbounds.x, mapbounds.x), Random.Range(-mapbounds.y, mapbounds.y));
        }

        var weights = GetWeights();
        foreach (var randomPos in pos)
        {
            map.SelectTile(mapbounds, (pos) =>
            {
                if (randomPos == pos)
                {
                    map.SetTile((Vector3Int)pos, _tileSettings[ChooseWeight(weights)]._tile);
                }
            });
        }

        refmap = map;
        return refmap;
    }
}
