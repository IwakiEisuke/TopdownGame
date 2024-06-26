using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public abstract class EntityGenerationAlgorithm : ScriptableObject
{
    public GameObject _entityBase;
    public SpawnData[] _spawnDatas;

    public abstract GameObject SpawnEntity(MapEnvironment env, int mapIndex, Tilemap groundmap, Tilemap objectmap);

    public bool[] CalcProbabilityOnSpawn()
    {
        bool[] canSpawn = new bool[_spawnDatas.Length];
        for ( int i = 0; i < _spawnDatas.Length; i++)
        {
            if (Random.Range(0f, 1f) <= _spawnDatas[i]._spawnProbability)
            {
                canSpawn[i] = true;
            }
        }
        return canSpawn;
    }

    /// <summary>
    /// spawnCountフィールドの回数Entityをスポーンします。
    /// </summary>
    /// <remarks>pos関数を生成時に毎回評価し、その戻り値を生成位置にします。</remarks>
    /// <param name="pos"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public List<GameObject> CreateEntity(Func<Vector3> pos, GameObject parent)
    {
        var obj = new List<GameObject>();
        foreach (var spawnData in _spawnDatas)
        {
            for (int i = 0; i < spawnData._spawnCount; i++)
            {
                if (Random.Range(0f, 1f) <= spawnData._spawnProbability)
                {
                    foreach (var spawnEntityData in spawnData._spawnEntityDatas)
                    {
                        for (int j = 0; j < spawnEntityData._entityCount; j++)
                        {
                            obj.Add(spawnEntityData._entityData.CreateEntityInstance(pos(), parent));
                        }
                    }
                }
            }
        }

        return obj;
    }

    public GameObject CreateParentObj()
    {
        var parentObject = new GameObject();
        SetParentOnGrid(parentObject);
        return parentObject;
    }

    public void SetParentOnGrid(GameObject gameObject)
    {
        gameObject.transform.parent = FindObjectOfType<Grid>().transform;
    }
}

[Serializable]
public class SpawnData
{
    public SpawnEntityData[] _spawnEntityDatas;
    public int _spawnCount;
    public float _spawnProbability;
}

[Serializable]
public class SpawnEntityData
{
    public EntityData _entityData;
    public int _entityCount;
}
