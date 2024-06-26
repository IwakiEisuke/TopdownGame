using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemActionBase : ScriptableObject
{
    public abstract void Action(ItemUseController controller, InventoryItemData item);
}
