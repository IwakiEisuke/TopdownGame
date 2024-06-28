using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileSettings", menuName = "Settings/TileSettings")]
public class TileSettings : ScriptableObject
{
    [SerializeField] TileBase[] grounds;
    [SerializeField] TileObject[] facilities;
    public TileBase[] Grounds {  get { return grounds; } }
    public TileObject[] Facilities { get { return facilities; } }
}
