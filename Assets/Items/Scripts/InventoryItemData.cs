using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewItem", menuName = "Object/New Item")]
public class InventoryItemData : ScriptableObject
{
    [HideInInspector] public int ID, amount;
    public Sprite sprite;
    public Sprite UISprite;
    public TileBase[] itemTiles;
    public Recipe[] recipes;
    public TransformRecipe[] recipesTransform;
    public ItemStatus status;
    public GameObject light;
    public ItemActionBase[] skills;
    public AudioClip pickupSE;
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

    /// <summary>
    /// 要求されたアイテムを持っている場合消費し、actを実行します
    /// </summary>
    /// <param name="recipe"></param>
    /// <param name="act"></param>
    public static void RecipeProcess(Recipe recipe, Action act)
    {
        if (IsCreatable(recipe))
        {
            ConsumeItems(recipe);
            act();
        }
    }

    /// <summary>
    /// 要求されたアイテムを持っているかチェックした後消費します
    /// </summary>
    /// <param name="recipe"></param>
    public static void RecipeProcess(Recipe recipe)
    {
        if (IsCreatable(recipe))
        {
            ConsumeItems(recipe);
        }
    }

    /// <summary>
    /// Recipeで要求されたアイテムを消費する。所持数を事前にチェックする場合IsCreatableメソッドが必要。
    /// </summary>
    /// <param name="recipe"></param>
    public static void ConsumeItems(Recipe recipe)
    {
        foreach (var require in recipe)
        {
            require.item.amount -= require.amount;
        }
    }

    /// <summary>
    /// 要求アイテムを全て持っているかチェックする
    /// </summary>
    /// <param name="recipe"></param>
    /// <returns></returns>
    public static bool IsCreatable(Recipe recipe)
    {
        if (recipe == null)
        {
            return false;
        }
        else
        {
            foreach (var require in recipe)
            {
                //require.item.amountはitemの所持数。require.amountは要求数。
                if (require.item.amount < require.amount)
                {
                    return false;
                }
            }
            return true;
        }
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