using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Object/TileClickEvents/OpenFacilityUI")]
public class OpenFacilityUI : TileClickEvent
{
    [SerializeField] GameObject UIPref;
    [SerializeField] GameObject canvasUI;
    [SerializeField] AudioClip openSE, closeSE;
    [SerializeField] float openSEVolume, closeSEVolume;
    GraphicRaycaster graphicRaycaster;
    EventSystem eventSystem;


    public override void Enter(TileClickController obj, Vector3Int cellPos, TileObject tileObj)
    {
        graphicRaycaster = canvasUI.GetComponent<GraphicRaycaster>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        Setup(obj, cellPos, UIPref);
        var creator = uiInstance.GetComponentInChildren<RecipeUICreator>();
        creator.CreateTransformUI(tileObj.Tile);
        obj.audioSource.PlayOneShot(openSE, openSEVolume);
    }

    public override void UpdateEvent(TileClickController obj, Vector3Int cellPos)
    {
        uiInstance.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(cellPos + Vector3.right);

        //なんらかのUIを開いている状態でUI以外をクリックすると閉じる
        //UIを開いてない状態でUIを持っているタイルをクリックするとUIを開く

        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI(Input.mousePosition))
        {
            Exit(obj);
        }
    }

    public override void Exit(TileClickController obj)
    {
        DestroyUI(uiInstance);
        obj.ActiveTileObject = null;
        obj.audioSource.PlayOneShot(closeSE, closeSEVolume);
    }

    public void Setup(TileClickController obj, Vector3Int cellPos, GameObject UIPref)
    {
        uiInstance = CreateUI(UIPref);
        uiInstance.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(cellPos + Vector3.right);
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
