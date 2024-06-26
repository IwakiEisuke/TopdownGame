using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// レシピUIをレシピから作成するクラス
/// </summary>
public class RecipeUICreator : MonoBehaviour
{
    [SerializeField] GameObject recipeRowUI;

    public void CreateItemUI(List<InventoryItemData> items)
    {
        foreach (var item in items)
        {
            for (int i = 0; i < item.recipes.Length; i++)
            {
                var row = Instantiate(recipeRowUI, gameObject.transform);
                var manager = row.GetComponent<RecipeUIManager>();
                manager.CreateItemRecipeUI(item, i);
            }
        }
    }

    public void CreateTransformUI(TileBase equipment)
    {
        var items = Inventory.Items;
        foreach (var item in items)
        {
            for (int i = 0; i < item.recipesTransform.Length; i++)
            {
                if (item.recipesTransform[i].requireFacilities.Contains(equipment))
                {
                    var row = Instantiate(recipeRowUI, gameObject.transform);
                    var manager = row.GetComponent<RecipeUIManager>();
                    manager.CreateTransformRecipeUI(item, i);
                }
            }
        }
    }

    public void CreateTileUI(List<TileObject> tileObjects)
    {
        foreach (var tile in tileObjects)
        {
            var row = Instantiate(recipeRowUI, gameObject.transform);
            var manager = row.GetComponent<RecipeUIManager>();
            manager.CreateTileRecipeUI(tile);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
