using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private static MapManager Instance { get; set; }

    public static Tilemap _currentGroundMap;
    public static Tilemap _currentObjectMap;
    public static GameObject _currentObjectsParent;

    [SerializeField] List<MapData> maps;
    public static List<MapData> Maps { get => Instance.maps; private set => Instance.maps = value; }

    public static MapData _currentMapData;
    public static int _currentLayer;
    public static int _currentLocalLayer;
    public static List<Vector2Int> _currentStairsPos;

    [SerializeField] MapEnvironment _startEnv;
    [SerializeField] GlobalLightController lightController;
    private static Tilemap currentGroundMap;

    public static GlobalLightController LightController { get => Instance.lightController; private set => Instance.lightController = value; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        else if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(Instance);
    }

    public void StartProcess()
    {
        Maps = new List<MapData>();
        CreateStartMap(_startEnv);
    }

    public static void AddMap(MapData map)
    {
        Maps.Add(map);
    }

    /*
    static Tilemap SetGroundMap(Tilemap groundmap)
    {
        if (_currentMapData != null)
        {
            _currentGroundMap.gameObject.SetActive(false);
        }

        _currentGroundMap = groundmap;
        _currentGroundMap.gameObject.SetActive(true);
        return _currentGroundMap;
    }

    static Tilemap SetObjectMap(Tilemap objectmap)
    {
        if (_currentMapData != null)
        {
            _currentObjectMap.gameObject.SetActive(false);
        }

        _currentObjectMap = objectmap;
        _currentObjectMap.gameObject.SetActive(true);
        return _currentObjectMap;
    }

    static GameObject SetObjectsParent(GameObject objectsParent)
    {
        if (_currentMapData != null)
        {
            _currentObjectsParent.gameObject.SetActive(false);
        }

        _currentObjectsParent = objectsParent;
        _currentObjectsParent.SetActive(true);
        return _currentObjectsParent;
    }
    */

    public static void SetCurrentMap(MapData map)
    {
        //ゲーム開始時には設定先のオブジェクトが存在しないためnullチェックする
        //移動前のマップを非アクティブ化
        if (_currentMapData != null)
        {
            _currentGroundMap.gameObject.SetActive(false);
            _currentObjectMap.gameObject.SetActive(false);
            _currentObjectsParent.SetActive(false);
        }

        //移動先のマップをアクティブ化
        if (IsCreated(map._index))
        {
            _currentMapData = map;
            _currentGroundMap = map._groundmap;
            _currentObjectMap = map._objectmap;
            _currentObjectsParent = map._objectsParent;
            _currentGroundMap.gameObject.SetActive(true);
            _currentObjectMap.gameObject.SetActive(true);
            _currentObjectsParent.SetActive(true);
            if (LightController != null)
            {
                LightController.setting = map._env._envGlobalLightSetting;
            }
            else
            {
                Debug.LogWarning("LightControllerがアタッチされていません");
            }
            FollowTarget.SetBounds();
        }
        _currentLayer = map._layer;
        Debug.Log("currentLayer : " + _currentLayer);
    }

    public static void SetCurrentMap(int index)
    {
        MapData map = null;
        foreach (var _map in Maps)
        {
            if (_map._index == index) map = _map;
        }

        if (map != null)
        {
            SetCurrentMap(map);
        }
        else
        {
            throw new IndexOutOfRangeException($"index:{index}のマップは存在しません");
        }
    }

    public static MapData GetMap(int index)
    {
        return Maps[index];
    }

    public static int GetIndex(MapData map)
    {
        foreach (var _map in Maps)
        {
            if (map == _map) return _map._index;
        }
        return -1;
    }

    /// <summary>
    /// indexとlayerのみ設定したマップデータを生成しMapsリストに追加します
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static MapData CreateNullMap(int layer)
    {
        var map = new MapData(
                null,
                null,
                null,
                null,
                Maps.Count,
                layer
                );

        AddMap(map);
        return map;
    }

    /// <summary>
    /// ゲーム開始時用のマップ生成メソッド
    /// </summary>
    /// <param name="env"></param>
    /// <param name="layer"></param>
    /// <param name="setMapCurrent"></param>
    /// <returns></returns>
    public static MapData CreateStartMap(MapEnvironment env)
    {
        Tilemap O_map;
        Tilemap G_map;

        var debug_timer = Time.realtimeSinceStartup;
        var map = new MapData(
                G_map = env._objectAlgo.CreateMap(env, 0),
                O_map = env._mapAlgo.CreateMap(env),
                env._entityAlgo.SpawnEntity(env, G_map, O_map),
                env,
                Maps.Count,
                0
                );

        AddMap(map);
        SetCurrentMap(map);
        StairsCreator.StartProcess();
        PlaceStair(map);
        Debug.Log("生成時間:" + (Time.realtimeSinceStartup - debug_timer));
        return map;
    }

    /// <summary>
    /// 指定したインデックスのマップインスタンスに指定したマップ環境のマップを生成し、現在のマップに設定する
    /// </summary>
    /// <param name="env"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static MapData CreateAndSetMap(MapEnvironment env, int index)
    {
        var debug_timer = Time.realtimeSinceStartup;
        Tilemap O_map;
        Tilemap G_map;

        Maps[index] = new MapData(
                G_map = env._objectAlgo.CreateMap(env, index),
                O_map = env._mapAlgo.CreateMap(env),
                env._entityAlgo.SpawnEntity(env, G_map, O_map),
                env,
                Maps[index]._index,
                Maps[index]._layer
                );

        SetCurrentMap(index);
        PlaceStair(Maps[index]);
        Debug.Log("生成時間:" + (Time.realtimeSinceStartup - debug_timer));
        return Maps[index];
    }

    /// <summary>
    /// Mapsに登録されているMapDataの_indexの中にmapIndexと一致するものがある場合trueを返します
    /// </summary>
    /// <param name="mapIndex"></param>
    /// <returns></returns>
    public static bool IsCreated(int mapIndex)
    {
        if (mapIndex < 0)
        {
            Debug.LogError($"IsCreated({mapIndex})は生成されない範囲を指定しています。0以上の値を渡すようにしてください");
        }

        foreach (MapData map in Maps)
        {
            if (map._index == mapIndex)
            {
                return true;
            }
        }

        return false;
    }

    private static void PlaceStair(MapData mapData)
    {
        SelectStair((pos, tile) =>
        {
            mapData._objectmap.SetTile(pos, tile);
            Debug.Log("set : " + pos + " " + tile);
            //_currentStairsPos.Add((Vector2Int)pos);
        });
    }

    public static void PlaceStair(int index)
    {
        var mapData = Maps[index];
        PlaceStair(mapData);
    }

    private static void SelectStair(Action<Vector3Int, TileBase> process)
    {
        foreach (var stairSet in StairsCreator.CreatedMapSets)
        {
            foreach (var stair in stairSet.stairs)
            {
                for (int i = 0; i < stair.points.Length; i++)
                {
                    if (GetMap(stair.points[i].mapIndex) == _currentMapData)
                    {
                        process(stair.points[i].pos, stairSet.env.stairTile);
                    }
                }
            }
        }
    }

    public static List<Vector2Int> GetCurStairsPos(int mapIndex)
    {
        var _currentStairsPos = new List<Vector2Int>();
        foreach (var stairSet in StairsCreator.CreatedMapSets)
        {
            foreach (var stair in stairSet.stairs)
            {
                for (int i = 0; i < stair.points.Length; i++)
                {
                    if (stair.points[i].mapIndex == mapIndex)
                    {
                        _currentStairsPos.Add((Vector2Int)stair.points[i].pos);
                    }
                }
            }
        }
        return _currentStairsPos;
    }

    public static void GetCurrentMapStairs(MapData mapData)
    {
        _currentStairsPos = new List<Vector2Int>();
        foreach (var stairSet in StairsCreator.CreatedMapSets)
        {
            foreach (var stair in stairSet.stairs)
            {
                for (int i = 0; i < stair.points.Length; i++)
                {
                    if (GetMap(stair.points[i].mapIndex) == _currentMapData)
                    {
                        _currentStairsPos.Add((Vector2Int)stair.points[i].pos);
                    }
                }
            }
        }
    }

    public static bool IsMapEmpty(int mapIndex)
    {
        return GetMap(mapIndex)._groundmap == null;
    }

    public static bool IsCurEnv(MapEnvironment env)
    {
        if (_currentMapData._env == env)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsCurEnv(Env env)
    {
        try
        {
            if (_currentMapData._env.name == env.ToString())
            {
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
}

[Serializable]
public class MapData
{
    public Tilemap _objectmap;
    public Tilemap _groundmap;
    public GameObject _objectsParent;
    public MapEnvironment _env;
    public int _index, _layer;

    public MapData(Tilemap objectmap, Tilemap groundmap, GameObject objectsParent, MapEnvironment env, int index, int layer)
    {
        _objectmap = objectmap;
        _groundmap = groundmap;
        _objectsParent = objectsParent;
        _env = env;
        _index = index;
        _layer = layer;
    }
}

public enum Env
{
    SnowyPlains,
    Cave
}
