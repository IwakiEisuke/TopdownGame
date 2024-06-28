using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Throw")]
public class Throw : ItemActionBase
{
    [SerializeField] AudioClip throwSE;
    [SerializeField] float SEVolume;
    public override void Action(ItemUseController controller, InventoryItemData item)
    {
        item.amount--;
        var throwItem = ItemObjectCreator.CreateAndThrow(controller.player, item, Tag.PlayerDropItem);
        if (throwSE != null)
        {
            var audio = throwItem.AddComponent<AudioSource>();
            audio.clip = throwSE;
            audio.volume = SEVolume;
            audio.Play();
            Destroy(audio, throwSE.length);
        }
    }
}
