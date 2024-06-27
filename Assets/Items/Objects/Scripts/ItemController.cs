using UnityEngine;

public class ItemController : MonoBehaviour
{
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーアイテム取得処理
        if (CompareTag(Tag.EntityDropItem) && collision.CompareTag(Tag.Player))
        {
            Inventory.AddItem(GetComponent<ItemStatsContainer>().ID);
            Destroy(gameObject);
        }

        //敵ヒット処理
        if (collision.CompareTag(Tag.Entity))
        {
            if (rb != null)
            {
                //一定以上の運動速度を持ってないと判定しない（移動の速さが1以上、または回転の速さが100以上であれば判定）
                if (rb.velocity.magnitude < 1 && Mathf.Abs(rb.angularVelocity) < 100)
                {
                    return;
                }
            }
            var entity = collision.GetComponent<EntityController>();
            //プレイヤーの攻撃に当たった場合ダメージを受けて攻撃を破壊する
            if (CompareTag(Tag.PlayerDropItem) || CompareTag(Tag.PlayerAttack))
            {
                var itemController = GetComponent<ItemStatsContainer>();
                entity.TakeDamage(itemController);

                if (CompareTag(Tag.PlayerDropItem))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
