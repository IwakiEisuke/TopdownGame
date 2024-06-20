using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public int[] initHaveItems;
    public TextMeshProUGUI InventorySlotUI;

    [SerializeField] private List<InventoryItemData> items = new List<InventoryItemData>();
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
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            Items[i].ID = i;
            Items[i].amount = initHaveItems[i];
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
            InventorySlotUI.text += item.name + " : " + item.amount + "\n";
            index++;
        }
    }

    public static void AddItemFromID(int ID)
    {
        foreach (var item in Items)
        {
            if(item.ID == ID)
            {
                item.amount++;
            }
        }
    }

    public static InventoryItemData GetInventoryItem(int ID)
    {
        return Items[ID];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.EntityDropItem))
        {
            AddItemFromID(collision.GetComponent<ItemController>().ID);
            Destroy(collision.gameObject);
        }

    }
}
