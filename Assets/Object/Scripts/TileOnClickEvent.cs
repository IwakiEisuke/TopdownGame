using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Object/TileOnClickEvent")]
public class TileOnClickEvent : ScriptableObject
{
    GameObject uiInstance;

    public void Open(TileClickController obj, Vector3Int cellPos, GameObject UIPref)
    {
        Debug.Log(cellPos);
        uiInstance = obj.CreateUI(UIPref);
        uiInstance.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(cellPos);
        var creator = uiInstance.GetComponentInChildren<RecipeUICreator>();

            creator.items = Inventory.Items;
        
        
    }

    public void UpdateUI(Vector3Int cellPos)
    {
        uiInstance.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(cellPos);
    }

    public void Close(TileClickController obj)
    {
        obj.DestroyUI(uiInstance);
    }
}
