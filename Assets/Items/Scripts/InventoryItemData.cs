using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewItem", menuName = "Object/New Item")]
public class InventoryItemData : ScriptableObject
{
    [HideInInspector] public int ID, amount;
    public Sprite sprite;
    public Sprite UISprite;
    public TileBase[] itemTiles;
    public bool isThrowable;
    public Recipe[] recipes;
    public TransformRecipe[] recipesTransform;
    public ItemStatus status;
    public GameObject light;
    public ItemActionBase[] skills;
}

[Serializable]
public class ItemStatus
{
    public int atk;
    public float mass = 1;
    public float power = 10;
    public float torque = 1;
    public float linearDrag = 3;
    public float angularDrag = 3;
}

[Serializable]
public class Recipe : IEnumerable<ConsumeItemSetting>
{
    public ConsumeItemSetting[] requireItems;

    // IEnumerable<ConsumeItemSetting>のGetEnumeratorを実装
    public IEnumerator<ConsumeItemSetting> GetEnumerator()
    {
        // requireItems配列の要素を列挙する
        foreach (var item in requireItems)
        {
            yield return item;
        }
    }

    // 非ジェネリックなIEnumeratorのGetEnumeratorを実装
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

[Serializable]
public class TransformRecipe : Recipe
{
    public TileBase[] requireFacilities;
}

[Serializable]
public class ConsumeItemSetting
{
    /// <summary>
    /// 要求アイテム
    /// </summary>
    public InventoryItemData item;
    /// <summary>
    /// 要求数
    /// </summary>
    public int amount;
}