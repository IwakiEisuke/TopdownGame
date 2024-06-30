using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMouseEventHandler : MonoBehaviour
{
    public static event EventHandler LeftMouseClickNotUI, LeftMouseClickUI;
    [SerializeField] GameObject canvasUI;
    GraphicRaycaster graphicRaycaster;
    EventSystem eventSystem;

    private void Start()
    {
        graphicRaycaster = canvasUI.GetComponent<GraphicRaycaster>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (IsPointerOverUI(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
    //        {
    //        }
    //        else
    //        {
    //            LeftMouseClickNotUI();
    //        }
    //    }
    //}

    protected virtual void OnLeftMouseClickNotUIEvent(EventArgs e)
    {
        LeftMouseClickUI?.Invoke(this, e);
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
