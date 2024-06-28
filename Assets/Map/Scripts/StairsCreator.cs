using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class StairsCreator : MonoBehaviour
{
    public static StairsCreator Instance { get; private set; }

    [SerializeField] int createMapSetsCount;
    [SerializeField] MapEnvironment[] createEnvs;

    [SerializeField] List<MapSet> createdMapSets;
    //[SerializeField] List<StairData> createdStairsData;

    public static int CreateStairsSetCount => Instance.createMapSetsCount;
    public static MapEnvironment[] CreateEnvs { get => Instance.createEnvs; set => Instance.createEnvs = value; }
    public static List<MapSet> CreatedMapSets { get => Instance.createdMapSets; set => Instance.createdMapSets = value; }
    //public static List<StairData> CreatedStairsData { get => Instance.createdStairsData; set => Instance.createdStairsData = value; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(Instance);
    }

    public static void StartProcess()
    {
        CreateMapSetOnChunk();
        CreateStairs();

    }

    private void Start()
    {
        Debug.Log("createEnvs.Length : " + createEnvs.Length);
        Debug.Log("CreateEnvs.Length : " + CreateEnvs.Length);
    }

    private static void CreateMapSetOnChunk()
    {
        /*
        //生成されるチャンク毎の処理
        //現在プレイヤーのいるマップの環境と階層の両方において生成可能なエレベーターを検索

        
        List<CanCreate> canCreate = new();
        for (int i = 0; i < CreateEnvs.Length; i++)
        {
            foreach (var sett in CreateEnvs[i]._appearSettings)
            {
                if (sett._appearEnv == MapManager._currentMapData._env)
                {
                    if (sett._onlyFloor == 0 || MapManager._currentLayer == sett._onlyFloor && sett._onlyLocalFloor == 0 || MapManager._currentLocalLayer == sett._onlyLocalFloor)
                    {
                        canCreate.Add(new CanCreate(CreateEnvs[i], sett._moveUp, sett._moveHorizon));
                    }
                }
            }
        }

        if (canCreate.Count > 0)
        {
            for (int i = 0; i < CreateCavesCount; i++)
            {
                var wow = canCreate[Random.Range(0, canCreate.Count)];

                var mapBounds = MapManager._currentMapData._env._mapAlgo._mapBounds;
                var x = Random.Range(-mapBounds.x, mapBounds.x);
                var y = Random.Range(-mapBounds.y, mapBounds.y);

                var currentLayer = MapManager._currentLayer;





                var offset = 0;
                if (!wow.moveHorizon)
                {
                    offset = 1;
                }

                var upLayer = 0;
                var lowLayer = 0;
                if (wow.moveUp)
                {
                    upLayer = Random.Range(currentLayer + offset, wow.env._maxFloor + offset);
                    lowLayer = currentLayer + offset;
                }
                else
                {
                    upLayer = currentLayer - offset;
                    lowLayer = Random.Range(currentLayer - offset, -wow.env._maxFloor - offset);
                }


                CreatedSurfaceStairsData.Add(new StairSetData(
                    index: CreatedSurfaceStairsData.Count,
                    upLayer: upLayer,
                    lowLayer: lowLayer,
                    env: wow.env));
            }
        }
        */

        //マップ生成時に生成範囲からランダムに階段を設置。設置先と同位置のグラウンドタイルがどの環境かに応じて階段の移動先環境を設定する。
        //複数環境で同じタイルを共有する場合単純に同一タイルかどうかを測定してもうまくいかないので、その場合
        //タイル位置ごとにタイル種類とタイル環境を定義する情報クラスを作成し、それを参照するように設計する。
        for (int i = 0; i < CreateStairsSetCount; i++)
        {
            //マップ生成時ランダムなタイルから生成可能環境を取得する用のポジション生成
            var mapBounds = MapManager._currentMapData._groundmap.localBounds;
            var x = (int)Random.Range(mapBounds.min.x, mapBounds.max.x);
            var y = (int)Random.Range(mapBounds.min.y, mapBounds.max.y);

            List<CanCreate> toCreateEnv = new List<CanCreate>();

            //アサインされた全環境の＜出現可能環境が生成するグラウンド＞が生成先にある場合生成可能環境に追加する。
            foreach (var env in CreateEnvs)
            {
                foreach (var setting in env._appearSettings)
                {
                    foreach (var ground in setting._appearEnv._mapAlgo._tileSettings)
                    {
                        if (MapManager._currentGroundMap.GetTile(new Vector3Int(x, y)) == ground._tile)
                        {
                            toCreateEnv.Add(new CanCreate(env, setting._moveUp, setting._moveHorizon));
                        }
                    }

                }
            }


            var s = toCreateEnv[Random.Range(0, toCreateEnv.Count)];

            var upLayer = 0;
            var lowLayer = 0;

            //var offset = 1;
            //var diffLayer = Random.Range(1, s.env._maxFloor);


            //if (s.moveUp)
            //{
            //    upLayer = offset + diffLayer;
            //    lowLayer = offset;
            //}
            //else
            //{
            //    upLayer = -offset;
            //    lowLayer = -offset - diffLayer;
            //}

            var offset = 1;
            var diffLayer = Random.Range(s.env._minFloor, s.env._maxFloor + 1);

            if (s.moveUp)
            {
                upLayer = offset;
                lowLayer = s.env._minFloor;
            }
            else
            {
                upLayer = -offset;
                lowLayer =-diffLayer;
            }

            if (s.moveHorizon)
            {

            }

            var currentLayer = MapManager._currentLayer;
            var moveToLayer = MapManager._currentLayer;

            CreatedMapSets.Add(new MapSet(
                index: CreatedMapSets.Count,
                topLayer: upLayer,
                lowLayer: lowLayer,
                env: s.env,
                initPos: new Vector3Int(x, y)));
        }
    }

    private static void CreateStairs()
    {
        foreach (var mapSet in CreatedMapSets)
        {
            var counter = 0;
            var mapMaxIndex = MapManager.Maps.Count - 1;
            for (int i = mapSet.topLayer; i >= mapSet.lowLayer; i--)
            {
                CreateStair(counter == 0 ? 0 : mapMaxIndex - 1 - i, mapMaxIndex - i, i + 1, i, mapSet);
                if (counter == 0)
                {
                    mapSet.stairs.Last().points[0].pos = mapSet.initPos;
                }

                counter++;
                if (counter > 10)
                {
                    throw new Exception("無限ループ");
                }
            }
        }
    }

    private static void CreateStair(int mapIndex1, int mapIndex2, int topLayer, int lowLayer, MapSet mapSet)
    {
        var bounds = mapSet.env._mapAlgo._mapBounds;

        var topPos = new Vector3Int(Random.Range(-bounds.x, bounds.x), Random.Range(-bounds.y, bounds.y));
        var lowPos = new Vector3Int(Random.Range(-bounds.x, bounds.x), Random.Range(-bounds.y, bounds.y));

        var topIndex = MapManager.GetIndex(MapManager.IsCreated(mapIndex1) ? MapManager.GetMap(mapIndex1) : MapManager.CreateNullMap(topLayer));
        var lowIndex = MapManager.GetIndex(MapManager.IsCreated(mapIndex2) ? MapManager.GetMap(mapIndex2) : MapManager.CreateNullMap(lowLayer));

        var top = new WarpPoint(topPos, topIndex);
        var low = new WarpPoint(lowPos, lowIndex);

        mapSet.stairs.Add(new StairData(top, low));
    }
}

[Serializable]
public class StairData
{
    public WarpPoint[] points = new WarpPoint[2];

    public StairData(WarpPoint top, WarpPoint low)
    {
        points[0] = top;
        points[1] = low;
    }
}

[Serializable]
public class WarpPoint
{
    public Vector3Int pos;
    public int mapIndex;

    public WarpPoint(Vector3Int pos, int index)
    {
        this.pos = pos;
        this.mapIndex = index;
    }
}

[Serializable]
public class CanCreate
{
    public MapEnvironment env;
    public bool moveUp;
    public bool moveHorizon;

    public CanCreate(MapEnvironment env, bool moveUp, bool moveHorizon)
    {
        this.env = env;
        this.moveUp = moveUp;
        this.moveHorizon = moveHorizon;
    }
}


[Serializable]
public class MapSet
{
    public int index, topLayer, lowLayer;
    public MapEnvironment env;
    public readonly Vector3Int initPos;
    public List<StairData> stairs = new List<StairData>();

    public MapSet(int index, int topLayer, int lowLayer, MapEnvironment env, Vector3Int initPos)
    {
        this.index = index;
        this.topLayer = topLayer;
        this.lowLayer = lowLayer;
        this.env = env;
        this.initPos = initPos;
    }
}
