using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pawn : EventTrigger
{
    private Color color = Color.clear;
    private bool isFirstMove = true;

    private Cell originalCell = null;
    private Cell currentCell = null;

    private RectTransform rectTransform = null;
    private PieceManager pieceManager;

    private Cell targetCell = null;

    private Vector3Int movement = Vector3Int.one;
    private List<Cell> highlightedCells = new List<Cell>();

    public Color Color
    {
        get => color;
        set=>  color = value;
    }

    public bool IsFirstMove
    {
        get => isFirstMove;
        set => isFirstMove = value;
    }

    public void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        pieceManager = newPieceManager;

        color = newTeamColor;
        GetComponent<Image>().color = newSpriteColor;
        rectTransform = GetComponent<RectTransform>();

        movement = color == Color.white ? new Vector3Int(0, 1, 1) : new Vector3Int(0, -1, -1);
        GetComponent<Image>().sprite = Resources.Load<Sprite>("T_Pawn");
    }

    public void Place(Cell newCell)
    {
        currentCell = newCell;
        originalCell = newCell;
        currentCell.CurrentPiece = this;


        transform.position = newCell.transform.position;
        gameObject.SetActive(true);
    }

    public void Kill()
    {
        currentCell.CurrentPiece = null;
        gameObject.SetActive(false);
    }

    public bool HasMove()
    {
        CheckPathing();

        if (highlightedCells.Count == 0)
            return false;

        return true;
    }

    private void ShowCells()
    {
        foreach (Cell cell in highlightedCells)
            cell.OutlineImage.enabled = true;
    }

    private void ClearCells()
    {
        foreach (Cell cell in highlightedCells)
            cell.OutlineImage.enabled = false;

        highlightedCells.Clear();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        CheckPathing();
        ShowCells();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        transform.position += (Vector3)eventData.delta;

        foreach (Cell cell in highlightedCells)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(cell.RectTransform, Input.mousePosition))
            {
                targetCell = cell;
                break;
            }

            targetCell = null;
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        ClearCells();

        if (!targetCell)
        {
            transform.position = currentCell.gameObject.transform.position;
            return;
        }
        Move();
        pieceManager.SwitchSides(color);
    }

    private void Move()
    {
        isFirstMove = false;
        targetCell.RemovePiece();

        currentCell.CurrentPiece = null;

        currentCell = targetCell;
        currentCell.CurrentPiece = this;

        transform.position = currentCell.transform.position;
        targetCell = null;

        CheckForPoint();
    }

    private bool MatchesState(int targetX, int targetY, CellState targetState)
    {
        CellState cellState = CellState.None;
        cellState = currentCell.Board.ValidateCell(targetX, targetY, this);

        if (cellState == targetState)
        {
            highlightedCells.Add(currentCell.Board.AllCells[targetX, targetY]);
            return true;
        }

        return false;
    }

    private void CheckForPoint()
    {
        int currentX = currentCell.BoardPosition.x;
        int currentY = currentCell.BoardPosition.y;
        CellState cellState = currentCell.Board.ValidateCell(currentX, currentY + movement.y, this);

        if (cellState == CellState.OutOfBounds)
        {
            pieceManager.AddPoint(this);
        }
    }

    private void CheckPathing()
    {
        int currentX = currentCell.BoardPosition.x;
        int currentY = currentCell.BoardPosition.y;

        MatchesState(currentX - movement.z, currentY + movement.z, CellState.Enemy);
        if (MatchesState(currentX, currentY + movement.y, CellState.Free))
        {
            if (isFirstMove)
            {
                MatchesState(currentX, currentY + (movement.y * 2), CellState.Free);
            }
        }
        MatchesState(currentX + movement.z, currentY + movement.z, CellState.Enemy);
    }

    public void Reset()
    {
        Kill();

        isFirstMove = true;

        Place(originalCell);
    }
}
