using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    public static Tilemap _currentGroundMap;
    public static Tilemap _currentObjectMap;
    public static GameObject _currentObjectsParent;

    [SerializeField] List<MapData> maps;
    public static List<MapData> Maps { get => Instance.maps; private set => Instance.maps = value; }

    public static MapData _currentMapData;

    public static int _currentLayer;
    public static int _currentLocalLayer;

    public static List<Vector2Int> _currentStairsPos;

    public MapEnvironment _startEnv;

    [SerializeField] GlobalLightController lightController;
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
        CreateMap(_startEnv, setMapCurrent: true);
    }

    public static void AddMap(MapData map)
    {
        Maps.Add(map);
    }

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
        Debug.Log(_currentLayer);
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


    public static MapData CreateMap(int layer)
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

    public static MapData CreateMap(MapEnvironment env, int layer = 0, bool setMapCurrent = false)
    {
        //var newObjectTile = new GameObject("ObjectTile").AddComponent<Tilemap>();
        //newObjectTile.AddComponent<TilemapRenderer>();
        //newObjectTile.transform.parent = FindObjectOfType<Grid>().transform;

        var map = new MapData(
                env._objectAlgo.CreateMap(env, 0),
                env._mapAlgo.CreateMap(env),
                env._entityAlgo.SpawnEntity(env),
                env,
                Maps.Count,
                layer
                );

        AddMap(map);
        if (setMapCurrent) SetCurrentMap(map);

        StairsCreator.StartProcess();

        PlaceStair(map);

        return map;
    }


    public static MapData CreateAndSetMap(MapEnvironment env, int index)
    {
        //var newObjectTile = new GameObject("ObjectTile").AddComponent<Tilemap>();
        //newObjectTile.AddComponent<TilemapRenderer>();
        //newObjectTile.transform.parent = FindObjectOfType<Grid>().transform;

        Maps[index] = new MapData(
                env._objectAlgo.CreateMap(env, index),
                env._mapAlgo.CreateMap(env),
                env._entityAlgo.SpawnEntity(env),
                env,
                Maps[index]._index,
                Maps[index]._layer
                );

        SetCurrentMap(index);
        PlaceStair(Maps[index]);

        return Maps[index];
    }

    /// <summary>
    /// 実際には階段の表示処理といった所
    /// </summary>
    /// <param name="mapData"></param>
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

    public static List<Vector2Int> GetStairs(int mapIndex)
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



    public static bool IsCreated(int mapIndex)
    {
        if (mapIndex < 0)
        {
            throw new Exception($"IsCreated({mapIndex})は生成されない範囲を指定しています");
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

    public static bool IsMapEmpty(int mapIndex)
    {
        return GetMap(mapIndex)._groundmap == null;
    }



    public static bool IsMap(MapEnvironment env)
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

    public static bool IsMap(Env env)
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
