using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "TileSettings")]
public class TileSettings : ScriptableObject
{
    public TileBase[] grounds;
    public TileBase[] equipments;
}
