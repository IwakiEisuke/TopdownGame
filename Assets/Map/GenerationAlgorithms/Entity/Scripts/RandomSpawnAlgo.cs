using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "GenerationAlgorithm/Entity/Random")]
public class RandomSpawnAlgo : EntityGenerationAlgorithm
{
    public override GameObject SpawnEntity(MapEnvironment env)
    {
        var canSpawn = CalcProbabilityOnSpawn();

        var obj = new List<GameObject>();
        /*
        foreach (var entityData in _spawnEntityData)
        {
            if (canSpawn[0] == true)
            {
                var main = RandomVector3(-100, 100);
                foreach (var entity in entityData._spawnEntities)
                {
                    var count = entityData._entitiesPerSpawn;
                    obj = count.Times(() => entity.CreateEntityInstance(RandomVector3(-5, 5) + main));
                }
            }
        }

        foreach (var entityData in _spawnEntityData)
        {
            foreach (var i in entityData._spawnEntities)
            {

            }
        }
        */

        var bounds = env._mapAlgo._mapBounds;

        Func<Vector3> action = () => RandomVector3(-bounds.x, bounds.x, -bounds.y, bounds.y);

        obj = CreateEntity(action);

        var parentObject = new GameObject();
        SetParentOnGrid(parentObject);

        obj.ForEach(child => child.transform.SetParent(parentObject.transform));

        return parentObject;
    }

    public static Vector3 RandomVector3(float minX, float maxX, float minY, float maxY)
    {
        var x = Random.Range(minX, maxX);
        var y = Random.Range(minY, maxY);

        var pos = new Vector3(x, y);

        //Debug.Log("wow ; " + Random.insideUnitCircle * Random.Range(min, max));
        //return Random.insideUnitCircle * Random.Range(min, max);

        Debug.Log("woah : " + pos);
        return pos;
    }
}

public static class ZattaExtensions
{
    public static List<GameObject> Times(this int count, Func<GameObject> action)
    {
        List<GameObject> obj = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            obj.Add(action());
        }
        return obj;
    }
}
