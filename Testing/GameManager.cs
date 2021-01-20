using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Board mBoard;
    [SerializeField]
    private PieceManager mPieceManager;

    void Start()
    {
        mBoard.Create();
        mPieceManager.Setup(mBoard);
    }
}
