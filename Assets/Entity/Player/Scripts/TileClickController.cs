using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileClickController : MonoBehaviour
{
    [SerializeField] TileSettings tileSettings;
    [SerializeField] Vector3Int clickedPos;
    public AudioSource audioSource;
    public TileObject ActiveTileObject;

    private void Update()
    {
        if (ActiveTileObject)
        {
            ActiveTileObject.ClickEvent.UpdateEvent(this, clickedPos);
        }
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            //�N���b�N�����^�C���ʒu���擾
            var clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = 0;
            var map = MapManager._currentObjectMap;
            var targetCellPos = map.WorldToCell(clickPos);

            //�w��̃^�C�����N���b�N�����ꍇ�C�x���g�����s
            foreach (var tile in tileSettings.Facilities)
            {
                if (tile.ClickEvent != null && map.GetTile(targetCellPos) == tile.Tile)
                {
                    ActiveTileObject = tile;
                    clickedPos = targetCellPos;
                    tile.ClickEvent.Enter(this, clickedPos, tile);
                }
            }
        }
    }

    public void ForcedExit()
    {
        ActiveTileObject?.ClickEvent?.Exit(this);
    }
}
