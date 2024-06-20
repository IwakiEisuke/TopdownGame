using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemObject : MonoBehaviour
{
    /// <summary>
    /// itemBaseを代入してください
    /// </summary>
    public static GameObject itemBase;

    public static GameObject Create(GameObject position, InventoryItemData itemData, string itemMode = "Untagged")
    {
        var currentObjectsParent = MapManager._currentObjectsParent;

        var item = Instantiate(itemBase, currentObjectsParent.transform);
        item.name = itemData.name;
        item.tag = itemMode;
        item.GetComponent<ItemController>().atk = itemData.status.atk + PlayerController.Status.bonusAtk;
        item.GetComponent<ItemController>().ID = itemData.ID;

        item.GetComponent<SpriteRenderer>().sprite = itemData.sprite;
        item.transform.position = position.transform.position;
        item.transform.Rotate(Vector3.forward * Random.Range(0, 360));

        var col = item.AddComponent<PolygonCollider2D>();
        col.excludeLayers = LayerMask.GetMask("Entity");

        var rb = item.GetComponent<Rigidbody2D>();
        rb.mass = itemData.status.mass;

        if(itemData.light != null)
        {
            Instantiate(itemData.light, item.transform);
        }

        return item;
    }

    public static void CreateAndThrow(GameObject fromObj, InventoryItemData itemData, string itemMode)
    {
        var item = Create(fromObj, itemData, itemMode);

        //プレイヤーからマウスへの方向ベクトルを取得
        var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - fromObj.transform.position;
        direction.z = 0;
        direction.Normalize();

        //投擲処理
        var rb = item.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * itemData.status.power, ForceMode2D.Impulse);
        rb.AddTorque(itemData.status.torque, ForceMode2D.Impulse);
        rb.drag = itemData.status.linearDrag;
        rb.angularDrag = itemData.status.angularDrag;
    }

    public static void CreateAndDrop(GameObject fromObj, InventoryItemData itemData, float power, float torque, string itemMode)
    {
        var item = Create(fromObj, itemData, itemMode);

        //ランダムな角度の方向ベクトルを取得
        var direction = Quaternion.Euler(Vector3.forward * Random.Range(0, 360)) * Vector3.up;

        //投擲処理
        var rb = item.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * power, ForceMode2D.Impulse);
        rb.AddTorque(torque, ForceMode2D.Impulse);
    }
}
