using System;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] public PlayerStatus status;
    public static PlayerStatus Status
    {
        get { return Instance.status; }
        set { Instance.status = value; }
    }
    public static Transform Transform { get { return Instance.transform; } }

    public TextMeshProUGUI statusUI;

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
    }

    private void Update()
    {
        if (rb != null)
        {
            Move();
        }
        StatusEffect();
        if (statusUI != null)
        {
            UpdateText();
        }
    }

    private void UpdateText()
    {
        statusUI.text = $"HP : {status.hp}";
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

    public static void Damage(float damage)
    {
        Status.hp -= (int)Math.Clamp(damage - Status.bonusDef, 0, 9999);
    }

    private void StatusEffect()
    {
        if (status.hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag(Tag.Entity))
    //    {
    //        Status.hp -= collision.GetComponent<EntityController>().entityData.atk;
    //    }
    //}
}
