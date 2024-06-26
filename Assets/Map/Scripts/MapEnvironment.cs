using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Environment/New Environment")]
public class MapEnvironment : ScriptableObject
{
    [Header("Environment Settings")]
    public MapGenerationAlgorithm _mapAlgo;
    public ObjectGenerationAlgorithm _objectAlgo;
    public EntityGenerationAlgorithm _entityAlgo;
    public EnvGlobalLightSetting _envGlobalLightSetting;
    public EnvironmentSetting _environmentSetting;

    [Header("Elevator Settings")]
    public int _maxFloor;
    public AppearanceEnvironmentSetting[] _appearSettings;
    public TileBase stairTile;
}

[Serializable]
public class AppearanceEnvironmentSetting
{
    public MapEnvironment _appearEnv;
    public int _onlyFloor;
    public int _onlyLocalFloor;
    public bool _moveHorizon;
    public bool _moveUp;
    public float _rarity;
}