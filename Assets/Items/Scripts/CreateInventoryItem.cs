using UnityEngine;

public class CreateInventoryItem : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateItem(RecipeType.Craft, 0);
        }
    }

    public static void CreateItem(int recipeType, int recipeID)
    {
        Recipe[] recipes = new Recipe[0];
        var selectedItem = Inventory.GetInventoryItem(ItemUseController.selectedItem);

        switch (recipeType)
        {
            case 0:
                recipes = selectedItem.recipes;
                break;
            case 1:
                recipes = selectedItem.recipesTransform;
                break;
        }

        if (recipes.Length > 0)
        {
            var recipe = recipes[recipeID];
            var isCreatable = true;

            foreach (var require in recipe)
            {
                if (require.item.amount < require.amount)
                {
                    isCreatable = false;
                }
            }

            if (isCreatable)
            {
                foreach (var require in recipe)
                {
                    require.item.amount -= require.amount;
                    Inventory.AddItemFromID(ItemUseController.selectedItem);
                }
            }
        }
    }
}

public struct RecipeType
{
    public const int Craft = 0;
    public const int Equipment = 1;
}


