using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CreateFromRecipe : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateSelectedItem(RecipeType.Craft, 0);
        }
    }

    /// <summary>
    /// 現在選択しているアイテムから、どの種類の何番目のレシピを使用するか指定し作成する。アイテムが足りない場合はキャンセルされる
    /// </summary>
    /// <param name="recipeType"></param>
    /// <param name="recipeID"></param>
    public static void CreateSelectedItem(RecipeType recipeType, int recipeID)
    {
        var selectedItem = Inventory.GetSelectedItem();

        var recipe = SelectRecipe(selectedItem, recipeType, recipeID);
        RecipeProcess(recipe, () => Inventory.AddSelectedItem());
    }

    public static void CreateTile(Vector3Int pos, TileObject obj)
    {
        var recipe = obj.Recipe;
        var tile = obj.Tile;
        RecipeProcess(recipe, () => MapManager._currentObjectMap.SetTile(pos, tile));
    }

    private static Recipe SelectRecipe(InventoryItemData item, RecipeType recipeType, int recipeID)
    {
        switch (recipeType)
        {
            case RecipeType.Craft:
                return item.recipes[recipeID];
            case RecipeType.Transform:
                return item.recipesTransform[recipeID];
            default:
                return null;
        }
    }

    /// <summary>
    /// 要求されたアイテムを持っているかチェックした後消費し、actを実行します
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

    private static void ConsumeItems(Recipe recipe)
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
    private static bool IsCreatable(Recipe recipe)
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

public enum RecipeType
{
    Craft,
    Transform
}


