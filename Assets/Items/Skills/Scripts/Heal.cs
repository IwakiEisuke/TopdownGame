using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Heal")]
public class Heal : ItemActionBase
{
    public int effectSize;
    public override void Action(ItemUseController controller, InventoryItemData item)
    {
        PlayerController.Status.hp += effectSize;
        item.amount--;
    }
}
