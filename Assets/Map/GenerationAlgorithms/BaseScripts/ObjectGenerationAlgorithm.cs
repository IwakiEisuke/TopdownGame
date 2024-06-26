using UnityEngine.Tilemaps;

public abstract class ObjectGenerationAlgorithm : TileGenerationBase
{
    /// <summary>
    /// InitMap���J�n���̂ݍs�����߂̃��\�b�h�B�ʏ킢����K�v�͂Ȃ�
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
    /// refmap�Ƀ}�b�v�𐶐����Ԃ�
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
