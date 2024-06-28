using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileClickController : MonoBehaviour
{
    [SerializeField] TileSettings tileSettings;
    [SerializeField] Vector3Int clickedPos;
    public TileObject ActiveTileObject;

    private void Update()
    {
        if (ActiveTileObject)
        {
            ActiveTileObject.ClickEvent.UpdateEvent(this, clickedPos);
        }
        if (Input.GetMouseButtonDown(0))
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

    ///// <summary>
    ///// Canvas���쐬���A���̎q�Ƃ���ui���쐬����
    ///// </summary>
    ///// <param name="ui"></param>
    ///// <returns></returns>
    //public GameObject CreateUI(GameObject ui)
    //{
    //    //����UI��p��Canvas���쐬����
    //    var canvas = Instantiate(canvasUI);
    //    canvas.name = ui.name + "Canvas";

    //    //var canvas = GameObject.Find("Canvas").GetComponent<Canvas>(); //���C����Canvas�ɓ��ꂽ���ꍇ������

    //    graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
    //    var obj = Instantiate(ui, canvas.transform);
    //    return obj;
    //}

    //public void DestroyUI(GameObject ui)
    //{
    //    Destroy(ui.transform.parent.gameObject);
    //}

    ///// <summary>
    ///// position�i�X�N���[�����W�j��UI�����݂��邩���肷��
    ///// </summary>
    ///// <param name="position"></param>
    ///// <returns></returns>
    //private bool IsPointerOverUI(Vector2 position) //AI����
    //{
    //    if (graphicRaycaster != null)
    //    {
    //        // �|�C���^�̃f�[�^���Z�b�g�A�b�v
    //        PointerEventData pointerEventData = new PointerEventData(eventSystem);
    //        pointerEventData.position = position;

    //        // ���C�L���X�g���i�[���郊�X�g���쐬
    //        List<RaycastResult> raycastResults = new List<RaycastResult>();

    //        // �O���t�B�b�N���C�L���X�^�[���g�p���ă��C�L���X�g�����s
    //        graphicRaycaster.Raycast(pointerEventData, raycastResults);

    //        // �����Ƀq�b�g�������ǂ�����Ԃ�
    //        return raycastResults.Count > 0;
    //    }
    //    Debug.Log("graphicRaycaster��null");
    //    return false;
    //}
}
