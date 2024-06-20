using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance { get; private set; }
    [SerializeField] private TileSettings tileSettings;
    public static TileSettings TileSettings
    {
        get => Instance.tileSettings;
        set => Instance.tileSettings = value;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);
    }
}
