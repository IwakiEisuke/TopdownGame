using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// レシピUIをレシピから作成するクラス
/// </summary>
public class RecipeUICreator : MonoBehaviour
{
    [SerializeField] GameObject recipeUI;

    public void CreateItemUI(List<InventoryItemData> items)
    {
        foreach (var item in items)
        {
            for (int i = 0; i < item.recipes.Length; i++)
            {
                var ui = Instantiate(recipeUI, gameObject.transform);
                var manager = ui.GetComponent<RecipeUIManager>();
                manager.CreateItemRecipeUI(item, i);
            }
        }
    }

    public void CreateTransformUI(List<InventoryItemData> items)
    {
        foreach (var item in items)
        {
            for (int i = 0; i < item.recipesTransform.Length; i++)
            {
                var ui = Instantiate(recipeUI, gameObject.transform);
                var manager = ui.GetComponent<RecipeUIManager>();
                manager.CreateTransformRecipeUI(item, i);
            }
        }
    }

    public void CreateTileUI(List<TileObject> tileObjects)
    {
        foreach (var tile in tileObjects)
        {
            var ui = Instantiate(recipeUI, gameObject.transform);
            var manager = ui.GetComponent<RecipeUIManager>();
            manager.CreateTileRecipeUI(tile);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
