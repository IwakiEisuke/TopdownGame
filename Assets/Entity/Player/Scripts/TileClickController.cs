using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileClickController : MonoBehaviour
{
    [SerializeField] TileSettings tileSettings;
    [SerializeField] GameObject canvasUI;
    TileObject ActiveTileObject;

    [SerializeField] GraphicRaycaster graphicRaycaster;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Vector3Int clickedPos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //クリックしたタイルを取得
            var clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = 0;
            var map = MapManager._currentObjectMap;
            var targetCellPos = map.WorldToCell(clickPos);
            //Debug.Log(map.GetTile(targetCellPos));

            //なんらかのUIを開いている状態でUI以外をクリックすると閉じる
            //UIを開いてない状態でUIを持っているタイルをクリックするとUIを開く
            if (ActiveTileObject)
            {
                if (!IsPointerOverUI(Input.mousePosition))
                {
                    ActiveTileObject.ClickEvent.Close(this);
                    ActiveTileObject = null;
                }
            }
            else
            {
                //指定のタイルをクリックした場合イベントを実行
                foreach (var tile in tileSettings.Equipments)
                {
                    if (map.GetTile(targetCellPos) == tile.Tile)
                    {
                        ActiveTileObject = tile;
                        clickedPos = targetCellPos;
                        tile.ClickEvent.Open(this, targetCellPos + Vector3Int.right, tile.UI, tile);
                    }
                }
            }
        }
        else if (ActiveTileObject)
        {
            ActiveTileObject.ClickEvent.UpdateUI(clickedPos + Vector3Int.right);
        }
    }

    /// <summary>
    /// Canvasを作成し、その子としてuiを作成する
    /// </summary>
    /// <param name="ui"></param>
    /// <returns></returns>
    public GameObject CreateUI(GameObject ui)
    {
        //このUI専用のCanvasを作成する
        var canvas = Instantiate(canvasUI);
        canvas.name = ui.name + "Canvas";

        //var canvas = GameObject.Find("Canvas").GetComponent<Canvas>(); //メインのCanvasに入れたい場合こっち

        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        var obj = Instantiate(ui, canvas.transform);
        return obj;
    }

    public void DestroyUI(GameObject ui)
    {
        Destroy(ui.transform.parent.gameObject);
    }

    /// <summary>
    /// position（スクリーン座標）にUIが存在するか判定する
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private bool IsPointerOverUI(Vector2 position) //AI生成
    {
        if (graphicRaycaster != null)
        {
            // ポインタのデータをセットアップ
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = position;

            // レイキャストを格納するリストを作成
            List<RaycastResult> raycastResults = new List<RaycastResult>();

            // グラフィックレイキャスターを使用してレイキャストを実行
            graphicRaycaster.Raycast(pointerEventData, raycastResults);

            // 何かにヒットしたかどうかを返す
            return raycastResults.Count > 0;
        }
        Debug.Log("graphicRaycasterがnull");
        return false;
    }
}
