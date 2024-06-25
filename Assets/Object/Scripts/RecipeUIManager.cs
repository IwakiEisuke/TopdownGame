using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// UIのテキストをレシピ通りに書き換えるクラス
/// </summary>
public class RecipeUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] Button button;

    public void CreateItemRecipeUI(InventoryItemData toItem, int recipeIndex)
    {
        var recipe = toItem.recipes[recipeIndex];
        var requires = recipe.requireItems;

        var newText = $"<size=50><sprite name={toItem.UISprite.name}> {toItem.name}\r\n";
        foreach (var require in requires)
        {
            newText += $"<size=36><sprite name={require.item.UISprite.name}> x{require.amount}  ";
        }
        textMesh.text = newText;

        button.onClick.AddListener(() => CreateFromRecipe.RecipeProcess(recipe, () => Inventory.AddItem(toItem)));
    }

    public void CreateTransformRecipeUI(InventoryItemData toItem, int recipeIndex)
    {
        var recipe = toItem.recipesTransform[recipeIndex];
        var requires = recipe.requireItems;

        var newText = $"<size=50><sprite name={toItem.UISprite.name}> {toItem.name}\r\n";
        foreach (var require in requires)
        {
            newText += $"<size=36><sprite name={require.item.UISprite.name}> x{require.amount}  ";
        }
        textMesh.text = newText;

        button.onClick.AddListener(() => CreateFromRecipe.RecipeProcess(recipe, () => Inventory.AddItem(toItem)));
    }

    public void CreateTileRecipeUI(TileObject toTile)
    {
        var recipe = toTile.Recipe;
        var requires = recipe.requireItems;

        var newText = $"<size=50><sprite name={toTile.TileUISprite.name}> {toTile.name}\r\n";
        foreach (var require in requires)
        {
            newText += $"<size=36><sprite name={require.item.UISprite.name}> x{require.amount}  ";
        }
        textMesh.text = newText;

        button.onClick.AddListener(() => GameObject.Find("Player").GetComponentInChildren<InteractUnderfootTile>().PlaceTile(toTile));
    }
}
