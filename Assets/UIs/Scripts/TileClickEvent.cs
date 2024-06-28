using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TileClickEvent : ScriptableObject
{
    protected GameObject uiInstance;

    public abstract void Enter(TileClickController obj, Vector3Int cellPos, TileObject tileObj);

    public abstract void UpdateEvent(TileClickController obj, Vector3Int cellPos);

    public abstract void Exit(TileClickController obj);
}
