using System.Collections;
using TMPro;
using UnityEngine;

public class DamageNumberEffect : MonoBehaviour
{
    [SerializeField] AudioClip damageSE, healSE;
    GameObject canvas, numberObj;
    Vector2 pos;
    private void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    /// <summary>
    /// ダメージ表記を生成して初期化するメソッド。アニメーションは別
    /// </summary>
    /// <param name="amount"></param>
    public void CreateDamageNumberObject(int amount)
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(ResetColor());
        numberObj = Instantiate(EffectSettings._damageNumberObject, canvas.transform);
        numberObj.GetComponent<AudioSource>().PlayOneShot(damageSE);
        numberObj.GetComponent<DamageNumberEffectController>().pos = transform.position;
        var tmp = numberObj.GetComponent<TextMeshProUGUI>();
        tmp.text = amount.ToString();
        if (amount >= PlayerController.Status.maxHP / 2)
        {
            tmp.fontSize = 54;
        }
        tmp.color = Color.red;
    }

    public void CreateHealNumberObject(int amount)
    {
        //GetComponent<SpriteRenderer>().color = Color.green;
        StartCoroutine(ResetColor());
        numberObj = Instantiate(EffectSettings._damageNumberObject, canvas.transform);
        numberObj.GetComponent<AudioSource>().PlayOneShot(healSE);
        numberObj.GetComponent<DamageNumberEffectController>().pos = transform.position;
        var tmp = numberObj.GetComponent<TextMeshProUGUI>();
        tmp.text = amount.ToString();
        if (amount >= PlayerController.Status.maxHP / 2)
        {
            tmp.fontSize = 72;
        }
        tmp.color = Color.green;
    }

    IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(0.04f); // 2/60秒ぐらい
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
