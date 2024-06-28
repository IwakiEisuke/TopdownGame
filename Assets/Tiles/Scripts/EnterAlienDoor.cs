using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Object/TileInteractEvents/EnterAlienDoor")]
public class EnterAlienDoor : TileInteractEvent
{
    [SerializeField] Recipe consumeForInteract;
    public override void Enter(InteractUnderfootTile obj, Vector3Int cellPos, TileObject tile)
    {
        Recipe.RecipeProcess(consumeForInteract, () =>
        {
            Debug.Log("GameClear");
            obj.GetComponent<GameClearManager>().Enter();
        });
    }

    public override void Exit(InteractUnderfootTile obj)
    {
        
    }

    public override void UpdateEvent(InteractUnderfootTile obj, Vector3Int cellPos)
    {

    }
}
