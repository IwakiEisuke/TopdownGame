using UnityEngine;

[CreateAssetMenu(fileName = "EffectSettings", menuName = "Settings/EffectSettings")]
public class EffectSettings : ScriptableObject
{
    [SerializeField] GameObject damageNumberObject;
    public static GameObject _damageNumberObject;

    /// <summary>
    /// ゲーム開始時に実行
    /// </summary>
    public void Initialize()
    {
        _damageNumberObject = damageNumberObject;
    }

}
