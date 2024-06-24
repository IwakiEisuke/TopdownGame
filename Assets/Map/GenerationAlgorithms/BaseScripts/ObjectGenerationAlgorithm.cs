using UnityEngine.Tilemaps;

public abstract class ObjectGenerationAlgorithm : TileGenerationBase
{
    public virtual Tilemap CreateMap(MapEnvironment env, int mapIndex)
    {
        Tilemap map = InitMap();
        return Algorithm(env, mapIndex, ref map);
    }
    public virtual Tilemap Algorithm(MapEnvironment env, int mapIndex, ref Tilemap refmap)
    {
        return refmap;
    }
    public Tilemap InitMap()
    {
        return base.InitMap("Object", "ObjectMap");
    }
}
