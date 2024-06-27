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
    /// �_���[�W�\�L�𐶐����ď��������郁�\�b�h�B�A�j���[�V�����͕�
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
