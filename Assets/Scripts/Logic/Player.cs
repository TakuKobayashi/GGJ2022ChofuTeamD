using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    [SerializeField] private List<BoardPosition> territoryPositions;
    // 各駒の初期配置の位置を定義
    [SerializeField] private List<PiecePosition> defaultPieces;
    private List<Piece> currentPieces = new List<Piece>();
    public List<Piece> CurrentPieces{ get{ return currentPieces; } }

    // 初期配置、拠点陣地かどうか判別する
    public bool CheckTerritoryPosition(int x, int y)
    {
        return territoryPositions.Exists(tp => tp.x == x && tp.y == y);
    }

    public void SpawnPieces(List<Square> allGridSquares, bool isReverse)
    {
        foreach(PiecePosition piecePosition in defaultPieces)
        {
            Square gridSquare = allGridSquares.Find(g => g.boardPosition.x == piecePosition.position.x && g.boardPosition.y == piecePosition.position.y);
            if(gridSquare != null)
            {
				this.SpawnPiece(gridSquare, piecePosition.pieceObj.gameObject, isReverse);
			}
        }
    }

	public void SpawnPiece(Square gridSquare, GameObject pieceObj, bool isReverse)
	{
		Piece piece = ComponentUtil.InstantiateTo<Piece>(gridSquare.gameObject, pieceObj);
		if (isReverse)
		{
			piece.transform.localScale = new Vector3(-piece.transform.localScale.x, piece.transform.localScale.y, piece.transform.localScale.z);
		}
		piece.Initialize(this, gridSquare.boardPosition.x, gridSquare.boardPosition.y);
		this.currentPieces.Add(piece);
	}

	public void LostPiece(Piece willLostPiece)
    {
		if (currentPieces.Remove(willLostPiece))
        {
			willLostPiece.LostItem();
			GameObject.Destroy(willLostPiece.gameObject);
        }
    }
}
