using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Object/TileOnClickEvent")]
public abstract class TileOnClickEvent : ScriptableObject
{
    protected GameObject uiInstance;

    public abstract void OpenEx();
    public void Open(TileClickController obj, Vector3Int cellPos, GameObject UIPref)
    {
        SetupUI(obj, cellPos, UIPref);
        OpenEx();
    }

    public void UpdateUI(Vector3Int cellPos)
    {
        uiInstance.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(cellPos);
    }

    public void Close(TileClickController obj)
    {
        obj.DestroyUI(uiInstance);
    }

    public void SetupUI(TileClickController obj, Vector3Int cellPos, GameObject UIPref)
    {
        uiInstance = obj.CreateUI(UIPref);
        uiInstance.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(cellPos);
    }
}
