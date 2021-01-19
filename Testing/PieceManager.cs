using System;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mPiecePrefab;
    [SerializeField]
    private Color32 colorWhite;
    [SerializeField]
    private Color32 colorBlack;

    private int blackPoints;
    private int whitePoints;

    private List<Pawn> whitePieces = null;
    private List<Pawn> blackPieces = null;

    private string[] pieceOrder = new string[16]
    {
        "P", "P", "P", "P", "P", "P", "P", "P",
        "P", "P", "P", "P", "P", "P", "P", "P"
    };

    public static event Action<int,int> addPoint;

    public void Setup(Board board)
    {
        whitePieces = CreatePieces(Color.white, colorWhite);
        blackPieces = CreatePieces(Color.black, colorBlack);
        PlacePieces(1, 0, whitePieces, board);
        PlacePieces(6, 7, blackPieces, board);
        SwitchSides(Color.black);
    }

    private List<Pawn> CreatePieces(Color teamColor, Color32 spriteColor)
    {
        List<Pawn> newPieces = new List<Pawn>();

        for (int i = 0; i < pieceOrder.Length; i++)
        {
            string key = pieceOrder[i];

            Pawn newPiece = Create();
            newPieces.Add(newPiece);

            newPiece.Setup(teamColor, spriteColor, this);
        }

        return newPieces;
    }

    private Pawn Create()
    {
        GameObject newPieceObject = Instantiate(mPiecePrefab);
        newPieceObject.transform.SetParent(transform);
        newPieceObject.transform.localScale = new Vector3(1, 1, 1);
        newPieceObject.transform.localRotation = Quaternion.identity;
        Pawn newPiece = (Pawn)newPieceObject.AddComponent(typeof(Pawn));

        return newPiece;
    }

    private void PlacePieces(int firstRow, int scndRow, List<Pawn> pieces, Board board)
    {
        for (int i = 0; i < 8; i++)
        {
            pieces[i].Place(board.AllCells[i, firstRow]);
            pieces[i + 8].Place(board.AllCells[i, scndRow]);
        }
    }

    private void SetInteractive(List<Pawn> allPieces, bool value)
    {
        foreach (Pawn piece in allPieces)
            piece.enabled = value;
    }

    public void SwitchSides(Color color)
    {
        bool isBlackTurn = color == Color.white ? true : false;

        SetInteractive(whitePieces, !isBlackTurn);
        SetInteractive(blackPieces, isBlackTurn);
    }

    public void AddPoint(Pawn pawn)
    {
        pawn.Kill();
        if(pawn.Color== Color.black)
        {
            blackPoints++;
        }
        else
        {
            whitePoints++;
        }
        addPoint?.Invoke(blackPoints,whitePoints);
    }
}
