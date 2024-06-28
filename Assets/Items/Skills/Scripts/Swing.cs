using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Swing")]
public class Swing : ItemActionBase
{
    [SerializeField] GameObject swingObj;
    public override void Action(ItemUseController controller, InventoryItemData item)
    {
        var dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - controller.player.transform.position;
        dir.z = 0;
        dir = dir.normalized;

        var itemInstance = Instantiate(swingObj, controller.player.transform);
        itemInstance.transform.up = dir;
        var swing = itemInstance.GetComponent<SwingController>();
        swing.basisAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        controller.coolTime = swing.swingDuration;
        var stats = itemInstance.GetComponentInChildren<ItemStatsContainer>();
        stats.atk = item.status.atk;
        stats.ID = item.ID;
    }
}
