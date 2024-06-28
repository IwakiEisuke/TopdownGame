using System;
using UnityEngine;

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
    /// ���ݑI�����Ă���A�C�e������A�ǂ̎�ނ̉��Ԗڂ̃��V�s���g�p���邩�w�肵�쐬����B�A�C�e��������Ȃ��ꍇ�̓L�����Z�������
    /// </summary>
    /// <param name="recipeType"></param>
    /// <param name="recipeID"></param>
    public static void CreateSelectedItem(RecipeType recipeType, int recipeID)
    {
        var selectedItem = Inventory.GetSelectedItem();

        var recipe = SelectRecipe(selectedItem, recipeType, recipeID);
        Recipe.RecipeProcess(recipe, () => Inventory.AddSelectedItem());
    }

    public static void CreateTile(Vector3Int pos, TileObject obj)
    {
        var recipe = obj.Recipe;
        var tile = obj.Tile;
        Recipe.RecipeProcess(recipe, () => MapManager._currentObjectMap.SetTile(pos, tile));
    }

    private static Recipe SelectRecipe(InventoryItemData item, RecipeType recipeType, int recipeID)
    {
        switch (recipeType)
        {
            case RecipeType.Craft:
                if (recipeID < item.recipes.Length)
                {
                    return item.recipes[recipeID];
                }
                return null;
            case RecipeType.Transform:
                if (recipeID < item.recipesTransform.Length)
                {
                    return item.recipesTransform[recipeID];
                }
                return null;
            default:
                return null;
        }
    }

    
}

public enum RecipeType
{
    Craft,
    Transform
}


