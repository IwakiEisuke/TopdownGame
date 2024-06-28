using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Throw")]
public class Throw : ItemActionBase
{
    [SerializeField] AudioClip throwSE;
    public override void Action(ItemUseController controller, InventoryItemData item)
    {
        item.amount--;
        var throwItem = ItemObjectCreator.CreateAndThrow(controller.player, item, Tag.PlayerDropItem);
        var audio = throwItem.AddComponent<AudioSource>();
        audio.clip = throwSE;
        audio.Play();
        Destroy(audio, throwSE.length);
    }
}
