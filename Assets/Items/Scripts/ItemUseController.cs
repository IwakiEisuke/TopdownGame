using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class ItemUseController : MonoBehaviour
{
    public GameObject player;
    public GameObject itemBase;
    public float power;
    public float torque;
    public static int selectedItem;
    public TextMeshProUGUI text;
    public float coolTime;

    private void Start()
    {
        ItemObjectCreator.itemBase = itemBase;
    }

    private void Update()
    {
        coolTime -= Time.deltaTime;
        //アイテム使用処理
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl)) && coolTime <= 0)
        {
            var item = Inventory.GetSelectedItem();

            if (item.amount > 0)
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
                selectedItem = i;
                break;
            }
        }

        //マウスホイールでアイテム選択
        if (Input.mouseScrollDelta.y > 0)
        {
            selectedItem--;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            selectedItem++;
        }

        //選択アイテム番号が範囲外にならない処理
        selectedItem = (selectedItem + Inventory.Items.Count) % Inventory.Items.Count;

    }
}
