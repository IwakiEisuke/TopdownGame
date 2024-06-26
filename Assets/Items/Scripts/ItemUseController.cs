using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class ItemUseController : MonoBehaviour
{
    public GameObject player;
    public GameObject itemBase;
    public float power;
    public float torque;
    public static int selectedItem;
    public TextMeshProUGUI text;

    private void Start()
    {
        ItemObject.itemBase = itemBase;
        SetItemPointer(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var item = Inventory.GetSelectedItem();

            if (item.amount > 0)
            {
                foreach (var skill in item.skills)
                {
                    skill.Action(this, item);
                }
            }
        }

        for (int i = 0; i < Inventory.Items.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectedItem = i;

                SetItemPointer(selectedItem);
                break;
            }
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            selectedItem--;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            selectedItem++;
        }

        selectedItem = (selectedItem + Inventory.Items.Count) % Inventory.Items.Count;

    }

    private void Throw(InventoryItemData itemData)
    {
        ItemObject.CreateAndThrow(player, itemData, Tag.PlayerDropItem);
    }

    private void SetItemPointer(int i)
    {
        text.text = "<-";
        for (int j = Inventory.Items.Count; j > i; j--)
        {
            text.text += "\n";
        }
    }
}
