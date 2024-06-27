using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] public PlayerStatus status;
    [SerializeField] DamageNumberEffect damageNumberEffect;
    public static PlayerStatus Status
    {
        get { return Instance.status; }
        set { Instance.status = value; }
    }
    public static DamageNumberEffect DamageNumberEffect { get { return Instance.damageNumberEffect; } }
    public static Transform Transform { get { return Instance.transform; } }

    [SerializeField] Slider hpBar;
    private static Slider HPBar { get { return Instance.hpBar; } }

    Rigidbody2D rb;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HPBarEffect();
    }

    private void Update()
    {
        if (rb != null)
        {
            Move();
        }
    }

    private void Move()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var direction = new Vector2(horizontal, vertical);
        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }
        rb.velocity = direction * Status.speed;

        var localBounds = MapManager._currentGroundMap.localBounds;
        var max = localBounds.max;
        var min = localBounds.min;

        var pos = transform.position;

        var pSize = new Vector2(0.5f, 0.5f);

        transform.position = new Vector2(Mathf.Clamp(pos.x, min.x + pSize.x, max.x - pSize.x), Mathf.Clamp(pos.y, min.y + pSize.y, max.y - pSize.y));
    }

    public static void TakeDamage(float amount)
    {
        var damage = (int)Math.Clamp(amount - Status.bonusDef, 0, 9999);
        Status.hp -= damage;
        DamageNumberEffect.CreateDamageNumberObject(damage);
        StatusEffect();
    }

    public static void HealHP(float amount)
    {
        var heal = (int)Math.Clamp(amount, 0, 9999);
        Status.hp = (int)Math.Clamp(Status.hp + heal, 0, Status.maxHP);
        DamageNumberEffect.CreateHealNumberObject(heal);
        StatusEffect();
    }

    private static void StatusEffect()
    {
        HPBarEffect();
        if (Status.hp <= 0)
        {
            Instance.gameObject.SetActive(false);
        }
    }

    private static void HPBarEffect()
    {
        HPBar.value = Status.hp / Status.maxHP;
    }
}
