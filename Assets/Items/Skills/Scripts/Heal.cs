using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Heal")]
public class Heal : ItemActionBase
{
    public int effectSize;
    public override void Action(ItemUseController controller, InventoryItemData item)
    {
        var stats = PlayerController.Status;
        if(stats.hp < stats.maxHP)
        {
            PlayerController.HealHP(effectSize);
            item.amount--;
        }
    }
}
