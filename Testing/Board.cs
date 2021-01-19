using UnityEngine;
using UnityEngine.UI;

public enum CellState
{
    None = 0,
    Friendly = 1,
    Enemy = 2,
    Free = 3,
    OutOfBounds = 4 
}

public class Board : MonoBehaviour
{
    [SerializeField]
    private Color32 color;
    [SerializeField]
    private GameObject cellPrefab;

    private Cell[,] allCells = new Cell[8, 8];

    public Cell[,] AllCells
    {
        get => allCells;
    }

    public void Create()
    {
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                GameObject newCell = Instantiate(cellPrefab, transform);
                RectTransform rectTransform = newCell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2((x * 100) + 50, (y * 100) + 50);

                allCells[x, y] = newCell.GetComponent<Cell>();
                allCells[x, y].Setup(new Vector2Int(x, y), this);
            }
        }

        for (int x = 0; x < 8; x += 2)
        {
            for (int y = 0; y < 8; y++)
            {
                int offset = (y % 2 != 0) ? 0 : 1;
                int finalX = x + offset;

                allCells[finalX, y].GetComponent<Image>().color = color;
            }
        }
    }

    public CellState ValidateCell(int targetX, int targetY, Pawn checkingPiece)
    {
        if (targetX < 0 || targetX > 7)
            return CellState.OutOfBounds;

        if (targetY < 0 || targetY > 7)
            return CellState.OutOfBounds;
        Cell targetCell = allCells[targetX, targetY];

        if (targetCell.CurrentPiece != null)
        {
            if (checkingPiece.Color == targetCell.CurrentPiece.Color)
                return CellState.Friendly;

            if (checkingPiece.Color != targetCell.CurrentPiece.Color)
                return CellState.Enemy;
        }

        return CellState.Free;
    }
}
