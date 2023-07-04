using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] rows;
    public TileCell[] cells;

    public int size => cells.Length;
    public int height => rows.Length;
    public int width => size / height;

    private void Awake()
    {
        rows = GetComponentsInChildren<TileRow>();
        cells = GetComponentsInChildren<TileCell>();
        //Debug.Log("GRIDDDDDD");
    }

    private void Start()
    {
        for(int x = 0; x < rows.Length; x++)
        {
            for(int y = 0; y < rows[x].cells.Length; y++)
            {
                rows[x].cells[y].coordinates = new Vector2Int(y, x);
                Debug.Log("y: " + y + " x: " + x);
            }
        }

        //Debug.Log("Star GRID ");
    }


    private TileCell GetCell(Vector2Int coordinates)
    {
        return GetCell(coordinates.x,coordinates.y);
    }

    public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction)
    {
        Vector2Int coordinates = cell.coordinates;
        coordinates.x += direction.x;
        coordinates.y -= direction.y;

        return GetCell(coordinates);
    }

    public TileCell GetCell(int x, int y)
    {
        if(x>=0 && x<width && y>=0 && y < height)
        {
            return rows[y].cells[x];
        }
        else
        {
            return null;
        }
    }

    public TileCell RamdomTileCell()
    {
        int index = Random.Range(0, cells.Length);
        int indexCurrent = index;

        while (!cells[index].emtpty)
        {
            index++;
            if (index > cells.Length-1)
            {
                index = 0;
            }
            if (index == indexCurrent) return null;
        }
        
        return cells[index];
    }


    public TileCell GetTileCell(int x, int y)
    {
        foreach(TileCell tileCell in cells)
        {
            if (tileCell.coordinates.x == x && tileCell.coordinates.y == y)
                return tileCell;
        }
        return null;
    }

    public TileCell Spaw(Tile tile)
    {
        
        TileCell tileCell = RamdomTileCell();
        if (tileCell == null) return null;
        //tileCell.tile.SetTileCell(tileState,number);
        tileCell.tile = tile;
        
        return tileCell;
    }

    
}
