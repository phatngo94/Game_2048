using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    public GameManager gameManager;
    public Tile prefabTile;
    public TileGrid grid;
    public List<TileState> tileStates;
    private List<Tile> tiles;
    private bool awaiting;

    Vector2 pos1;
    Vector2 pos2;
    bool onclick = false;
    bool cul = false;
    private void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tile>(16);
       
    }

    private void Start()
    {
       
        Debug.Log("CREATEEEEEEEEEE");
       //CreateTile();
        //CreateTile();
        
    }

    public void ClearBoard()
    {
        foreach(var cell in grid.cells)
        {
            cell.tile = null;
        }

        foreach(var tile in tiles)
        {
            Destroy(tile.gameObject);
        }

        tiles.Clear(); 
    }

    public void CreateTile()
    {
       
        Tile tile = Instantiate(prefabTile, grid.transform);
        tile.SetState(tileStates[0], 2);
        tile.Spaw(grid.RamdomTileCell());       
        tiles.Add(tile);
        //tile.Spaw(tileCell);
        /*tile.SetTileCell(tileStates[0], 2);
        TileCell tileCell = grid.Spaw(tile);
        if (tileCell == null) { Destroy(tile.gameObject); return; }
        tile.transform.position = tileCell.transform.position;
        tileCell.tile = tile;*/
    }

    public void StartGame()
    {
        CreateTile();
        CreateTile();
        gameManager.command.AddBlockUnit(tiles,4);
        Debug.Log("dddd dd" + gameManager.command.blockUnits.Count);
    }

    public void CreateTileWithIndex(int number, int x,int y)
    {
        Tile tile = Instantiate(prefabTile, grid.transform);
        //.SetState(tileStates[0], 2);
        //tile.Spaw(grid.RamdomTileCell());
        tile.Spaw(grid.GetTileCell(x, y));
        TileState state = tileStates.Find(x => x.name == number.ToString());
        tile.SetState(state, number);       
        tiles.Add(tile);
    }

    public void Undo()
    {
        if(gameManager.command.blockUnits.Count>1)
            gameManager.command.Undo();      
        BlockUnit block = gameManager.command.blockUnits[gameManager.command.blockUnits.Count - 1];             
        foreach(Unit unit in block.blockUnit)
        {
            CreateTileWithIndex(unit.number, unit.x, unit.y);
        }
        gameManager.SetScore(block.numberScore);
        
    }

    public void Redo()
    {
        BlockUnit block = gameManager.command.undoBlockUnits[gameManager.command.undoBlockUnits.Count - 1];
        foreach (Unit unit in block.blockUnit)
        {
            CreateTileWithIndex(unit.number, unit.x, unit.y);
        }
        gameManager.SetScore(block.numberScore);
        gameManager.command.Redo();
        
    }

    private void Update()
    {
        CheckWin();
        if (Input.GetKeyDown(KeyCode.Q)) { Win(); }
        if (!awaiting)
        {
            HotKeyHandleInput();
            MuoseHandleInput();
        }
        
    }
    private void HotKeyHandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveTiles(Vector2Int.up, 0, 1, 1, 1);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveTiles(Vector2Int.down, 0, 1, grid.height - 2, -1);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveTiles(Vector2Int.left, 1, 1, 0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveTiles(Vector2Int.right, grid.width - 2, -1, 0, 1);
        }
    }
    private void MuoseHandleInput()
    {
        if (Input.GetMouseButtonDown(0) && !onclick)
        {
            pos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("Click Pos1 : " + pos1);
            onclick = true;
        }

        if (Input.GetMouseButtonUp(0) && onclick)
        {
            pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (pos1 == pos2)
            {
                onclick = false;
                return;
            }
           
            onclick = false;
            cul = true;
        }

        if (cul)
        {
            float x = pos2.x - pos1.x;
            float y = pos2.y - pos1.y;
            /*Debug.Log(Mathf.Abs(pos2.x) > Mathf.Abs(pos1.x) ? "tren" : "duoi");
            Debug.Log(Mathf.Abs(pos1.y) > Mathf.Abs(pos2.y) ? "phai" : "trai");*/
            //Debug.Log(pos2.y>pos1.y ? "tren" : "duoi");
            //Debug.Log(pos2.x>pos1.x ? "phai" : "trai");

            // UP-Right
            if (pos2.y >= pos1.y && pos2.x >= pos1.x)
            {
                if ((pos2.x - pos1.x) > (pos2.y - pos1.y))
                {
                    MoveTiles(Vector2Int.right, grid.width - 2, -1, 0, 1);
                    Debug.Log("SANG PHAI");
                }

                else {
                    MoveTiles(Vector2Int.up, 0, 1, 1, 1);
                    Debug.Log("LEN TREN");
                } 
            }

            //DOWN-Left
            if (pos2.y <= pos1.y && pos2.x <= pos1.x)
            {

                if ((pos2.x - pos1.x) < (pos2.y - pos1.y))
                {
                    MoveTiles(Vector2Int.left, 1, 1, 0, 1);
                    Debug.Log("SANG TRAI");
                }
                else {
                    MoveTiles(Vector2Int.down, 0, 1, grid.height - 2, -1);
                    Debug.Log("XUONG DUOI");
                }
                
            }

            //UP-Left
            if (pos2.y >= pos1.y && pos2.x <= pos1.x)
            {

                if (Mathf.Abs((pos2.x - pos1.x)) > Mathf.Abs((pos2.y - pos1.y)))
                {
                    MoveTiles(Vector2Int.left, 1, 1, 0, 1);
                    Debug.Log("SANG TRAI");
                }
                else
                {
                    MoveTiles(Vector2Int.up, 0, 1, 1, 1);
                    Debug.Log("LEN TREN");
                }

            }

            //DOWN-Right
            if (pos2.y <= pos1.y && pos2.x >= pos1.x)
            {

                if (Mathf.Abs((pos2.x - pos1.x)) > Mathf.Abs((pos2.y - pos1.y)))
                {
                    MoveTiles(Vector2Int.right, grid.width - 2, -1, 0, 1);
                    Debug.Log("SANG PHAI");
                }
                else {
                    MoveTiles(Vector2Int.down, 0, 1, grid.height - 2, -1);
                    Debug.Log("XUONG DUOI");
                } 
            }

            cul = false;
        }
    }


    private void MoveTiles(Vector2Int direction, int startX,int incrementX, int startY, int incrementY)
    {
        gameManager.SetPrevScore();
        bool changed = false;
        for (int x = startX; x >= 0 && x < grid.width; x += incrementX)
        {
            for(int y = startY; y >= 0 && y < grid.height; y += incrementY)
            {
                TileCell cell = grid.GetCell(x, y);
                if (cell.occ)
                {
                    changed = MoveTile(cell.tile,direction);
                }
            }
        }
        
        gameManager.FadeScore();
        StartCoroutine(WaitForChanges());
       /* if (changed)
        {
            StartCoroutine(WaitForChanges());
            //StartCoroutine(SaveUndo());
        }*/


    }

    private IEnumerator SaveUndo()
    {
        yield return new WaitForSeconds(0.25f);
        gameManager.command.AddBlockUnit(tiles,gameManager.score);
        gameManager.command.undoBlockUnits.Clear();
    }
    private bool MoveTile(Tile tile , Vector2Int direction)
    {
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(tile.cell,direction);

        while (adjacent != null)
         {
             if (adjacent.occ)
             {
                if (CanMerge(tile, adjacent.tile))
                {
                    Merge(tile,adjacent.tile);
                    return true;
                }
                 break;
             }
             newCell = adjacent;
             adjacent = grid.GetAdjacentCell(adjacent,direction);
             
         }


        if (newCell != null)
        {
            tile.MoveTo(newCell);
            return true;
        }

        return false;
    }

    private bool CanMerge(Tile a, Tile b) => a.number == b.number && !b.locked;
   
    private void Merge(Tile a, Tile b)
    {
        tiles.Remove(a);
        a.Merge(b.cell);

        int index = Mathf.Clamp(IndexOf(b.state)+1,0,tileStates.Count-1);
        int number = b.number * 2;
        b.SetState(tileStates[index],number);

        gameManager.IncreaseScore(number);
        gameManager.SaveHiscore();
    }

    private int IndexOf(TileState state)
    {
        for(int i = 0; i < tileStates.Count; i++)
        {
            if(state == tileStates[i])
            {
                return i;
            }
        }
        return -1;
    }

    private IEnumerator WaitForChanges()
    {
        awaiting = true;
        yield return new WaitForSeconds(0.11f);
        awaiting = false;

        foreach(var tile in tiles)
        {
            tile.locked = false;
        }

        if (tiles.Count != grid.size)
        {
            CreateTile();
        }
        gameManager.command.AddBlockUnit(tiles, gameManager.score);
        gameManager.command.undoBlockUnits.Clear();       
        if (CheckForGameOver())
        {
            gameManager.GameOver();
        }
        
    }

    public void CheckWin()
    {
        foreach(var tile in tiles)
        {
            if (tile.number == 2048)
            {
                Debug.Log("Win Game");
            }
        }
    }

    public void Win()
    {
        Tile tile = Instantiate(prefabTile, grid.transform);
        tile.SetState(tileStates[10], 2048);
        tile.Spaw(grid.RamdomTileCell());
        tiles.Add(tile);
    }

    public bool CheckForGameOver()
    {
        if(tiles.Count != grid.size)
        {
            return false;
        }

        foreach(var tile in tiles)
        {
            TileCell up = grid.GetAdjacentCell(tile.cell,Vector2Int.up);
            TileCell down = grid.GetAdjacentCell(tile.cell,Vector2Int.down);
            TileCell left = grid.GetAdjacentCell(tile.cell,Vector2Int.left);
            TileCell right = grid.GetAdjacentCell(tile.cell,Vector2Int.right);

            if(up !=null && CanMerge(tile, up.tile))
            {
                return false;
            }

            if (down != null && CanMerge(tile, down.tile))
            {
                return false;
            }

            if (left != null && CanMerge(tile, left.tile))
            {
                return false;
            }

            if (right != null && CanMerge(tile, right.tile))
            {
                return false;
            }
        }

        return true;
    }
}


