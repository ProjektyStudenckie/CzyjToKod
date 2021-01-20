using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField]
    public Image outlineImage;

    private Vector2Int boardPosition = Vector2Int.zero;
    private Board board;
    private RectTransform rectTransform;
    private Pawn currentPiece;

    public Image OutlineImage
    {
        get => outlineImage;
    }

    public Vector2Int BoardPosition
    {
        get => boardPosition;
    }

    public Board Board
    {
        get => board;
        set => board = value;
    }

    public RectTransform RectTransform
    {
        get => rectTransform;
        set => rectTransform = value;
    }

    public Pawn CurrentPiece
    {
        get => currentPiece;
        set => currentPiece = value;
    }

    public void Setup(Vector2Int newBoardPosition, Board newBoard)
    {
        boardPosition = newBoardPosition;
        board = newBoard;

        rectTransform = GetComponent<RectTransform>();
    }

    public void RemovePiece()
    {
        if (currentPiece != null)
        {
            currentPiece.Kill();
        }
    }
}
