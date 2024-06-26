using UnityEngine.Tilemaps;

public abstract class ObjectGenerationAlgorithm : TileGenerationBase
{
    /// <summary>
    /// InitMapを開始時のみ行うためのメソッド。通常いじる必要はない
    /// </summary>
    /// <param name="env"></param>
    /// <param name="mapIndex"></param>
    /// <returns></returns>
    public virtual Tilemap CreateMap(MapEnvironment env, int mapIndex)
    {
        Tilemap map = InitMap();
        return GenerateWithAlgorithm(env, mapIndex, ref map);
    }

    /// <summary>
    /// refmapにマップを生成し返す
    /// </summary>
    /// <param name="env"></param>
    /// <param name="mapIndex"></param>
    /// <param name="refmap"></param>
    /// <returns></returns>
    public abstract Tilemap GenerateWithAlgorithm(MapEnvironment env, int mapIndex, ref Tilemap refmap);

    public Tilemap InitMap()
    {
        return base.InitMap("Object", "ObjectMap");
    }
}
