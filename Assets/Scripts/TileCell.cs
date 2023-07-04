using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCell : MonoBehaviour
{
    public Vector2Int coordinates;

    public Tile tile;
    public bool emtpty => tile == null;
    public bool occ => tile != null;

}
