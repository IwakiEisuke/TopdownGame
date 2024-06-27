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
            //プレイヤーの攻撃に当たった場合ダメージを受けて攻撃を破壊する
            if (collision.gameObject.CompareTag(Tag.PlayerDropItem) || collision.gameObject.CompareTag(Tag.PlayerAttack))
            {
                var itemController = collision.GetComponent<ItemStatsContainer>();
                TakeDamage(itemController);
                if (collision.gameObject.CompareTag(Tag.PlayerDropItem))
                {
                    Destroy(collision.gameObject);
                }
            }

            if (status.hp <= 0)
            {
                foreach (var drop in entityData.drops)
                {
                    for (int i = 0; i < drop.amount + Random.Range(0, drop.addFluctuation + 1); i++)
                    {
                        ItemObject.CreateAndDrop(gameObject, drop.dropItem, 2, 0, Tag.EntityDropItem);
                    }
                }
                Destroy(gameObject);
            }
        }

        if (collision.CompareTag(Tag.Player))
        {
            PlayerController.TakeDamage(entityData.atk);
        }
    }

    private void TakeDamage(ItemStatsContainer itemController)
    {
        var damage = (int)Math.Clamp((itemController.atk + PlayerController.Status.bonusAtk - entityData.def), 0, 9999);
        status.hp -= damage;
        GetComponent<DamageNumberEffect>().CreateDamageNumberObject(damage);
    }
}

[Serializable]
public class EntityStatus
{
    public float hp;
}
