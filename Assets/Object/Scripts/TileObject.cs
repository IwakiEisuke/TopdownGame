using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Object/New Object")]
public class TileObject : ScriptableObject
{
    [SerializeField] TileBase tile;
    [SerializeField] Recipe recipe;
    public TileBase Tile { get { return tile; } }
    public Recipe Recipe { get { return recipe; } }
}
