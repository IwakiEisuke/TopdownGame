using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractUnderfootTile : MonoBehaviour
{
    [SerializeField] GameObject entranceLight;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var objectMap = MapManager._currentObjectMap;
            var targetPos = Vector3Int.FloorToInt(transform.position);
            TileBase underfootTile = objectMap.GetTile(targetPos);

            //if (underfootTile != null)
            //{
            //    Debug.Log(underfootTile.name + " : " + targetPos);
            //}

            //足元のアイテムを獲得
            foreach (var item in Inventory.Items)
            {
                foreach (var tile in item.itemTiles)
                {
                    if (underfootTile == tile)
                    {
                        item.amount++;
                        objectMap.SetTile(targetPos, null);
                    }
                }
            }

            //足元の階段を判定
            foreach (var mapSet in StairsCreator.CreatedMapSets)
            {
                foreach (var stair in mapSet.stairs)
                {
                    foreach (var point in stair.points)
                    {
                        if (MapManager._currentMapData._index == point.mapIndex)
                        {
                            if (targetPos == point.pos)
                            {
                                Debug.Log("It's a cave");

                                int index;
                                Vector3Int movePos;

                                if (point == stair.points[0])
                                {
                                    index = stair.points[1].mapIndex;
                                    movePos = stair.points[1].pos;
                                }
                                else
                                {
                                    index = stair.points[0].mapIndex;
                                    movePos = stair.points[0].pos;
                                }


                                //階段生成時に空マップを生成しているため階段の移動先であればマップは作成済み
                                if (MapManager.IsCreated(index))
                                {
                                    if (MapManager.IsMapEmpty(index)) //空マップの場合新規作成
                                    {
                                        MapManager.CreateAndSetMap(mapSet.env, index);
                                    }
                                    else
                                    {
                                        MapManager.SetCurrentMap(index);
                                    }

                                    transform.position = movePos + new Vector3(0.5f, 0.5f);
                                }
                            }
                        }
                    }
                }
            }

            
        }
    }

    public void PlaceTile(TileBase placeTile)
    {
        var objectMap = MapManager._currentObjectMap;

        var targetPos = Vector3Int.FloorToInt(transform.position);
        TileBase underfootTile = objectMap.GetTile(targetPos);

        if (underfootTile == null)
        {
            objectMap.SetTile(targetPos, placeTile);
        }
    }

    public void PlaceTile(TileObject tileObj)
    {
        var pos = Vector3Int.FloorToInt(transform.position);
        if(MapManager._currentObjectMap.GetTile(pos) == null)
        {
            CreateFromRecipe.CreateTile(pos, tileObj);
        }
    }
}
