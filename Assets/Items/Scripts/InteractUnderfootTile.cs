using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractUnderfootTile : MonoBehaviour
{
    [SerializeField] GameObject entranceLight;
    [SerializeField] TileSettings tileSettings;
    [SerializeField] AudioSource enterAS;
    [SerializeField] AudioSource pickupAS;
    [SerializeField] AudioClip[] pickupSE;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var objectMap = MapManager._currentObjectMap;
            var targetPos = Vector3Int.FloorToInt(transform.position);
            TileBase underfootTile = objectMap.GetTile(targetPos);

            //if (underfootTile != null)
            //{
            //    Debug.Log(underfootTile.name + " : " + targetPos);
            //}

            //�����̃A�C�e�����l��
            foreach (var item in Inventory.Items)
            {
                //foreach (var tile in item.itemTiles)
                //{
                //    if (underfootTile == tile)
                //    {
                //        pickupAS.PlayOneShot(pickupSE[Random.Range(0, pickupSE.Length)]);
                //        item.amount++;
                //        objectMap.SetTile(targetPos, null);
                //    }
                //}

                foreach (var tileSetting in item.itemTileSettings)
                {
                    if(underfootTile == tileSetting.tile)
                    {
                        pickupAS.PlayOneShot(pickupSE[Random.Range(0, pickupSE.Length)]);
                        item.amount += tileSetting.giveAmount;
                        objectMap.SetTile(targetPos, null);
                    }
                }    
            }

            //�����̊K�i�𔻒�
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
                                enterAS.Play();

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


                                //�K�i�������ɋ�}�b�v�𐶐����Ă��邽�ߊK�i�̈ړ���ł���΃}�b�v�͍쐬�ς�
                                if (MapManager.IsCreated(index))
                                {
                                    if (MapManager.IsMapEmpty(index)) //��}�b�v�̏ꍇ�V�K�쐬
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

            //TileObject�ɃA�T�C������Ă���TileInteractEvent�����s
            foreach(var tile in tileSettings.Facilities)
            {
                if(MapManager._currentObjectMap.GetTile(targetPos) == tile.Tile)
                {
                    if(tile.InteractEvent != null)
                    {
                        tile.InteractEvent.Enter(this, targetPos, tile);
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
