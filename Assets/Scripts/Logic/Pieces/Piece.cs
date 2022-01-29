using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 駒の基底クラス
public abstract class Piece : MonoBehaviour
{
    // その駒がそもそも移動可能なマス目の一覧
    // 例1: 右に一マス移動可能な場合はx: 1,y: 0と入力
    // 例2: 左に一マス移動可能な場合はx: -1,y: 0と入力
    // 例3: 上に一マス移動可能な場合はx: 0,y: 1と入力
    // 例4: 下に一マス移動可能な場合はx: 0,y: -1と入力 
    [SerializeField] protected List<BoardPosition> defaultMovableBoardPositions;

    public BoardPosition CurrentPosition { private set; get; }

    public void Initialize(int positionX, int positionY)
    {
        this.CurrentPosition = new BoardPosition(positionX, positionY);
    }

    public List<Square> FilterMovableSquares(List<Square> allSquares, List<Piece> myPieses)
    {
        List<Square> pieceMovableSquares = new List<Square>();
        foreach (BoardPosition boardPosition in defaultMovableBoardPositions)
        {
            int willMoveX = CurrentPosition.x + boardPosition.x;
            int willMoveY = CurrentPosition.y + boardPosition.y;

            // 移動したい先に自分のコマがある時は移動可能なマスに含めない
            if (myPieses.Exists(piece => piece.CurrentPosition.x == willMoveX && piece.CurrentPosition.y == willMoveY)){
                continue;
            }
            // マスとして存在するもののみを絞り込む
            Square willMovableSquare = allSquares.Find(square => square.boardPosition.x == willMoveX && square.boardPosition.y == willMoveY);
            if(willMovableSquare != null)
            {
                pieceMovableSquares.Add(willMovableSquare);
            }
        }
        return pieceMovableSquares;
    }
}
