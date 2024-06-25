using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Object/TileOnClickEvent")]
public class TileOnClickEvent : ScriptableObject
{
    GameObject uiInstance;

    public void Open(TileClickController obj, Vector3Int pos, GameObject UIPref)
    {
        Debug.Log(pos);
        uiInstance = obj.CreateUI(UIPref);
        uiInstance.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(pos);
        UpdateUI(pos);
    }

    public void UpdateUI(Vector3Int pos)
    {
        uiInstance.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(pos);
    }

    public void Close(TileClickController obj)
    {
        obj.DestroyUI(uiInstance);
    }
}
