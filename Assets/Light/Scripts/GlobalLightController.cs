using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalLightController : MonoBehaviour
{
    [SerializeField] Light2D lig;
    [SerializeField] VolumeProfile volume;
    public EnvGlobalLightSetting setting;
    [SerializeField] float t;
    ColorAdjustments colorAdj;

    private void Start()
    {
        volume.TryGet(out colorAdj);
    }
    void Update()
    {
        setting.UpdateLight(lig, colorAdj, t);

        t += Time.deltaTime * 2;
    }
}
