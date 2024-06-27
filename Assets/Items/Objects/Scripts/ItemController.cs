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
        //�v���C���[�A�C�e���擾����
        if (CompareTag(Tag.EntityDropItem) && collision.CompareTag(Tag.Player))
        {
            Inventory.AddItem(GetComponent<ItemStatsContainer>().ID);
            Destroy(gameObject);
        }

        //�G�q�b�g����
        if (collision.CompareTag(Tag.Entity))
        {
            if (rb != null)
            {
                //���ȏ�̉^�����x�������ĂȂ��Ɣ��肵�Ȃ��i�ړ��̑�����1�ȏ�A�܂��͉�]�̑�����100�ȏ�ł���Δ���j
                if (rb.velocity.magnitude < 1 && Mathf.Abs(rb.angularVelocity) < 100)
                {
                    return;
                }
            }
            var entity = collision.GetComponent<EntityController>();
            //�v���C���[�̍U���ɓ��������ꍇ�_���[�W���󂯂čU����j�󂷂�
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
