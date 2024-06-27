using UnityEngine;

[CreateAssetMenu(fileName = "EffectSettings", menuName = "Settings/EffectSettings")]
public class EffectSettings : ScriptableObject
{
    [SerializeField] GameObject damageNumberObject;
    public static GameObject _damageNumberObject;

    /// <summary>
    /// �Q�[���J�n���Ɏ��s
    /// </summary>
    public void Initialize()
    {
        _damageNumberObject = damageNumberObject;
    }

}
