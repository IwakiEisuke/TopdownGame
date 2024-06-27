using UnityEngine;

public class ItemController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CompareTag(Tag.EntityDropItem) && collision.CompareTag(Tag.Player))
        {
            Inventory.AddItem(GetComponent<ItemStatsContainer>().ID);
            Destroy(gameObject);
        }
    }
}
