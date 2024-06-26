using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Environment/New EnvironmentSetting")]
public class EnvironmentSetting : ScriptableObject
{
    public LayeredEnvironmentSetting[] environmentSettings;
}

[Serializable]
public class LayeredEnvironmentSetting
{
    public int layer;
    public MapGenerationAlgorithm _mapAlgo;
    public ObjectGenerationAlgorithm _objectAlgo;
    public EntityGenerationAlgorithm _entityAlgo;
    public EnvGlobalLightSetting _envGlobalLightSetting;
}
