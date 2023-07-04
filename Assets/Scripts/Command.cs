using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command 
{
    public List<BlockUnit> blockUnits = new List<BlockUnit>();
    public List<BlockUnit> undoBlockUnits = new List<BlockUnit>();
    public List<BlockUnit> redoBlockUnits = new List<BlockUnit>();

    public void AddBlockUnit(List<Tile> listTile, int score)
    {
        BlockUnit blockUnit = new BlockUnit();
        blockUnit.AddBlockUnit(listTile, score);
        blockUnits.Add(blockUnit);
    }

    public void Undo()
    {
        if (blockUnits.Count == 0) return;
        undoBlockUnits.Add(blockUnits[blockUnits.Count - 1]);
        blockUnits.RemoveAt(blockUnits.Count - 1);
    }

    public void Redo()
    {
        if (undoBlockUnits.Count == 0) return;
        blockUnits.Add(undoBlockUnits[undoBlockUnits.Count - 1]);
        undoBlockUnits.RemoveAt(undoBlockUnits.Count - 1);
    }
}

public class BlockUnit
{
    public List<Unit> blockUnit = new List<Unit>();
    public int numberScore = 0;
    public void AddBlockUnit(List<Tile> listTile, int score)
    {

        foreach (Tile tile in listTile)
        {
            Unit unit = new Unit();
            unit.CreateUnit(tile);
            blockUnit.Add(unit);

        }
        numberScore = score;
    }


}

public class Unit
{
    public int x;
    public int y;
    public int number;

    public void CreateUnit(Tile tile)
    {
        x = tile.cell.coordinates.x;
        y = tile.cell.coordinates.y;
        number = tile.number;
    }

}
