using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "GenerationAlgorithm/Entity/Random")]
public class E_Random : EntityGenerationAlgorithm
{
    public override GameObject SpawnEntity(MapEnvironment env, Tilemap groundmap, Tilemap objectmap)
    {
        var canSpawn = CalcProbabilityOnSpawn();
        var obj = new List<GameObject>();
        var bounds = env._mapAlgo._mapBounds;

        var parentObject = CreateParentObj();

        Vector3 RandomNullTile()
        {
            var pos = RandomPosInBounds(bounds);
            if (groundmap.GetTile(pos) != null)
            {
                return RandomNullTile();
            }
            return pos + new Vector3(0.5f, 0.5f); //�^�C���̃s�{�b�g�ʒu�ƃG���e�B�e�B�̃s�{�b�g�ʒu�̃Y����␳���ĕԂ�
        }

        obj = CreateEntity(() => RandomNullTile(), parentObject);
        obj.ForEach(child => child.transform.SetParent(parentObject.transform));

        return parentObject;
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
