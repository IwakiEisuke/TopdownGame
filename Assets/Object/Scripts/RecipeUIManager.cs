using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIのテキストをレシピ通りに書き換えるクラス
/// </summary>
public class RecipeUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] Button button;

    public void CreateItemRecipeUI(InventoryItemData to, int recipeIndex)
    {
        var recipe = to.recipes[recipeIndex];
        var requires = recipe.requireItems;

        var newText = $"<size=50><sprite name={to.UISprite.name}> {to.name}\r\n";
        foreach (var require in requires)
        {
            newText += $"<size=36><sprite name={require.item.UISprite.name}> x{require.amount}  ";
        }
        textMesh.text = newText;

        button.onClick.AddListener(() => CreateFromRecipe.RecipeProcess(recipe, () => Inventory.AddItem(to)));
    }

    public void CreateTileRecipeUI(TileObject to)
    {
        var recipe = to.Recipe;
        var requires = recipe.requireItems;

        var newText = $"<size=50><sprite name={to.TileUISprite.name}> {to.name}\r\n";
        foreach (var require in requires)
        {
            newText += $"<size=36><sprite name={require.item.UISprite.name}> x{require.amount}  ";
        }
        textMesh.text = newText;

        button.onClick.AddListener(() => GameObject.Find("Player").GetComponentInChildren<InteractUnderfootTile>().PlaceTile(to));
    }
}
