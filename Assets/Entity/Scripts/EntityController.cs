using DG.Tweening;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityController : MonoBehaviour
{
    public float speed;
    public EntityData entityData;
    public EntityStatus status;
    private AIBase AI;

    void Start()
    {
        AI = entityData.AI;
        
        status.hp = entityData.hp;

        if (AI != null)
        {
            AI.Act(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tag.PlayerDropItem) &&  collision.GetComponent<ItemController>() != null)
        {
            var itemController = collision.GetComponent<ItemController>();
            var damage = itemController.atk + PlayerController.Status.bonusAtk - entityData.def;

            if (damage >= 0)
            {
                status.hp -= damage;
            }

            Destroy(collision.gameObject);
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
    }

    private void OnDestroy()
    {
        var sequence = AI.GetSequence();
        sequence.Kill();
    }
}

[Serializable]
public class EntityStatus
{
    public float hp;
}
