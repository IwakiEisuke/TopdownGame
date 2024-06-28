using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileInteractEvent : ScriptableObject
{
    public abstract void Enter(InteractUnderfootTile obj, Vector3Int cellPos, TileObject tileObj);
    public abstract void UpdateEvent(InteractUnderfootTile obj, Vector3Int cellPos);
    public abstract void Exit(InteractUnderfootTile obj);
}
