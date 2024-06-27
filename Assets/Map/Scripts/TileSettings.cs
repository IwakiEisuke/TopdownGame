using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileSettings", menuName = "Settings/TileSettings")]
public class TileSettings : ScriptableObject
{
    [SerializeField] TileBase[] grounds;
    [SerializeField] TileObject[] equipments;
    public TileBase[] Grounds {  get { return grounds; } }
    public TileObject[] Equipments { get { return equipments; } }
}
