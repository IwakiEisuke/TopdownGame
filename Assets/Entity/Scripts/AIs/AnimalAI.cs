using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Animal")]
public class AnimalAI : AIBase
{
    [SerializeField] float duration;
    [SerializeField] float distance;
    [SerializeField] Sequence sequence;

    public override void Act(GameObject entity)
    {
        sequence = DOTween.Sequence();
        var rb = entity.GetComponent<Rigidbody2D>();

        rb.velocity = Vector3.zero;

        Vector2 randomDirection = Quaternion.Euler(0, 0, Random.Range(0, 360)) * Vector2.up;
        var moveVector = randomDirection * distance;

        sequence = DOTween.Sequence();
        for (int i = 0; i < Random.Range(1, 5); i++)
        {
            sequence.Append(DOTween.To(() => randomDirection, x => rb.velocity = x, Vector2.zero, duration).SetEase(Ease.InQuint));
        }

        var speed = distance / duration;
        sequence.AppendInterval(Random.Range(speed / 2, speed));

        sequence.Play().OnComplete(() =>
            {
                if(sequence.IsActive())
                {
                    Act(entity);
                }
            });
    }

    public override Sequence GetSequence()
    {
        return sequence;
    }
}
