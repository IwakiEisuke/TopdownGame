using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumberEffect : MonoBehaviour
{
    GameObject canvas, damageObj;
    Vector2 pos;
    private void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    /// <summary>
    /// ダメージ表記を生成して初期化するメソッド。アニメーションは別
    /// </summary>
    /// <param name="damage"></param>
    public void CreateDamageNumberObject(int damage)
    {
        Debug.Log(damage);
        GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(ResetColor());
        damageObj = Instantiate(EffectSettings._damageNumberObject, canvas.transform);
        damageObj.GetComponent<DamageNumberEffectController>().pos = transform.position;
        damageObj.GetComponent<TextMeshProUGUI>().text = damage.ToString();
    }

    IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(0.04f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
