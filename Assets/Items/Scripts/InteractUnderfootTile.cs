using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractUnderfootTile : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var objectMap = MapManager._currentObjectMap;
            var targetPos = Vector3Int.FloorToInt(transform.position);
            TileBase underfootTile = objectMap.GetTile(targetPos);
            if (underfootTile != null)
            {
                Debug.Log(underfootTile.name + " : " + targetPos);
            }

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

            var selecteditem = Inventory.GetInventoryItem(ItemUseController.selectedItem);

            foreach (var recipe in selecteditem.recipesTransform)
            {
                foreach (var requireEq in recipe.equipment)
                {
                    foreach (var equipment in Settings.TileSettings.equipments)
                    {
                        if (underfootTile == equipment && requireEq == equipment)
                        {
                            CreateInventoryItem.CreateItem(RecipeType.Equipment, 0);
                            Debug.Log("can create transformRecipe");
                        }
                    }
                }
            }



            foreach (var mapSet in StairsCreator.CreatedMapSets)
            {
                //CreateMapでStairを生成しているとエラーが起きる。処理を切り離すもしくは非推奨だがforループにする
                //そもそもこのタイミングでStair生成しなくてよかったので生成個所を消して解決しました
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
                                    if (MapManager.IsMapEmpty(index)) //空マップの場合
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

                /*
                var debug = "";

                var index = -1;
                var isUp = false;


                Action action = () =>
                {
                    var pos = new Vector3Int[] { stair.top.pos, stair.low.pos };
                    if (targetPos == pos[0] || targetPos == pos[1])
                    {
                        if (MapManager.IsCreated(stair.top.map._index))
                        {
                            debug = "isCreated";
                            MapManager.SetMap(stair.index);
                        }
                        else
                        {
                            debug = "noCreated";
                            MapManager.CreateMap(stair);
                        }

                        debug += $" | pos:{stair.pos}, up:{stair.topLayer}, low:{stair.lowLayer}, index:{stair.index}";
                        Debug.Log(debug);
                    }
                };


                if (stair.topMapIndex == MapManager._currentMapData._index)
                {
                    index = stair.topMapIndex;
                    isUp = true;
                    action();
                }
                else if (stair.lowMapIndex == MapManager._currentMapData._index)
                {
                    index = stair.lowMapIndex;
                    isUp = false;
                    action();
                }
                */

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
}
