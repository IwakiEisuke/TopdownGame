using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUseController : MonoBehaviour
{
    [SerializeField] GameObject canvasUI;
    [SerializeField] AudioSource selectAS;
    public GameObject player;
    public GameObject itemBase;
    public float power;
    public float torque;
    public static int selectedItem;
    public TextMeshProUGUI text;
    public float coolTime;

    GraphicRaycaster graphicRaycaster;
    EventSystem eventSystem;

    private void Start()
    {
        graphicRaycaster = canvasUI.GetComponent<GraphicRaycaster>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        ItemObjectCreator.itemBase = itemBase;
    }

    private void Update()
    {
        coolTime -= Time.deltaTime;
        //アイテム使用処理
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl)) && coolTime <= 0)
        {
            var item = Inventory.GetSelectedItem();

            if (item.amount > 0 && !IsPointerOverUI(Input.mousePosition))
            {
                foreach (var skill in item.skills)
                {
                    skill.Action(this, item);
                }
            }
        }

        //数字キーでアイテム選択
        for (int i = 0; i < Inventory.Items.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectAS.Play();
                selectedItem = i;
                break;
            }
        }

        //マウスホイールでアイテム選択
        if (Input.mouseScrollDelta.y > 0)
        {
            selectAS.Play();
            selectedItem--;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            selectAS.Play();
            selectedItem++;
        }

        //選択アイテム番号が範囲外にならない処理
        selectedItem = (selectedItem + Inventory.Items.Count) % Inventory.Items.Count;
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
