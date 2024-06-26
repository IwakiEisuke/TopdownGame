using UnityEngine.Tilemaps;

public abstract class ObjectGenerationAlgorithm : TileGenerationBase
{
    public virtual Tilemap CreateMap(MapEnvironment env, int mapIndex)
    {
        Tilemap map = InitMap();
        return GenerateWithAlgorithm(env, mapIndex, ref map);
    }

    /// <summary>
    /// refmap‚Éƒ}ƒbƒv‚ğ¶¬‚·‚é
    /// </summary>
    /// <param name="env"></param>
    /// <param name="mapIndex"></param>
    /// <param name="refmap"></param>
    /// <returns></returns>
    public virtual Tilemap GenerateWithAlgorithm(MapEnvironment env, int mapIndex, ref Tilemap refmap)
    {
        return refmap;
    }
    public Tilemap InitMap()
    {
        return base.InitMap("Object", "ObjectMap");
    }
}
