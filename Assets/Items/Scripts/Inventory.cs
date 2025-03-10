using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] int[] initHaveItems;
    [SerializeField] List<InventoryItemData> items = new List<InventoryItemData>();
    [SerializeField] TextMeshProUGUI InventorySlotUI;
    [SerializeField] bool cheatMode;

    public static List<InventoryItemData> Items
    {
        get { return Instance.items; }
        set { Instance.items = value; }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            Items[i].ID = i;
            Items[i].amount = initHaveItems[i];
            if (cheatMode)
            {
                items[i].amount = 500;
            }
            Debug.Log($"{Items[i].ID} {Items[i].name} {Items[i].amount}");
        }
    }

    private void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        var index = 0;
        InventorySlotUI.text = "";
        foreach (var item in Items)
        {
            var select = "";
            if (ItemUseController.selectedItem == index)
            {
                for(int i = 0; i <  4 - item.amount.ToString().Length; i++)
                {
                    select += " ";
                }
                select += "<";
            }

            InventorySlotUI.text += $"<sprite name={item.UISprite.name}> : {item.amount}{select}\n";
            index++;
        }
    }

    public static void AddItem(int ID)
    {
        foreach (var item in Items)
        {
            if(item.ID == ID)
            {
                item.amount++;
            }
        }
    }

    public static void AddItem(InventoryItemData itemData)
    {
        foreach (var item in Items)
        {
            if (item == itemData)
            {
                item.amount++;
            }
        }
    }

    public static void AddSelectedItem()
    {
        AddItem(ItemUseController.selectedItem);
    }

    public static InventoryItemData GetInventoryItem(int ID)
    {
        return Items[ID];
    }

    public static InventoryItemData GetSelectedItem()
    {
        return GetInventoryItem(ItemUseController.selectedItem);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag(Tag.EntityDropItem))
    //    {
    //        AddItem(collision.GetComponent<ItemStatsContainer>().ID);
    //        Destroy(collision.gameObject);
    //    }
    //}
}
