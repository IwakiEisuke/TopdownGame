using UnityEngine;

public class DamageNumberEffectController : MonoBehaviour
{
    public Vector2 pos;
    Vector2 velocity;
    float gravity = 9.8f * 2;
    float power = 8f;

    private void Start()
    {
        Destroy(gameObject, 0.7f);
        velocity = Quaternion.Euler(0, 0, Random.Range(-20f, 20f)) * Vector2.up * power;
    }
    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(pos);
        velocity.y -= Time.deltaTime * gravity;
        pos += velocity * Time.deltaTime;
    }
}
