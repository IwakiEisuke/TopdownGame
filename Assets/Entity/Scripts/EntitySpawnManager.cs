using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class EntitySpawnManager : MonoBehaviour
{
    [SerializeField] GameObject entityBasePrefab;
    [SerializeField] SpawnSetting[] spawnSetting;
    [SerializeField] Vector2Int spawnBounds;
    //[SerializeField] EntityGenerationAlgorithm algo;

    public void Spawn()
    {
        //algo.SpawnEntity(MapManager._currentMapData._env);
        var currentGroundMap = MapManager._currentGroundMap;
        var centerPos = new Vector3Int(Random.Range(0, spawnBounds.x), Random.Range(0, spawnBounds.x));

        foreach (var setting in spawnSetting)
        {
            foreach (var canSpawnTile in setting.spawnTiles)
            {
                //ê∂ê¨ê›íË
                var index = Random.Range(0, setting.spawnEntityTypeSetting.Length);

                var spawnCount = setting.spawnEntityTypeSetting[0].spawnCount;
                var max = spawnCount.max;
                var min = spawnCount.min;

                //ê∂ê¨èàóù
                for (int count = 0; count < Random.Range(min, max + 1); count++)
                {
                    var pos = centerPos + new Vector3Int(Random.Range(-2, 2), Random.Range(-2, 2));
                    var randomTile = currentGroundMap.GetTile(pos);

                    if (randomTile == canSpawnTile)
                    {
                        var entityData = setting.spawnEntityTypeSetting[index].entityType;
                        
                        entityData.CreateEntityInstance(pos, MapManager._currentObjectsParent);
                        //instance.transform.position = pos;
                        //instance.GetComponent<SpriteRenderer>().sprite = setting.spawnEntityTypeSetting[index].entityType.sprite;

                        //instance.GetComponent<EntityController>().entityData = setting.spawnEntityTypeSetting[index].entityType;
                    }
                }
            }
        }
    }

    void StartSpawnRoutine()
    {
        foreach (var setting in spawnSetting)
        {
            foreach (var spawn in setting.spawnEntityTypeSetting)
            {
                Invoke(nameof(StartSpawnRoutine), Random.Range(spawn.spawnLate.min, spawn.spawnLate.max));
                Spawn();
            }
        }
    }
}

[Serializable]
public class SpawnSetting
{
    public TileBase[] spawnTiles;
    public EntityTypeSetting[] spawnEntityTypeSetting;
}

[Serializable]
public class EntityTypeSetting
{
    public EntityData entityType;
    public SpawnTime spawnLate;
    public SpawnCount spawnCount;
}

[Serializable]
public class SpawnTime
{
    public float min;
    public float max;
}

[Serializable]
public class SpawnCount
{
    public int min;
    public int max;
}
