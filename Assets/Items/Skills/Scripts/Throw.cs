using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Throw")]
public class Throw : ItemActionBase
{
    public override void Action(ItemUseController controller, InventoryItemData item)
    {

        item.amount--;
        ItemObject.CreateAndThrow(controller.player, item, Tag.PlayerDropItem);
    }
}
