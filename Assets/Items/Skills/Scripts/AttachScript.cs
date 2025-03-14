using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/AttackScript")]
public class AttackScript : ItemActionBase
{
    [SerializeField] string attachScriptType;
    public override void Action(ItemUseController controller, InventoryItemData item)
    {
        var itemInstance = ItemObjectCreator.Create(controller.player, item);
        var script = itemInstance.AddComponent(Type.GetType(attachScriptType));
    }
}
