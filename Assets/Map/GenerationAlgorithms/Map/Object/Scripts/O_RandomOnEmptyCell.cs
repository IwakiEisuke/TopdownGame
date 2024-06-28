using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "GenerationAlgorithm/Object/RandomOnEmptyCell")]
public class O_RandomOnEmptyCell : ObjectGenerationAlgorithm
{
    [SerializeField] int count;
    public override Tilemap GenerateWithAlgorithm(MapEnvironment env, int mapIndex, ref Tilemap refmap)
    {
        var map = refmap;

        Vector3Int[] randomPos = new Vector3Int[count];
        var mapbounds = env._mapAlgo._mapBounds;

        Vector3Int RandomNullTile()
        {
            var pos = RandomPosInBounds(mapbounds);
            if (map.GetTile(pos) != null)
            {
                return RandomNullTile();
            }
            return pos;
        }

        for (int i = 0; i < count; i++)
        {
            randomPos[i] = RandomNullTile();
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

    public static Vector3Int RandomPosInBounds(Vector2Int bounds)
    {
        var minX = -bounds.x;
        var minY = -bounds.y;
        var maxX = bounds.x;
        var maxY = bounds.y;

        var x = Random.Range(minX, maxX);
        var y = Random.Range(minY, maxY);
        var pos = new Vector3Int(x, y);
        return pos;
    }
}
