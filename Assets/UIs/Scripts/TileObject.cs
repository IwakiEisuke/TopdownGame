using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Object/New Object")]
public class TileObject : ScriptableObject
{
    [SerializeField] TileBase tile;
    [SerializeField] Sprite tileUISprite;
    [SerializeField] Recipe recipe;
    [SerializeField] TileClickEvent clickEvent;
    public TileBase Tile { get { return tile; } }
    public Sprite TileUISprite {  get { return tileUISprite; } }
    public Recipe Recipe { get { return recipe; } }
    public TileClickEvent ClickEvent { get { return clickEvent; } }
}
