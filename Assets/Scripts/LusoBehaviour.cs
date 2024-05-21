using System;
using System.Collections.Generic;
using UnityEngine;

public class LusoBehaviour : MonoBehaviour
{
    private List<Cell> cellList;
    private int currentCellIndex;
    private Vector2 gridDimensions;
    
    public int CurrentCell
    {
        get => currentCellIndex;
        private set
        {
            currentCellIndex = value;
            OnCurrentCellChange();
        }
    }

    public Vector2 GridDimensions
    {
        get => gridDimensions;
        private set
        {
            gridDimensions = value;
            CalculateMoveIndexDeltas((int)value.y);
        }
    }

    private int upMoveCellIndexDelta;
    private int downMoveCellIndexDelta;
    private int leftMoveCellIndexDelta;
    private int rightMoveCellIndexDelta;

    private void Awake()
    {
        cellList = FindObjectOfType<GameManager>().CellList;
        if (cellList is null)
            Debug.Log(
                "Couldn't find cellArray, either it wasn't created " +
                "or there is no object with a GameManager in the scene");
    }

    public void UpdateCurrentCell(int newCellIndex)
    {
        CurrentCell = newCellIndex;
    }

    public void UpdateGridDimensions(Vector2 newGridDimensions)
    {
        GridDimensions = newGridDimensions;
    }
    
    private void UpdatePositionInGrid(int newPositionCellIndex)
    {
        CurrentCell = newPositionCellIndex;
    }

    private void OnCurrentCellChange()
    {
        transform.position = cellList[CurrentCell].transform.position;
    }
    
    private void CalculateMoveIndexDeltas(int numOfCollumns)
    {
        upMoveCellIndexDelta = -numOfCollumns;
        downMoveCellIndexDelta = numOfCollumns;
        rightMoveCellIndexDelta = 1;
        leftMoveCellIndexDelta = -1;
    }

    private void TryMove(int moveCellIndexDelta)
    {
        if(!CheckValidMove(moveCellIndexDelta)) return;

        UpdatePositionInGrid(CurrentCell + moveCellIndexDelta);
    }
    
    private bool CheckValidMove(int moveCellIndexDelta)
    {
        if (moveCellIndexDelta == 0) return false;
            
        Cell targetCell = cellList[CurrentCell + moveCellIndexDelta];
        
        return targetCell is not null && targetCell.CellState != Cell.State.Wall;
    }

    public void MoveUp()
    {
        TryMove(upMoveCellIndexDelta);
    }

    public void MoveDown()
    {
        TryMove(downMoveCellIndexDelta);
    }

    public void MoveLeft()
    {
        TryMove(leftMoveCellIndexDelta);
    }

    public void MoveRight()
    {
        TryMove(rightMoveCellIndexDelta);
    }
}