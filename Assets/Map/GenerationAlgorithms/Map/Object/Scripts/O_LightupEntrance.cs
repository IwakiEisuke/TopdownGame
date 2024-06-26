using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "GenerationAlgorithm/Object/LightupEntrance", fileName = "O_LightupEntrance")]
public class O_LightupEntrance : ObjectGenerationAlgorithm
{
    [SerializeField] GameObject entranceLight;
    public override Tilemap GenerateWithAlgorithm(MapEnvironment env, int mapIndex, ref Tilemap refmap)
    {
        if (MapManager.Maps[mapIndex]._layer == -1)
        {
            Vector3 movePos = Vector3.zero;
            bool findStair = false;
            MapManager.SelectStair(MapManager.Maps[mapIndex], (stair, index, tile) =>
            {
                if(index == 0)
                {
                    if (MapManager.GetMap(stair.points[1].mapIndex)._layer == 0)
                    {
                        movePos = stair.points[0].pos;
                        findStair = true;
                    }
                }
                else
                {
                    if (MapManager.GetMap(stair.points[0].mapIndex)._layer == 0)
                    {
                        movePos = stair.points[1].pos;
                        findStair = true;
                    }
                }
            });

            if (findStair)
            {
                var light = Instantiate(entranceLight, refmap.transform);
                light.transform.position = movePos + new Vector3(0.5f, 0.5f);
            }
            
        }

        return refmap;
    }
}
