using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Object/New Object")]
public class TileObject : ScriptableObject
{
    [SerializeField] TileBase tile;
    [SerializeField] Recipe recipe;
    [SerializeField] TileOnClickEvent clickEvent;
    [SerializeField] GameObject ui;
    public TileBase Tile { get { return tile; } }
    public Recipe Recipe { get { return recipe; } }
    public TileOnClickEvent ClickEvent { get { return clickEvent; } }
    public GameObject UI { get { return ui; } }
}
