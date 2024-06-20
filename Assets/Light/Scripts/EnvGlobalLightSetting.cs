using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "New EnvLight", menuName = "Environment/New EnvLight")]
public class EnvGlobalLightSetting : ScriptableObject
{
    [SerializeField] Gradient sunLightColors;
    [SerializeField] AnimationCurve sunLightIntensity;
    [SerializeField] AnimationCurve expose;
    [SerializeField] float dayTime;

    public void UpdateLight(Light2D lig, ColorAdjustments colorAdj, float t)
    {
        var cycleTime = t / dayTime;
        lig.color = sunLightColors.Evaluate(Mathf.PingPong(cycleTime, 1));
        lig.intensity = sunLightIntensity.Evaluate(Mathf.PingPong(cycleTime, 1));
        colorAdj.postExposure.value = expose.Evaluate(Mathf.PingPong(cycleTime, 1));
    }
}
