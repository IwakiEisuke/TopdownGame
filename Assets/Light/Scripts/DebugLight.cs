using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DebugLight : MonoBehaviour
{
    [SerializeField] Slider _sliderPostExposure;
    [SerializeField] Slider _sliderIntensity;
    [SerializeField] Volume _volume;
    [SerializeField] Light2D _light;
    [SerializeField] Text _txtPostExposure;
    [SerializeField] Text _txtIntensity;

    void Start()
    {
        _volume.profile.TryGet(out ColorAdjustments colorAdjustments);
        _sliderPostExposure.onValueChanged.AddListener(x =>
        {
            colorAdjustments.postExposure.SetValue(new FloatParameter(x));
            _txtPostExposure.text = x.ToString("0.0000");
        });
        _sliderIntensity.onValueChanged.AddListener(x =>
        {
            _light.intensity = x;
            _txtIntensity.text = x.ToString("0.0000");
        });
    }

    public void ToggleLight()
    {
        _light.enabled = !_light.enabled;
    }
}
