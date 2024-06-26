using System.Collections.Generic;
using UnityEngine;

public class RecipeUIStarter : MonoBehaviour
{
    [SerializeField] List<TileObject> tileDatas;
    [SerializeField] List<InventoryItemData> items;
    [SerializeField] RecipeUICreator recipeUICreator;

    [SerializeField] bool createTileUI, createItemUI;
    // Start is called before the first frame update
    void Start()
    {
        if (createTileUI)
        {
            recipeUICreator.CreateTileUI(tileDatas);
        }
        if (createItemUI)
        {
            if (items.Count == 0)
            {
                items = Inventory.Items;
            }
            recipeUICreator.CreateItemUI(items);
        }
    }
}
