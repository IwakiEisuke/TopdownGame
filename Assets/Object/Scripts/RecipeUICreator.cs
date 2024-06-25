using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// レシピUIをレシピから作成するクラス
/// </summary>
public class RecipeUICreator : MonoBehaviour
{
    public List<InventoryItemData> items = new();
    public List<TileObject> tileObjects = new();
    [SerializeField] GameObject recipeUI;
    // Start is called before the first frame update
    void Start()
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
