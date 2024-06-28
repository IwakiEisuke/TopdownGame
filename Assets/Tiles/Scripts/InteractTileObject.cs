using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Object/TileClickEvents/InteractTileObject")]
public class InteractTileObject : TileClickEvent
{
    [SerializeField] Recipe consumeForInteract;
    public override void Enter(TileClickController obj, Vector3Int cellPos, TileObject tile)
    {
        Recipe.RecipeProcess(consumeForInteract, () => Debug.Log("GameClear"));
        Exit(obj);
    }

    public override void Exit(TileClickController obj)
    {
        obj.ActiveTileObject = null;
    }
    public override void UpdateEvent(TileClickController obj, Vector3Int cellPos)
    {

    }
}
