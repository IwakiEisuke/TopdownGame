using DG.Tweening;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityController : MonoBehaviour
{
    public float speed;
    public EntityData entityData;
    public EntityStatus status;

    void Start()
    {

        gameObject.AddComponent(Type.GetType(entityData.stateMachine.name));
        
        status.hp = entityData.hp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ItemStatsContainer>() != null)
        {
            var itemController = collision.GetComponent<ItemStatsContainer>();
            var damage = itemController.atk + PlayerController.Status.bonusAtk - entityData.def;

            if (damage >= 0)
            {
                status.hp -= damage;
            }

            if (collision.gameObject.CompareTag(Tag.PlayerDropItem))
            {
                Destroy(collision.gameObject);
            }

            if (status.hp <= 0)
            {
                foreach (var drop in entityData.drops)
                {
                    for (int i = 0; i < drop.amount + Random.Range(0, drop.fluctuation); i++)
                    {
                        ItemObject.CreateAndDrop(gameObject, drop.dropItem, 2, 0, Tag.EntityDropItem);
                    }
                }
                Destroy(gameObject);
            }
        }

        if (collision.CompareTag(Tag.Player))
        {
            PlayerController.Damage(entityData.atk);
        }
    }
}

[Serializable]
public class EntityStatus
{
    public float hp;
}
